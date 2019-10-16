using System.Collections.Generic;

public interface ICriterion
{
    IEnumerable<string> Choose(IDictionary<string, IList<double>> cases, IDictionary<string, object> parameters);
    IEnumerable<IParameter<object>> GetParameters();
}