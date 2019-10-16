using System.Collections.Generic;
using System.Linq;

public class PesimisticMinMax : MinMaxBase
{
    protected override double GetCaseCriterion(IList<double> values) => values.Min();
}