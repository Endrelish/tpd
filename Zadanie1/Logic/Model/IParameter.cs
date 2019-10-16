using System;

namespace Logic.Model
{
    public interface IParameter<out T>
    {
        string Name { get; }
        T Value { get; }
        T Max { get; }
        T Min { get; }
        Type Type { get; }
    }
}