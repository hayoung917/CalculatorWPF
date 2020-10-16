namespace CalculatorApp.ViewModels
{
    using System.Collections.Generic;
    using CalculatorApp.Models;
    using Saige.MVVM;

    public class TimeViewModel : ViewModel
    {
        private double _value;
        private TimeUnit _unit;

        public double Value
        {
            get => this._value;
            set => SetProperty(ref this._value, value);
        }

        public TimeUnit Unit
        {
            get => this._unit;
            set => SetProperty(ref this._unit, value);
        }

        public TimeViewModel(Time time)
        {
            ProjectModel(time);
        }

        private void ProjectModel(Time time)
        {
            this.Value = time?.Value ?? default;
            this.Unit = time?.Unit ?? default;
        }

        public static List<TimeViewModel> CovertedModelToViewModel(double inputCalculate, TimeUnit selectedComboType)
        {
            var result = new List<TimeViewModel>();
            List<Time> time = TimeCalculator.GetAllConvertedTimes(inputCalculate, selectedComboType);
            foreach (Time item in time)
            {
                result.Add(new TimeViewModel(item));
            }
            return result;
        }
    }
}
