using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model.CsvReader;
using Model.Model;
using Model.Solver;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Graph _graph;

        private bool _isCsvLoaded;
        private bool _isSolved;
        private Strategy _optimalStrategy;

        public MainViewModel()
        {
            LoadCsvCommand = new CommandHandler<Func<string>>(LoadCsv, () => true);
            SolveCommand = new CommandHandler<Action<Graph, Path>>(Solve, () => IsCsvLoaded);
        }

        public bool IsCsvLoaded
        {
            get => _isCsvLoaded;
            set
            {
                if (_isCsvLoaded == value)
                    return;
                _isCsvLoaded = value;
                SolveCommand.InvokeCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public bool IsSolved
        {
            get => _isSolved;
            set
            {
                if (value == _isSolved) return;
                _isSolved = value;
                OnPropertyChanged();
            }
        }

        public Strategy OptimalStrategy
        {
            get => _optimalStrategy;
            set
            {
                if (value == _optimalStrategy) return;
                _optimalStrategy = value;
                OnPropertyChanged();
            }
        }

        public CommandHandler<Func<string>> LoadCsvCommand { get; set; }
        public CommandHandler<Action<Graph, Path>> SolveCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private void LoadCsv(Func<string> getFilePath)
        {
            var path = getFilePath();
            if (path == null)
                return;
            _graph = CsvParser.ParseCsv(path);
            IsCsvLoaded = true;
        }

        private void Solve(Action<Graph, Path> draw)
        {
            OptimalStrategy = OptimalStrategyFinder.FindOptimalStrategy(_graph);
            IsSolved = true;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}