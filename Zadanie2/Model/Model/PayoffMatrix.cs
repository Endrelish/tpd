using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Model
{
    public class PayoffMatrix
    {
        private readonly IList<IList<double>> _values;
        public IList<string> XLabels { get; }
        public IList<string> YLabels { get; }

        public PayoffMatrix(IList<IList<double>> values)
        {
            _values = values;
            XLabels = Enumerable.Range(1, ColumnsCount + 1).Select(i => $"x{i}").ToList();
            YLabels = Enumerable.Range(1, RowsCount + 1).Select(i => $"y{i}").ToList();
        }

        public int RowsCount => _values.Count;
        public int ColumnsCount => _values.First().Count;

        public IList<double> GetRow(int index)
        {
            if (index >= _values.Count)
                throw new IndexOutOfRangeException();
            return _values[index];
        }

        public IList<double> GetColumn(int index)
        {
            if(index >= _values[0].Count)
                throw new IndexOutOfRangeException();
            return _values.Select(v => v[index]).ToList();
        }

        public void RemoveRow(int row)
        {
            _values.RemoveAt(row);
            YLabels.RemoveAt(row);
        } 

        public void RemoveColumn(int column)
        {
            foreach (var val in _values)
                val.RemoveAt(column);
            XLabels.RemoveAt(column);
        }

        public IEnumerable<IEnumerable<double>> Rows => _values;

        public IEnumerable<IEnumerable<double>> Columns
        {
            get
            {
                for (var i = 0; i < ColumnsCount; i++)
                    yield return GetColumn(i);
            }
        }

        public double this[int i, int j]
        {
            get => _values[i][j];
            set => _values[i][j] = value;
        }

        public override string ToString()
        {
            return string.Join(";\n", _values.Select(v => string.Join(",", v)));
        }
    }
}