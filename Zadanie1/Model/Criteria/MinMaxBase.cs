using System;
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
            var max = criteria.Max(c => c.Value);

            return criteria
                .Where(c => Math.Abs(c.Value - max) < double.Epsilon)
                .Select(c => c.Key);
        }

        public IEnumerable<Parameter> GetParameters()
        {
            return Enumerable.Empty<Parameter>();
        }

        protected abstract double GetCaseCriterion(IList<double> cases);
    }
}