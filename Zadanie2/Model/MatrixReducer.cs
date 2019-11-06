using System.Linq;
using Model.Model;

namespace Model
{
    public static class MatrixReducer
    {
        public static void Reduce(this PayoffMatrix matrix)
        {
            ReduceRows(matrix);
            ReduceColumns(matrix);
        }

        private static void ReduceRows(PayoffMatrix matrix)
        {
            for (var i = matrix.RowsCount - 1; i >= 0; i--)
            {
                if (IsRowDominated(matrix, i))
                    matrix.RemoveRow(i);
            }
        }

        private static void ReduceColumns(PayoffMatrix matrix)
        {
            for (var i = matrix.ColumnsCount - 1; i >= 0; i--)
            {
                if(IsColumnDominated(matrix, i))
                    matrix.RemoveColumn(i);
            }
        }

        private static bool IsRowDominated(PayoffMatrix matrix, int row)
        {
            var rowValues = matrix.GetRow(row);
            for (var i = 0; i < matrix.RowsCount; i++)
            {
                if(i == row)
                    continue;

                var currentRow = matrix.GetRow(i);
                if (rowValues.Select((r, j) => r < currentRow[j]).All(v => v))
                    return true;
            }

            return false;
        }

        private static bool IsColumnDominated(PayoffMatrix matrix, int column)
        {
            var columnValues = matrix.GetColumn(column);
            for (var i = 0; i < matrix.ColumnsCount; i++)
            {
                if(i == column)
                    continue;

                var currentColumn = matrix.GetColumn(i);
                if (columnValues.Select((c, j) => c > currentColumn[j]).All(v => v))
                    return true;
            }

            return false;
        }
    }
}