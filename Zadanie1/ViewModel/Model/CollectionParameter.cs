using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ViewModel.Model
{
    public class CollectionParameter : SingleParameter
    {
        private readonly List<SingleParameter> _parameters;

        public CollectionParameter(string name, double value, double min, double max, bool sumsUp, int states)
        :base(name, value, min, max)
        {
            SumsUp = sumsUp;
            var val = Max / states;
            _parameters = Enumerable.Repeat(new SingleParameter(Name, val, Min, Max, 0, BindValues), states)
                .ToList();
        }

        public bool SumsUp { get; set; }
        public IEnumerable<SingleParameter> Parameters => _parameters;

        public void AddParameter()
        {
            var val = SumsUp
                ? Max - _parameters.Sum(p => p.Value)
                : Max;
            _parameters.Add(new SingleParameter(Name, val, Min, Max, _parameters.Count, BindValues));
        }

        public void RemoveParameter()
        {
            if (!_parameters.Any())
                return;
            var last = _parameters.Last();
            _parameters.Remove(last);
            if (!SumsUp) return;

            var index = 0;
            _parameters[index].Value += last.Value;
        }

        private int GoToNextIndex(int index)
        {
            return index + 1 >= _parameters.Count ? 0 : index + 1;
        }

        private void BindValues(int index)
        {
            var remaining = Max - _parameters.Sum(p => p.Value);
            _parameters[GoToNextIndex(index)].Value += remaining;
        }
    }
}
