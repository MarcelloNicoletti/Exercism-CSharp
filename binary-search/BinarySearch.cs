using System;

public class BinarySearch
{
    private readonly int[] _values;

    public BinarySearch (int[] input)
    {
        _values = new int[input.Length];
        Array.Copy(input, _values, input.Length);
    }

    public int Find (int value)
    {
        if (_values.Length == 0)
        {
            return -1;
        }

        var start = 0;
        var end = _values.Length - 1;
        while (true)
        {
            var middle = (start + end) / 2;
            var guess = _values[middle];
            if (start == middle || end == middle)
            {
                return guess == value ? middle : -1;
            }

            if (guess == value)
            {
                return middle;
            }

            if (value < guess)
            {
                end = middle - 1;
            }
            else
            {
                start = middle + 1;
            }
        }
    }
}