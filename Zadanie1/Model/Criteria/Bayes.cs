using System;
using System.Collections.Generic;
using System.Linq;
using Model.Model;

namespace Model.Criteria
{
    public class Bayes : ICriterion
    {
        private const string Probabilities = "Probabilities";

        public IEnumerable<string> Choose(IDictionary<string, IList<double>> cases,
            IDictionary<string, object> parameters)
        {
            var probabilities = (IEnumerable<double>) parameters[Probabilities];
            var criteria = cases.ToDictionary(c => c.Key, c => GetCaseCriterion(c.Value, probabilities.ToList()));
            var max = criteria.Max(c => c.Value);

            return criteria.Where(c => Equals(max, c.Value)).Select(c => c.Key);
        }

        public IEnumerable<Parameter> GetParameters()
        {
            return new[]
            {
                new Parameter(Probabilities, 0.0d, 0.0d, 1.0d, true, 1.0d)
            };
        }

        private static double GetCaseCriterion(IList<double> values, ICollection<double> probabilities)
        {
            if (values.Count != probabilities.Count)
                throw new ArgumentException("values.Count != probabilities.Count"); //TODO

            return probabilities.Select((p, i) => p * values[i]).Aggregate((v1, v2) => v1 + v2);
        }
    }
}