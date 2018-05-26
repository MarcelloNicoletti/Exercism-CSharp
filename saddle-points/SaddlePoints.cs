using System;
using System.Collections.Generic;
using System.Linq;

public class SaddlePoints
{
    private readonly Matrix<int> _matrix;

    public SaddlePoints (int[,] values)
    {
        _matrix = new Matrix<int>(values);
    }

    public IEnumerable<Tuple<int, int>> Calculate ()
    {
        var maxForRow = Memoizer.Memoize((int x) => _matrix.GetRow(x).Max());
        var minForColumn = Memoizer.Memoize((int y) => _matrix.GetColumn(y).Min());

        for (var row = 0; row < _matrix.NumRows; row++)
        {
            for (var col = 0; col < _matrix.NumCols; col++)
            {
                if (maxForRow(row) == _matrix[row, col] && minForColumn(col) == _matrix[row, col])
                {
                    yield return (row, col).ToTuple();
                }
            }
        }
    }
}

internal class Matrix<T>
{
    private readonly T[,] _matrix;

    public Matrix (T[,] values)
    {
        _matrix = (T[,])values.Clone();
    }

    public int NumRows => _matrix.GetLength(0);
    public int NumCols => _matrix.GetLength(1);
    public T this [int row, int col] => _matrix[row, col];

    public IEnumerable<T> GetRow (int row)
    {
        return Enumerable.Range(0, NumCols).Select(col => this[row, col]);
    }

    public IEnumerable<T> GetColumn (int col)
    {
        return Enumerable.Range(0, NumRows).Select(row => this[row, col]);
    }
}

internal static class Memoizer
{
    public static Func<T, TResult> Memoize<T, TResult> (Func<T, TResult> func)
    {
        var map = new Dictionary<T, TResult>();
        return x =>
        {
            if (!map.ContainsKey(x))
            {
                map[x] = func(x);
            }

            return map[x];
        };
    }

    public static Func<T, TResult> AsMemoized<T, TResult> (this Func<T, TResult> func)
    {
        return Memoize(func);
    }
}
