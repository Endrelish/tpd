using System.Collections.Generic;
using Logic.Criteria;
using System.Linq;

namespace View.ViewModels
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            Criteria = new Dictionary<string, ICriterion>
            {
                ["Kryterium Bayesa"] = new Bayes(),
                ["Kryterium Hurwicza"] = new Hurwicz(),
                ["Kryterium Minimax użyteczności"] = new PesimisticMinMax(),
                ["Kryterium Minimax zawodu"] = new OptimisticMinMax(),
                ["Kryterium Savage'a"] = new Savage()
            };
            CurrentCriterion = Criteria.Values.First();
        }
        public Dictionary<string, ICriterion> Criteria { get; set; }
        public ICriterion CurrentCriterion { get; set; }
    }
}