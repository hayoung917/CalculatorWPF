namespace CalculatorApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using CalculatorApp.Models;
    using Saige.MVVM;

    public class MainViewModel : ViewModel
    {
        #region properties
        private string _numOutput;
        private IReadOnlyList<TimeViewModel> _timeCoverted;
        private TimeUnit _selectedUnit;
        private IEnumerable<TimeUnit> _units;

        public IEnumerable<TimeUnit> Units
        {
            get => this._units;
            private set => SetProperty(ref this._units, value);
        }
        public TimeUnit SelectedUnit
        {
            get => this._selectedUnit;
            set => SetProperty(ref this._selectedUnit, value);
        }
        public string NumOutput
        {
            get => this._numOutput;
            set => SetProperty(ref this._numOutput, value);
        }
        public IReadOnlyList<TimeViewModel> TimeConverted
        {
            get => this._timeCoverted;
            set => SetProperty(ref this._timeCoverted, value);
        }

        public ICommand InputCommand { get; }
        public ICommand BackspaceCommand { get; }
        public ICommand ClearCommand { get; }
        #endregion

        public MainViewModel()
        {
            this.Units = Enum.GetValues(typeof(TimeUnit)).OfType<TimeUnit>().ToArray();
            this.InputCommand = new RelayCommand<string>(InputNumber);
            this.BackspaceCommand = new RelayCommand(BackspaceNumber);
            this.ClearCommand = new RelayCommand(ClearNumber);
        }

        private void AppendDecimalPoint()
        {
            if (this.NumOutput.Contains(".") == false)
            {
                this.NumOutput += ".";
            }
        }

        private void InputNumber(string btnNum)
        {
            if (double.TryParse(btnNum, out double number) && number >= 0)
            {
                if (btnNum.Equals("."))
                    AppendDecimalPoint();
                else
                    this.NumOutput += btnNum;
            }
            else
            {
                this.NumOutput = " ";
            }
        }

        private void ClearNumber()
        {
            this.NumOutput = " ";
        }

        private void BackspaceNumber()
        {
            int length = 0;

            if (this.NumOutput != null)
            {
                length = this.NumOutput.Length - 1;
            }
            else
                this.NumOutput = " ";

            this.NumOutput = 0 < length ? this.NumOutput.Substring(0, length) : " ";
        }

        private void RemoveInvaildItem(List<TimeViewModel> resultList)
        {
            var converted = new List<TimeViewModel>();
            foreach (TimeViewModel result in resultList)
            {
                if (result.Unit.ToString() != this.SelectedUnit.ToString() && result.Value != 0)
                {
                    converted.Add(result);
                }
            }
            this.TimeConverted = converted;
        }

        protected override void OnPropertyChanged(object oldValue, object newValue, [CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(oldValue, newValue, propertyName);
            switch (propertyName)
            {
                case nameof(this.SelectedUnit):
                    OnSelectedComboTypeChanged(oldValue, newValue);
                    break;

                case nameof(this.NumOutput):
                    OnNumOutputChanged(oldValue, newValue);
                    break;
            }
        }

        protected virtual void OnSelectedComboTypeChanged(object oldValue, object newValue)
        {
            CalcInputNumber();
        }

        protected virtual void OnNumOutputChanged(object oldValue, object newValue)
        {
            CalcInputNumber();
        }

        private void CalcInputNumber()
        {
            _ = double.TryParse(this.NumOutput, out double inputCalculate);
            List<TimeViewModel> result = TimeViewModel.CovertedTimeViewModel(inputCalculate, this._selectedUnit);
            RemoveInvaildItem(result);
        }
    }
}
