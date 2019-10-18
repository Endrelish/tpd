using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using ViewModel.Annotations;

namespace ViewModel.Model
{
    public class ObservableValue<T> : INotifyPropertyChanged
    {
        private T _value;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableValue(T value)
        {
            Value = value;
        }

        public static implicit operator ObservableValue<T>(T value)
        {
            return new ObservableValue<T>(value);
        }

        public T Value
        {
            get => _value;
            set
            {
                if (Equals(value, _value)) return;
                _value = value;
                OnPropertyChanged();
            }
        }

        public override string ToString() => Value.ToString();

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
