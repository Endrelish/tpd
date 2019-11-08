using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Model;
using Model.Model;
using ViewModel.Annotations;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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

        private IEnumerable<IEnumerable<double>> _values;
        private bool _isCsvLoaded;
        private bool _isSolved;


        public MainViewModel()
        {
            LoadCsvCommand = new CommandHandler<Func<string>>(LoadCsv, () => true);
            SolveCommand = new CommandHandler<Action>(Solve, () => IsCsvLoaded);
            Solution = new GameSolution();
        }

        public CommandHandler<Func<string>> LoadCsvCommand { get; set; }
        public CommandHandler<Action> SolveCommand { get; set; }

        private void LoadCsv(Func<string> getFilePath)
        {
            var path = getFilePath();
            if (path == null)
                return;
            _values = CsvReader.ReadCsv(path).Select(r => r.Select(double.Parse));
            IsCsvLoaded = true;
        }

        private void Solve()
        {
            var solver = new GameSolver();
            var solution = solver.Solve(new PayoffMatrix(_values.Select(r => (IList<double>) r.ToList()).ToList()));
            CopySolution(solution);
            IsSolved = true;
        }

        private void CopySolution(GameSolution solution)
        {
            Solution.XStrategy = solution.XStrategy;
            Solution.XPayoff = solution.XPayoff;
            Solution.YStrategy = solution.YStrategy;
            Solution.YPayoff = solution.YPayoff;
        }

        public GameSolution Solution { get; set; }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}