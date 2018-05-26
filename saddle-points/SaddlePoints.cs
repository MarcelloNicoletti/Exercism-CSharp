using System;
using System.Collections.Generic;
using System.Data;
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
        var rowMaxes = new int[_matrix.NumRows];
        var colMins = new int[_matrix.NumCols];
        var candidateSaddlePoints = new List<(int, int)>();

        bool IsSaddlePoint (int row, int col, int val) => rowMaxes[row] == val && colMins[col] == val;

        for (var row = 0; row < _matrix.NumRows; row++)
        {
            for (var col = 0; col < _matrix.NumCols; col++)
            {
                var val = _matrix[row, col];
                if (col == 0)
                {
                    rowMaxes[row] = val;
                }
                else if (val > rowMaxes[row])
                {
                    rowMaxes[row] = val;
                }

                if (row == 0)
                {
                    colMins[col] = val;
                }
                else if (val < colMins[col])
                {
                    colMins[col] = val;
                }

                if (IsSaddlePoint(row, col, val))
                {
                    candidateSaddlePoints.Add((row, col));
                }
            }
        }
        
        foreach (var (row, col) in candidateSaddlePoints)
        {
            if (IsSaddlePoint(row, col, _matrix[row, col]))
            {
                yield return (row, col).ToTuple();
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
}
