using System.ComponentModel;
using System.Runtime.CompilerServices;
using ViewModel.Annotations;

namespace ViewModel.Model
{
    public class SingleParameter : INotifyPropertyChanged
    {
        private readonly string _name;
        private double _value;
        private ValueBinding _binder;

        public SingleParameter(string name, double value, double min, double max, int index = -1, ValueBinding binder = null)
        {
            _name = name;
            Value = value;
            Min = min;
            Max = max;
            Index = index;
            _binder = binder;
        }

        public string Name
        {
            get => Index >= 0 ? $"{_name}[{Index}]" : _name;
        }

        public int Index { get; }

        public double Value
        {
            get => _value;
            set
            {
                if (value.Equals(_value)) return;
                _value = value;
                OnPropertyChanged();
                _binder?.Invoke(Index);
            }
        }

        public double Min { get; }
        public double Max { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public delegate void ValueBinding(int index);
}