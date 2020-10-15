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

        private TimeUnit _selectedComboType;
        public TimeUnit SelectedComboType
        {
            get => this._selectedComboType;
            set
            {
                SetProperty(ref this._selectedComboType, value);
                //this._selectedComboType = (TimeUnit)Enum.Parse(typeof(TimeUnit), value);
                double inputCalculate = Convert.ToDouble(this.NumOutput);
                List<TimeViewModel> result = TimeCalculator.ConvertAll(inputCalculate, this._selectedComboType);
                SetResult(result);
            }
        }

        private string _numOutput;
        public string NumOutput
        {
            get => this._numOutput;
            set
            {
                SetProperty(ref this._numOutput, value);
                double inputCalculate = Convert.ToDouble(this.NumOutput);
                List<TimeViewModel> result = TimeCalculator.ConvertAll(inputCalculate, this._selectedComboType);
                SetResult(result);
            }
        }

        private List<TimeViewModel> _results;
        public List<TimeViewModel> Results
        {
            get => this._results;
            set => SetProperty(ref this._results, value);
        }

        #endregion
        public MainViewModel()
        {
            this.InputCommand = new RelayCommand<string>(InputNumber);
            this.BackspaceCommand = new RelayCommand(BackspaceNumber);
            this.ClearCommand = new RelayCommand(ClearNumber);
        }

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

        private void SetResult(List<TimeViewModel> resultList)
        {
            var temp = new List<TimeViewModel>();
            foreach (TimeViewModel result in resultList)
            {
                //if (result.OuputTimeUnit != this.SelectedComboType && result.CalcValue != 0)
                {
                    temp.Add(result);
                }
            }
            this.Results = temp;
        }
    }
}
