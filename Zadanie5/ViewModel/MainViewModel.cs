using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model;
using Model.Model;
using Task = System.Threading.Tasks.Task;

namespace ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool _isSolved;
        private Output _output;

        public MainViewModel()
        {
            SolveCommand = new CommandHandler<Action<Output>>(Solve, () => true);
            Input = new Input {AcceptedRate = 2, IncomeProbability = 0.3d, NewRate = 3, TasksNumber = 10};
        }

        public Input Input { get; set; }

        public Output Output
        {
            get { return _output;}
            set
            {
                if (value == _output)
                    return;
                _output = value;
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

        public CommandHandler<Action<Output>> SolveCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void Solve(Action<Output> drawChart)
        {
            var srr = new SelfishRoundRobin(Input);
            Output = srr.Process();
            IsSolved = true;
            await Task.Run(() => drawChart(Output));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}