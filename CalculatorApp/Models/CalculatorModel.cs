using Saige.MVVM;

namespace CalculatorApp.Models
{
    public class CalculatorModel : ViewModel
    {
        private string calcValue;

        public string CalcValue
        {
            get => calcValue;
            set => SetProperty(ref calcValue, value);
        }
    }
}
