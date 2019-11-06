using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Model.Model
{
    public class PayoffMatrix
    {
        private readonly IList<IList<double>> _values;

        public PayoffMatrix(IList<IList<double>> values)
        {
            _values = values;
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

        public void AddRow(IList<double> values)
        {
            if(_values[0].Count != values.Count)
                throw new ArgumentException("Invalid row length");
            _values.Add(values);
        }

        public void AddColumn(IList<double> values)
        {
            if(_values.Count != values.Count)
                throw new ArgumentException("Invalid column length");

            for (var i = 0; i < values.Count; i++)
                _values[i].Add(values[i]);
        }

        public void RemoveRow(int row) => _values.RemoveAt(row);

        public void RemoveColumn(int column)
        {
            foreach (var val in _values)
                val.RemoveAt(column);
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