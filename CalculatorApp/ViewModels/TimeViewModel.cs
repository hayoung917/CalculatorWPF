namespace CalculatorApp.ViewModels
{
    using Saige.MVVM;

    public class TimeViewModel : ViewModel
    {
        private double _calcValue;

        public double CalcValue
        {
            get => this._calcValue;
            set => SetProperty(ref this._calcValue, value);
        }

        public string OuputTimeUnit { get; set; }
    }
}
