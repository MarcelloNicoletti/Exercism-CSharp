using System;

public static class CollatzConjecture
{
    public static int Steps(int number)
    {
        if (number <= 0)
        {
            throw new ArgumentException();
        }

        if (number == 1)
        {
            return 0;
        }

        return ((number % 2 == 0) ? Steps(number / 2) : Steps(number * 3 + 1)) + 1;
    }
}