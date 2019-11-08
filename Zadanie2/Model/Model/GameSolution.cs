using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Model.Annotations;

namespace Model.Model
{
    public class GameSolution : INotifyPropertyChanged
    {
        private IDictionary<string, double> _xStrategy;
        private IDictionary<string, double> _yStrategy;
        private double _xPayoff;
        private double _yPayoff;

        public GameSolution()
        {
            XStrategy = new Dictionary<string, double>();
            YStrategy = new Dictionary<string, double>();
        }

        public IDictionary<string, double> XStrategy
        {
            get => _xStrategy;
            set
            {
                if (Equals(value, _xStrategy)) return;
                _xStrategy = value;
                OnPropertyChanged();
            }
        }

        public double XPayoff
        {
            get => _xPayoff;
            set
            {
                if (value.Equals(_xPayoff)) return;
                _xPayoff = value;
                OnPropertyChanged();
            }
        }

        public double YPayoff
        {
            get => _yPayoff;
            set
            {
                if (value.Equals(_yPayoff)) return;
                _yPayoff = value;
                OnPropertyChanged();
            }
        }

        public IDictionary<string, double> YStrategy
        {
            get => _yStrategy;
            set
            {
                if (Equals(value, _yStrategy)) return;
                _yStrategy = value;
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