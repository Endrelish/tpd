using System;

namespace Model.Model
{
    public class Parameter
    {
        public Parameter(string name, double value, double min, double max)
        {
            Name = name;
            Value = value;
            Min = min;
            Max = max;
            IsCollection = false;
        }
        public Parameter(string name, double value, double min, double max, bool sumsUp, double sum)
        {
            Name = name;
            Value = value;
            Min = min;
            Max = max;
            IsCollection = true;
            SumsUp = sumsUp;
            Sum = sum;
        }

        public string Name { get; }
        public double Value { get; }

        public double Max { get; }

        public double Min { get; }

        #region CollectionAttributes
        public bool IsCollection { get; }
        public bool SumsUp { get; }
        public double Sum { get; }
        #endregion
    }
}