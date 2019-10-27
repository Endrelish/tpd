using System.Collections.Generic;
using System.Linq;

namespace Model.Criteria
{
    public class PessimisticMinMax : MinMaxBase
    {
        protected override double GetCaseCriterion(IList<double> values)
        {
            return values.Min();
        }
    }
}