using System.Collections.Generic;
using Model.Model;

namespace Model.Criteria
{
    public interface ICriterion
    {
        IEnumerable<string> Choose(IDictionary<string, IList<double>> cases, IDictionary<string, object> parameters);
        IEnumerable<Parameter> GetParameters();
    }
}