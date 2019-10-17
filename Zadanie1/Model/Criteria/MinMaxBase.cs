using System.Collections.Generic;
using System.Linq;
using Model.Model;

namespace Model.Criteria
{
    public abstract class MinMaxBase : ICriterion
    {
        public IEnumerable<string> Choose(IDictionary<string, IList<double>> cases,
            IDictionary<string, object> parameters)
        {
            var criteria = cases
                .ToDictionary(c => c.Key, c => GetCaseCriterion(c.Value));
            var max = cases.Max(c => c.Value);

            return cases
                .Where(c => Equals(c, max))
                .Select(c => c.Key);
        }

        public IEnumerable<Parameter> GetParameters()
        {
            return Enumerable.Empty<Parameter>();
        }

        protected abstract double GetCaseCriterion(IList<double> cases);
    }
}