using System.Collections.Generic;
using System.Linq;
using Logic.Model;

namespace Logic.Criteria
{
    public class OptimisticMinMax : MinMaxBase
    {
        protected override double GetCaseCriterion(IList<double> values) => values.Max();
    }
}