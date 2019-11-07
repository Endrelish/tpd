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
        public IDictionary<int, double> XStrategy { get; set; }
        public double Payoff { get; set; }
        public IDictionary<int, double> YStrategy { get; set; }
    }
}