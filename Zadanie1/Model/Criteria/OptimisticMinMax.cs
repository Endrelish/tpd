using System.Collections.Generic;
using System.Linq;

namespace Model.Criteria
{
    public class OptimisticMinMax : MinMaxBase
    {
        protected override double GetCaseCriterion(IList<double> values)
        {
            return values.Max();
        }
    }
}