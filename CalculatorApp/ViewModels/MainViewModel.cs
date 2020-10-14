namespace CalculatorApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using CalculatorApp.Models;
    using Saige.MVVM;

    public class MainViewModel : ViewModel
    {
        #region properties
        public ICommand InputCommand { get; private set; }
        public ICommand BackspaceCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }

        private string _numOutput;
        public string NumOutput
        {
            get => this._numOutput;
            set
            {
                SetProperty(ref this._numOutput, value);
                double inputToCalculate = Convert.ToDouble(this.NumOutput);
                List<TimeViewModel> result = TimeCalculator.ConvertAll(inputToCalculate, this._selectedComboType);
                SetResult(result);
            }
        }

        private List<TimeViewModel> _results;
        public List<TimeViewModel> Results
        {
            get => this._results;
            set => SetProperty(ref this._results, value);
        }

        private TimeUnit _selectedComboType;
        public string SelectedComboType
        {
            get => this._selectedComboType.ToString();
            set
            {
                this._selectedComboType = (TimeUnit)Enum.Parse(typeof(TimeUnit), value);
                double inputToCalculate = Convert.ToDouble(this.NumOutput);
                List<TimeViewModel> result = TimeCalculator.ConvertAll(inputToCalculate, this._selectedComboType);
                SetResult(result);
            }
        }
        #endregion

        private void InputNumber(string btnNum)
        {
            this.NumOutput += btnNum;
        }

        private void ClearNumber()
        {
            this.NumOutput = " ";
        }

        private void BackspaceNumber()
        {
            int length = this.NumOutput.Length - 1;

            if (0 < length)
            {
                this.NumOutput = this.NumOutput.Substring(0, length);
            }
            else
            {
                this.NumOutput = " ";
            }
        }

        public MainViewModel()
        {
            this.InputCommand = new RelayCommand<string>(InputNumber);
            this.BackspaceCommand = new RelayCommand(BackspaceNumber);
            this.ClearCommand = new RelayCommand(ClearNumber);
        }

        private void SetResult(List<TimeViewModel> resultList)
        {
            var temp = new List<TimeViewModel>();
            foreach (TimeViewModel result in resultList)
            {
                if (result.OuputTimeUnit != this.SelectedComboType && result.CalcValue != 0)
                {
                    temp.Add(result);
                }
            }
            this.Results = temp;
        }
    }
}
