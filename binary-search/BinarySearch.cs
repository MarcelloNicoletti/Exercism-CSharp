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
        var left = 0;
        var right = _values.Length - 1;
        while (left <= right)
        {
            var middle = (left + right) / 2;

            if (_values[middle] == value)
            {
                return middle;
            }

            if (value < _values[middle])
            {
                right = middle - 1;
            }
            else
            {
                left = middle + 1;
            }
        }

        return -1;
    }
}
