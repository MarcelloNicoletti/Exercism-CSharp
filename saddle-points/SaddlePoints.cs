using System;
using System.Collections.Generic;
using System.Linq;

public class SaddlePoints
{
    private readonly int[,] _matrix;

    public SaddlePoints (int[,] values)
    {
        _matrix = values;
    }

    public IEnumerable<Tuple<int, int>> Calculate ()
    {
        var rowValues = new int[_matrix.GetLength(0)][];
        var colValues = new int[_matrix.GetLength(1)][];
        CacheRowsAndCols(rowValues, colValues);

        for (var row = 0; row < rowValues.Length; row++)
        {
            for (var col = 0; col < colValues.Length; col++)
            {
                var val = _matrix[row, col];
                if (rowValues[row].All(x => x <= val) && colValues[col].All(y => y >= val))
                {
                    yield return (row, col).ToTuple();
                }
            }
        }
    }

    private void CacheRowsAndCols (IList<int[]> rows, IList<int[]> cols)
    {
        for (var row = 0; row < rows.Count; row++)
        {
            rows[row] = new int[cols.Count];
            for (var col = 0; col < cols.Count; col++)
            {
                if (row == 0)
                {
                    cols[col] = new int[rows.Count];
                }

                rows[row][col] = _matrix[row, col];
                cols[col][row] = _matrix[row, col];
            }
        }
    }
}
