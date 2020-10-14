namespace CalculatorApp.Models
{
    using Saige.MVVM;

    public class TimeViewModel : ViewModel
    {
        private string _calcValue;

        public string CalcValue
        {
            get => this._calcValue;
            set => SetProperty(ref this._calcValue, value);
        }
    }
}
