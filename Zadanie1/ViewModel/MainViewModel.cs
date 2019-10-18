using System.Linq;

namespace ViewModel
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            InputViewModel = new InputViewModel();
            CriterionPickerViewModel = new CriterionPickerViewModel(InputViewModel);
            InputViewModel.InitializeCallbacks(CriterionPickerViewModel.AddStateParameters, CriterionPickerViewModel.RemoveStateParameters);
        }

        public CriterionPickerViewModel CriterionPickerViewModel { get; }
        public InputViewModel InputViewModel { get; }
        public string TestProp { get; set; } = "sdf";
    }
}