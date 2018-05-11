using System;

public static class CollatzConjecture
{
    public static int Steps(int number)
    {
        if (number <= 0)
        {
            throw new ArgumentException();
        }

        return Helper(number, 0);
    }

    private static int Helper(int number, int steps)
    {
        if (number == 1)
        {
            return steps;
        }

        return (number % 2 == 0) ? Helper(number / 2, steps + 1) : Helper(number * 3 + 1, steps + 1);
    }
}