using System;
using System.Linq;
using System.Collections.Generic;

public static class ArmstrongNumbers
{
    public static bool IsArmstrongNumber(int number)
    {
        var remainingDigits = number;
        var digits = new List<int>();
        while (remainingDigits > 0)
        {
            digits.Add(remainingDigits % 10);
            remainingDigits /= 10;
        }

        var numDigits = digits.Count;
        return number == digits.Select(x => Math.Pow(x, numDigits)).Sum();
    }
}