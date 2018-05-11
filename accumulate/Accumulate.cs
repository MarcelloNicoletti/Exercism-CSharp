using System;
using System.Collections.Generic;

public static class AccumulateExtensions
{
    public static IEnumerable<U> Accumulate<T, U>(this IEnumerable<T> collection, Func<T, U> func)
    {
        var accumulated = new List<U>();
        foreach (var item in collection)
        {
            yield return func(item);
        }
    }
}