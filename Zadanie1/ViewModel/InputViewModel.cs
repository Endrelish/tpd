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
        private readonly ObservableCollection<KeyValuePair<ObservableValue<string>, ObservableCollection<ObservableValue<double>>>> _states;
        private readonly ObservableCollection<ObservableValue<string>> _caseLabels;
        public event PropertyChangedEventHandler PropertyChanged;

        public InputViewModel()
        {
            _caseLabels = new ObservableCollection<ObservableValue<string>>{"Case 1", "Case 2"};
            _states = new ObservableCollection<KeyValuePair<ObservableValue<string>, ObservableCollection<ObservableValue<double>>>>
            {
                new KeyValuePair<ObservableValue<string>, ObservableCollection<ObservableValue<double>>>("State 1", new ObservableCollection<ObservableValue<double>> {0.0d, 0.0d}),
                new KeyValuePair<ObservableValue<string>, ObservableCollection<ObservableValue<double>>>("State 2", new ObservableCollection<ObservableValue<double>> {0.0d, 0.0d})
            };

            AddStateCommand = new CommandHandler(AddState, () => true);
            AddCaseCommand = new CommandHandler(AddCase, () => true);
            RunCommand = new CommandHandler(Run, () => true);
            RemoveCaseCommand = new CommandHandler(RemoveCase, () => true);
            RemoveStateCommand = new CommandHandler(RemoveState, () => true);
        }

        private void Run()
        {
            var cases = CaseLabels;
            var states = States;
        }

        public void InitializeCallbacks(Action stateAddCallback, Action stateRemoveCallback)
        {
            _stateAddCallback = stateAddCallback;
            _stateRemoveCallback = stateRemoveCallback;
        }

        public IEnumerable<ObservableValue<string>> CaseLabels => _caseLabels;

        public IEnumerable<KeyValuePair<ObservableValue<string>, ObservableCollection<ObservableValue<double>>>> States => _states;

        public ICommand AddStateCommand { get; }

        public ICommand AddCaseCommand { get; }
        public ICommand RunCommand { get; }

        public ICommand RemoveCaseCommand { get; }
        public ICommand RemoveStateCommand { get; }

        public void AddState()
        {
            _states.Add(new KeyValuePair<ObservableValue<string>, ObservableCollection<ObservableValue<double>>>("New state",
                new ObservableCollection<ObservableValue<double>>(Enumerable.Repeat<ObservableValue<double>>(0.0d, _caseLabels.Count))));
            _stateAddCallback();
        }

        public void RemoveState(ObservableValue<string> state)
        {
            var index = _states.IndexOf(_states.FirstOrDefault(s => s.Key.Equals(state)));
            _states.RemoveAt(index);
            _stateRemoveCallback();
        }

        public void AddCase()
        {
            _caseLabels.Add("New case");
            foreach (var state in _states)
                state.Value.Add(0.0d);
            
        }

        public void RemoveCase(ObservableValue<string> caseKey)
        {
            var index = _caseLabels.IndexOf(caseKey);
            _caseLabels.RemoveAt(index);
            foreach (var st in _states)
                st.Value.RemoveAt(index);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}