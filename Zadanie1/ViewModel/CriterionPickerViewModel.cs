using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Model.Criteria;
using ViewModel.Annotations;
using ViewModel.Model;

namespace ViewModel
{
    public class CriterionPickerViewModel : INotifyPropertyChanged
    {
        private readonly InputViewModel _inputViewModel;
        private ICriterion _currentCriterion;
        private List<CollectionParameter> _collectionParameters;
        private List<SingleParameter> _singleParameters;

        public CriterionPickerViewModel(InputViewModel inputViewModel)
        {
            _inputViewModel = inputViewModel;
            Criteria = new Dictionary<string, ICriterion>
            {
                ["Bayes"] = new Bayes(),
                ["Hurwicz"] = new Hurwicz(),
                ["Pessimistic MinMax"] = new PessimisticMinMax(),
                ["Optimistic MinMax"] = new OptimisticMinMax(),
                ["Savage"] = new Savage()
            };
            CurrentCriterion = Criteria.Values.First();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public Dictionary<string, ICriterion> Criteria { get; set; }

        public void AddStateParameters()
        {
            _collectionParameters.ForEach(p => p.AddParameter());
        }

        public void RemoveStateParameters()
        {
            _collectionParameters.ForEach(p => p.RemoveParameter());
        }

        public ICriterion CurrentCriterion
        {
            get => _currentCriterion;
            set
            {
                if (Equals(value, _currentCriterion)) return;
                _currentCriterion = value;
                SetParameters();
                OnPropertyChanged();
            }
        }

        public List<SingleParameter> SingleParameters
        {
            get => _singleParameters;
            set
            {
                if (Equals(value, _singleParameters)) return;
                _singleParameters = value;
                OnPropertyChanged();
            }
        }

        public List<CollectionParameter> CollectionParameters
        {
            get => _collectionParameters;
            set
            {
                if (Equals(value, _collectionParameters)) return;
                _collectionParameters = value;
                OnPropertyChanged();
            }
        }

        public void SetParameters()
        {
            var states = _inputViewModel.States.Count();
            var parameters = CurrentCriterion.GetParameters().ToList();
            SingleParameters = parameters
                .Where(p => !p.IsCollection)
                .Select(p => new SingleParameter(p.Name, p.Value, p.Min, p.Max))
                .ToList();
            CollectionParameters = parameters
                .Where(p => p.IsCollection)
                .Select(p => new CollectionParameter(p.Name, p.Value, p.Min, p.Max, p.SumsUp, states))
                .ToList();
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}