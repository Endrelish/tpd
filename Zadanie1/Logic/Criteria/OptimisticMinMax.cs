using System.Collections.Generic;
using System.Linq;

public class OptimisticMinMax : MinMaxBase
{
    protected override double GetCaseCriterion(IList<double> values) => values.Max();
}