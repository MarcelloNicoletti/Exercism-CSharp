using System;

public static class Bob
{
    private const string DefaultResponse = "Whatever.";
    private const string EmptyResponse = "Fine. Be that way!";
    private const string ShoutResponse = "Whoa, chill out!";
    private const string AskRespose = "Sure.";
    private const string ShoutedAskResponce = "Calm down, I know what I'm doing!";
    
    public static string Response(string statement)
    {
        var response = DefaultResponse;
        var trimmedStatement = statement.Trim();

        if (string.IsNullOrEmpty(trimmedStatement)) {
            response = EmptyResponse;
        }
        else if (IsShouting(trimmedStatement) && IsAsking(trimmedStatement))
        {
            response = ShoutedAskResponce;
        }
        else if (IsShouting(trimmedStatement))
        {
            response = ShoutResponse;
        }
        else if (IsAsking(trimmedStatement))
        {
            response = AskRespose;
        }

        return response;
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
        return statement.EndsWith('?');
    }
}