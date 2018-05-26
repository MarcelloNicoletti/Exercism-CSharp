using System;
using System.Collections.Generic;

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

        for (var row = 0; row < _matrix.NumRows; row++)
        {
            for (var col = 0; col < _matrix.NumCols; col++)
            {
                var cellVal = _matrix[row, col];
                if (col == 0)
                {
                    rowMaxes[row] = cellVal;
                }

                if (row == 0)
                {
                    colMins[col] = cellVal;
                }

                if (cellVal > rowMaxes[row])
                {
                    rowMaxes[row] = cellVal;
                }

                if (cellVal < colMins[col])
                {
                    colMins[col] = cellVal;
                }
            }
        }

        for (var row = 0; row < _matrix.NumRows; row++)
        {
            for (var col = 0; col < _matrix.NumCols; col++)
            {
                var cellVal = _matrix[row, col];
                if (rowMaxes[row] == cellVal && colMins[col] == cellVal)
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
}
