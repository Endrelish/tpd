using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ViewModel.Model
{
    public class CollectionParameter : SingleParameter
    {
        private readonly ObservableCollection<SingleParameter> _parameters;

        public CollectionParameter(string name, double value, double min, double max, bool sumsUp, int states)
        :base(name, value, min, max)
        {
            SumsUp = sumsUp;
            var val = Max / states;
            _parameters = new ObservableCollection<SingleParameter>();
            for(var i = 0; i < states; i++)
                _parameters.Add(new SingleParameter(Name, val, Min, Max, i, BindValues));
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
            while (true)
            {
                var remaining = Max - _parameters.Sum(par => par.Value);
                var i = GoToNextIndex(index);
                var p = _parameters[i];
                if (p.Value + remaining < Min)
                    if (Math.Abs(p.Value - Min) < double.Epsilon)
                    {
                        index = i;
                        continue;
                    }
                    else
                        p.Value = Min;
                else if (_parameters[i].Value + remaining > Max)
                    if (Math.Abs(p.Value - Max) < double.Epsilon)
                    {
                        index = i;
                        continue;
                    }
                    else
                        p.Value = Max;
                else
                    _parameters[i].Value += remaining;

                break;
            }
        }
    }
}
