using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model.CsvReader;
using Model.Model;
using Model.Solver;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Path _criticalPath;
        private Graph _graph;

        private bool _isCsvLoaded;
        private bool _isSolved;

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
                OnPropertyChanged(nameof(CriticalPath));
                OnPropertyChanged(nameof(Duration));
            }
        }

        public string CriticalPath => _criticalPath?.ToString();
        public string Duration => _criticalPath?.Duration.ToString();

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
            var cpf = new CriticalPathFinder();
            _criticalPath = cpf.FindCriticalPath(_graph);
            IsSolved = true;
            draw(_graph, _criticalPath);
        }

        private void CopySolution()
        {
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}