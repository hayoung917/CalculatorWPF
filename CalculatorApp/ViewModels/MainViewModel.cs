namespace CalculatorApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;
    using CalculatorApp.Models;
    using Saige.MVVM;

    //todo 5: 추상화 다시하기
    public class MainViewModel : ViewModel
    {
        #region properties
        public ICommand InputCommand { get; private set; }
        public ICommand BackspaceCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }

        public TimeUnit[] Units { get; }

        private TimeUnit _selectedComboType;
        public TimeUnit SelectedComboType
        {
            get => this._selectedComboType;
            set => SetProperty(ref this._selectedComboType, value); //todo 4 : set없애기
        }

        private string _numOutput;
        public string NumOutput
        {
            get => this._numOutput;
            set => SetProperty(ref this._numOutput, value);
        }

        private IReadOnlyList<TimeViewModel> _timeCoverted;
        public IReadOnlyList<TimeViewModel> Results
        {
            get => this._timeCoverted;
            set => SetProperty(ref this._timeCoverted, value);
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
            if(btnNum.Equals("."))
            {
                if (!this.NumOutput.ToString().Contains("."))
                {
                    this.NumOutput += btnNum;
                }
            }
            else
            {
                this.NumOutput += btnNum;
            }
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
                if (result.Unit.ToString() != this.SelectedComboType.ToString() && result.Value != 0)
                {
                    temp.Add(result);
                }
            }
            this.Results = temp;
        }

        protected override void OnPropertyChanged(object oldValue, object newValue, [CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(oldValue, newValue, propertyName);
            switch (propertyName)
            {
                case nameof(this.SelectedComboType):
                    OnSelectedComboTypeChanged(oldValue, newValue);
                    break;
                case nameof(this.NumOutput):
                    OnSelectedComboTypeChanged(oldValue, newValue);
                    break;
                default:
                    break;
            }
        }

        protected virtual void OnSelectedComboTypeChanged(object oldValue, object newValue)
        {
            _ = double.TryParse(this.NumOutput, out double inputCalculate);
            List<TimeViewModel> result = TimeViewModel.ModelToViewModel(inputCalculate, this._selectedComboType);
            SetResult(result);
        }
    }
}
