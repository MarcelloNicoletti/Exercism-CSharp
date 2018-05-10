using System;
using System.Text;

public static class Raindrops
{
    private const string Pling = "Pling";
    private const string Plang = "Plang";
    private const string Plong = "Plong";

    public static string Convert(int number)
    {
        var soundBuilder = new StringBuilder();
        if (number % 3 == 0)
        {
            soundBuilder.Append(Pling);
        }
        if (number % 5 == 0)
        {
            soundBuilder.Append(Plang);
        }
        if (number % 7 == 0)
        {
            soundBuilder.Append(Plong);
        }
        if (soundBuilder.Length == 0)
        {
            soundBuilder.Append(number);
        }

        return soundBuilder.ToString();
    }
}