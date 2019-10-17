using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using ViewModel.Annotations;

namespace ViewModel
{
    public class InputViewModel : INotifyPropertyChanged
    {
        private Action _stateAddCallback;
        private Action _stateRemoveCallback;
        private readonly List<string> _stateLabels;
        private readonly Dictionary<string, List<double>> _cases;
        public event PropertyChangedEventHandler PropertyChanged;

        public InputViewModel()
        {
            _stateLabels = new List<string> {"A", "B"};
            _cases = new Dictionary<string, List<double>>
            {
                ["I"] = new List<double> {0.0d, 0.0d},
                ["II"] = new List<double> {0.0d, 0.0d}
            };
        }

        public void InitializeCallbacks(Action stateAddCallback, Action stateRemoveCallback)
        {
            _stateAddCallback = stateAddCallback;
            _stateRemoveCallback = stateRemoveCallback;
        }

        public IEnumerable<string> StateLabels => _stateLabels;
        public IReadOnlyDictionary<string, List<double>> Cases => _cases;

        public void AddState()
        {
            _stateLabels.Add(string.Empty);
            foreach (var c in _cases)
                c.Value.Add(0.0d);
            _stateAddCallback();
        }

        public void RemoveState(string state)
        {
            var index = _stateLabels.FindIndex(s => s.Equals(state));
            _stateLabels.RemoveAt(index);
            foreach (var c in _cases)
                c.Value.RemoveAt(index);
            _stateRemoveCallback();
        }

        public void AddCase()
        {
            _cases.Add(string.Empty, Enumerable.Repeat(0.0d, _stateLabels.Count).ToList());
        }

        public void RemoveCase(string caseKey)
        {
            _cases.Remove(caseKey);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}