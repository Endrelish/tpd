using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model.CsvReader;
using ViewModel.Annotations;
using ViewModel.Model;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IEnumerable<string> _results;

        public MainViewModel()
        {
            InputViewModel = new InputViewModel();
            CriterionPickerViewModel = new CriterionPickerViewModel(InputViewModel);
            InputViewModel.InitializeCallbacks(CriterionPickerViewModel.AddStateParameters, CriterionPickerViewModel.RemoveStateParameters);
            RunCommand = new CommandHandler<Action>(Run, () => true);
            OpenFileCommand = new CommandHandler<Func<string>>(OpenFile, () => true);
        }


        public CriterionPickerViewModel CriterionPickerViewModel { get; }
        public InputViewModel InputViewModel { get; }
        public ICommand RunCommand { get; }
        public ICommand OpenFileCommand { get; set; }


        private void OpenFile(Func<string> getFileName)
        {
            var fileName = getFileName();
            var values = CsvReader.ReadCsv(fileName);
            ParseCsv(values);
        }

        private void ParseCsv(IEnumerable<IEnumerable<string>> values)
        {
            var lists = values.Select(v => v.ToList()).ToList();
            var caseLabels = lists.Skip(1).Select(l => l.First()).ToList();
            var states = lists.First().ToDictionary(l => l, l => new List<double>());
            for (var i = 0; i < states.Count; i++)
                states.ElementAt(i).Value.AddRange(lists.Skip(1).Select(l => double.Parse(l[i+1])));

            InputViewModel.CaseLabels = new ObservableCollection<ObservableValue<string>>(caseLabels.Select(c => new ObservableValue<string>(c)));
            InputViewModel.States = new ObservableCollection<KeyValuePair<ObservableValue<string>, ObservableCollection<ObservableValue<double>>>>(
                states.Select(s =>
                    new KeyValuePair<ObservableValue<string>, ObservableCollection<ObservableValue<double>>>(
                        s.Key, new ObservableCollection<ObservableValue<double>>(
                            s.Value.Select(v => new ObservableValue<double>(v)))))
                );
            CriterionPickerViewModel.SetParameters();
        }

        private void Run(Action playSound)
        {
            var criterion = CriterionPickerViewModel.CurrentCriterion;
            var parameters = CriterionPickerViewModel.CollectionParameters
                .ToDictionary(p => p.Name, p => (object) p.Parameters.Select(sp => sp.Value))
                .Concat(CriterionPickerViewModel.SingleParameters.ToDictionary(p => p.Name, p => (object) p.Value))
                .ToDictionary(p => p.Key, p => p.Value);

            var cases = InputViewModel.CaseLabels
                .Select((c, i) => new KeyValuePair<string, IList<double>>(
                    c.Value,
                    InputViewModel.States.Select(s => s.Value.ElementAt(i).Value).ToList()
                )).ToDictionary(c => c.Key, c => c.Value);

            Results = criterion.Choose(cases, parameters).ToList();
            playSound();
        }

        public IEnumerable<string> Results
        {
            get => _results;
            set
            {
                if (Equals(value, _results)) return;
                _results = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}