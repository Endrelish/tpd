using System.Collections.Generic;
using System.Linq;
using Model.Model;

namespace Model.Criteria
{
    public class Hurwicz : ICriterion
    {
        private const string PessimismCoefficient = "Pessimism coefficient";

        public IEnumerable<string> Choose(IDictionary<string, IList<double>> cases,
            IDictionary<string, object> parameters)
        {
            var pc = (double) parameters[PessimismCoefficient];
            var criteria = cases.ToDictionary(c => c.Key, c => GetCaseCriterion(c.Value, pc));
            var max = criteria.Values.Max();

            return criteria.Where(c => Equals(c.Value, max)).Select(c => c.Key);
        }

        public IEnumerable<Parameter> GetParameters()
        {
            return new[]
            {
                new Parameter(PessimismCoefficient, 0.25d, 0d, 1d)
            };
        }

        private static double GetCaseCriterion(IList<double> values, double pessimismCoefficient)
        {
            return pessimismCoefficient * values.Min() + (1 - pessimismCoefficient) * values.Max();
        }
    }
}