using System;

public static class Bob
{
    public static string Response(string statement)
    {
        if (string.IsNullOrEmpty(statement.Trim())) {
            return "Fine. Be that way!";
        }
        if (IsShouting(statement.Trim()))
        {
            if (IsAsking(statement.Trim()))
            {
                return "Calm down, I know what I'm doing!";
            }
            return "Whoa, chill out!";
        }
        if (IsAsking(statement.Trim()))
        {
            return "Sure.";
        }
        return "Whatever.";
    }

    private static bool IsShouting(string statement)
    {
        var hasLetters = false;
        for (int i = 0; i < statement.Length; i++)
        {
            if (Char.IsLetter(statement[i]))
            {
                hasLetters = true;
                if (!Char.IsUpper(statement[i])) {
                    return false;
                }
            }
        }
        return hasLetters;
    }

    private static bool IsAsking(string statement)
    {
        return statement[statement.Length - 1] == '?';
    }
}