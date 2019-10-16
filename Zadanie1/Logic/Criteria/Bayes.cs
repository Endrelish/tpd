using System;
using System.Collections.Generic;
using System.Linq;

public class Bayes : ICriterion
{
    private const string Probabilities = "Probabilities";
    public IEnumerable<string> Choose(IDictionary<string, IList<double>> cases, IDictionary<string, object> parameters)
    {
        var probabilities = (IEnumerable<double>)parameters[Probabilities];
        var criteria = cases.ToDictionary(c => c.Key, c => GetCaseCriterion(c.Value, probabilities));
        var max = criteria.Max(c => c.Value);

        return criteria.Where(c => double.Equals(max, c.Value)).Select(c => c.Key);
    }

    private double GetCaseCriterion(IList<double> values, IEnumerable<double> probabilities)
    {
        if (values.Count != probabilities.Count())
            throw new ArgumentException("values.Count != probabilities.Count()"); //TODO

        return probabilities.Select((p, i) => p * values[i]).Aggregate((v1, v2) => v1 + v2);
    }

    public IEnumerable<IParameter<object>> GetParameters()
    {
        return new[]
        {
            new Parameter<IEnumerable<IParameter<double>>>(
                Probabilities,
                Enumerable.Empty<IParameter<double>>(),
                null,
                null
            )
        };
    }
}