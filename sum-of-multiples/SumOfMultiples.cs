using System;
using System.Collections.Generic;
using System.Linq;

public static class SumOfMultiples
{
    public static int Sum(IEnumerable<int> multiples, int max)
    {
        var uniqueMultiples = new HashSet<int>();
        foreach (var multiple in multiples)
        {
            uniqueMultiples.UnionWith(getAllPositiveMultiples(multiple).TakeWhile(x => x < max));
        }
        return uniqueMultiples.Sum();
    }

    private static IEnumerable<int> getAllPositiveMultiples(int baseValue)
    {
        for (var i = 1; i < int.MaxValue; i++) {
            yield return baseValue * i;
        }
    }
}