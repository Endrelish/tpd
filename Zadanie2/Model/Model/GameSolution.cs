using System.Collections;
using System.Collections.Generic;

namespace Model.Model
{
    public class GameSolution
    {
        public GameSolution()
        {
            XStrategy = new Dictionary<string, double>();
            YStrategy = new Dictionary<string, double>();
        }
        public IDictionary<string, double> XStrategy { get; set; }
        public double Payoff { get; set; }
        public IDictionary<string, double> YStrategy { get; set; }
    }
}