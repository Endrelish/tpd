using System;

namespace Logic.Model
{
    public class Parameter<T> : IParameter<T>
    {
        public Parameter(string name, T value, T min, T max)
        {
            Name = name;
            Value = value;
            Min = min;
            Max = max;
        }
        public string Name { get; }
        public T Value { get; }

        public T Max { get; }

        public T Min { get; }

        public Type Type => Value.GetType();
    }
}