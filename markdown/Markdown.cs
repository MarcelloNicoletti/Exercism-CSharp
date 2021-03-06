﻿using System;
using System.Text.RegularExpressions;

public static class Markdown
{
    private static string Wrap(string text, string tag) => $"<{tag}>{text}</{tag}>";

    private static bool IsTag(string text, string tag) => text.StartsWith($"<{tag}>");

    private static string ParseDelimited(string markdown, string delimiter, string tag)
    {
        var pattern = $"{delimiter}(.+?){delimiter}";
        var replacement = $"<{tag}>$1</{tag}>";
        return Regex.Replace(markdown, pattern, replacement);
    }

    private static string ParseBold(string markdown) => ParseDelimited(markdown, "__", "strong");

    private static string ParseItalic(string markdown) => ParseDelimited(markdown, "_", "em");

    private static string ParseText(string markdown, bool isList)
    {
        var parsedText = ParseItalic(ParseBold(markdown));

        return isList ? parsedText : Wrap(parsedText, "p");
    }

    private static string ParseHeader(string markdown, bool list, out bool inListAfter)
    {
        var count = CountHashes(markdown);

        if (count == 0)
        {
            inListAfter = list;
            return null;
        }

        var headerTag = "h" + count;
        var headerHtml = Wrap(markdown.Substring(count + 1), headerTag);

        if (list)
        {
            inListAfter = false;
            return "</ul>" + headerHtml;
        }
        else
        {
            inListAfter = false;
            return headerHtml;
        }
    }

    private static int CountHashes (string markdown)
    {
        var count = 0;

        foreach (var character in markdown)
        {
            if (character == '#')
            {
                count += 1;
            }
            else
            {
                break;
            }
        }

        return count;
    }

    private static string ParseLineItem(string markdown, bool list, out bool inListAfter)
    {
        if (markdown.StartsWith("*"))
        {
            var innerHtml = Wrap(ParseText(markdown.Substring(2), true), "li");

            if (list)
            {
                inListAfter = true;
                return innerHtml;
            }
            else
            {
                inListAfter = true;
                return "<ul>" + innerHtml;
            }
        }

        inListAfter = list;
        return null;
    }

    private static string ParseParagraph(string markdown, bool list, out bool inListAfter)
    {
        if (!list)
        {
            inListAfter = false;
            return ParseText(markdown, list);
        }
        else
        {
            inListAfter = false;
            return "</ul>" + ParseText(markdown, list);
        }
    }

    private static string ParseLine(string markdown, bool list, out bool inListAfter)
    {
        var result = ParseHeader(markdown, list, out inListAfter);

        if (result == null)
        {
            result = ParseLineItem(markdown, list, out inListAfter);
        }

        if (result == null)
        {
            result = ParseParagraph(markdown, list, out inListAfter);
        }

        if (result == null)
        {
            throw new ArgumentException("Invalid markdown");
        }

        return result;
    }

    public static string Parse(string markdown)
    {
        var lines = markdown.Split('\n');
        var result = "";
        var list = false;

        for (int i = 0; i < lines.Length; i++)
        {
            var lineResult = ParseLine(lines[i], list, out list);
            result += lineResult;
        }

        if (list)
        {
            return result + "</ul>";
        }
        else
        {
            return result;
        }
    }
}