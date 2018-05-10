using System;
using System.Collections.Generic;
using System.Linq;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max)
    {
        return Enumerable.Range(1, max - 1)
            .Where(x => multiples.Any(y => x % y == 0))
            .Sum();
    }
}