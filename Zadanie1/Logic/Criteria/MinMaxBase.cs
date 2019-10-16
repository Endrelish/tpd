using System.Collections.Generic;
using System.Linq;
using Logic.Model;

namespace Logic.Criteria
{
    public abstract class MinMaxBase : ICriterion
    {
        public IEnumerable<string> Choose(IDictionary<string, IList<double>> cases, IDictionary<string, object> parameters)
        {
            var criteria = cases
                .ToDictionary(c => c.Key, c => GetCaseCriterion(c.Value));
            var max = cases.Max(c => c.Value);

            return cases
                .Where(c => double.Equals(c, max))
                .Select(c => c.Key);
        }

        public IEnumerable<IParameter<object>> GetParameters()
        {
            return Enumerable.Empty<IParameter<object>>();
        }

        protected abstract double GetCaseCriterion(IList<double> cases);
    }
}