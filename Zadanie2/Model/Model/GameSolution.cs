using System.Collections;
using System.Collections.Generic;

namespace Model.Model
{
    public class GameSolution
    {
        public GameSolution()
        {
            XStrategy = new Dictionary<int, double>();
            YStrategy = new Dictionary<int, double>();
        }
        public IDictionary<int, double> XStrategy { get; }
        public IDictionary<int, double> YStrategy { get; }
    }
}