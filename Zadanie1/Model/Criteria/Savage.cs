using System.Collections.Generic;
using System.Linq;
using Model.Model;

namespace Model.Criteria
{
    public class Savage : ICriterion
    {
        public IEnumerable<string> Choose(IDictionary<string, IList<double>> cases,
            IDictionary<string, object> parameters)
        {
            var maxLosses = GetLosses(cases).ToDictionary(c => c.Key, c => c.Value.Max());
            var maxLoss = maxLosses.Max(l => l.Value);
            return maxLosses.Where(l => Equals(l.Value, maxLoss)).Select(l => l.Key);
        }

        public IEnumerable<Parameter> GetParameters()
        {
            return Enumerable.Empty<Parameter>();
        }

        public IDictionary<string, IList<double>> GetLosses(IDictionary<string, IList<double>> cases)
        {
            var maxVals = GetMaxValues(cases);
            return cases.ToDictionary(c => c.Key,
                c => (IList<double>) c.Value.Select((v, i) => maxVals[i] - v).ToList());
        }

        public IList<double> GetMaxValues(IDictionary<string, IList<double>> cases)
        {
            var count = cases.First().Value.Count;
            var maxVals = new List<double>(Enumerable.Repeat(0.0d, count));
            for (var i = 0; i < count; i++)
                maxVals[i] = cases.Max(c => c.Value[i]);

            return maxVals;
        }
    }
}