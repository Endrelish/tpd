using System.Collections.Generic;
using System.Linq;
using Logic.Model;

namespace Logic.Criteria
{
    public class PesimisticMinMax : MinMaxBase
    {
        protected override double GetCaseCriterion(IList<double> values) => values.Min();
    }
}