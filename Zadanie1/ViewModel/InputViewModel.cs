using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Windows.Input;
using ViewModel.Annotations;
using ViewModel.Model;

namespace ViewModel
{
    public class InputViewModel : INotifyPropertyChanged
    {
        private Action _stateAddCallback;
        private Action _stateRemoveCallback;
        private ObservableCollection<KeyValuePair<ObservableValue<string>, ObservableCollection<ObservableValue<double>>>> _states;
        private ObservableCollection<ObservableValue<string>> _caseLabels;

        public InputViewModel()
        {
            _caseLabels = new ObservableCollection<ObservableValue<string>>{"Case 1", "Case 2"};
            _states = new ObservableCollection<KeyValuePair<ObservableValue<string>, ObservableCollection<ObservableValue<double>>>>
            {
                new KeyValuePair<ObservableValue<string>, ObservableCollection<ObservableValue<double>>>("State 1", new ObservableCollection<ObservableValue<double>> {0.0d, 0.0d}),
                new KeyValuePair<ObservableValue<string>, ObservableCollection<ObservableValue<double>>>("State 2", new ObservableCollection<ObservableValue<double>> {0.0d, 0.0d})
            };

            AddStateCommand = new CommandHandler<ObservableValue<string>>(AddState, () => true);
            AddCaseCommand = new CommandHandler<ObservableValue<string>>(AddCase, () => true);
            RemoveCaseCommand = new CommandHandler<ObservableValue<string>>(RemoveCase, () => true);
            RemoveStateCommand = new CommandHandler<ObservableValue<string>>(RemoveState, () => true);
        }


        public void InitializeCallbacks(Action stateAddCallback, Action stateRemoveCallback)
        {
            _stateAddCallback = stateAddCallback;
            _stateRemoveCallback = stateRemoveCallback;
        }

        public ObservableCollection<ObservableValue<string>> CaseLabels
        {
            get { return _caseLabels; }
            set
            {
                if (Equals(value, _caseLabels)) return;
                _caseLabels = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<KeyValuePair<ObservableValue<string>, ObservableCollection<ObservableValue<double>>>> States
        {
            get { return _states; }
            set
            {
                if (Equals(value, _states)) return;
                _states = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddStateCommand { get; }

        public ICommand AddCaseCommand { get; }

        public ICommand RemoveCaseCommand { get; }
        public ICommand RemoveStateCommand { get; }

        private void AddState()
        {
            _states.Add(new KeyValuePair<ObservableValue<string>, ObservableCollection<ObservableValue<double>>>("New state",
                new ObservableCollection<ObservableValue<double>>(Enumerable.Repeat(0.0d, _caseLabels.Count).Select(v => new ObservableValue<double>(v)))));
            _stateAddCallback();
        }

        private void RemoveState(ObservableValue<string> state)
        {
            var index = _states.IndexOf(_states.FirstOrDefault(s => s.Key.Equals(state)));
            _states.RemoveAt(index);
            _stateRemoveCallback();
        }

        private void AddCase()
        {
            _caseLabels.Add("New case");
            foreach (var state in _states)
                state.Value.Add(0.0d);
            
        }

        private void RemoveCase(ObservableValue<string> caseKey)
        {
            var index = _caseLabels.IndexOf(caseKey);
            _caseLabels.RemoveAt(index);
            foreach (var st in _states)
                st.Value.RemoveAt(index);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}