namespace CalculatorApp.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Controls;
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
                CalcHour();
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
                if (this._selectedComboType.ToString() == "시간")
                {
                    CalcHour();
                }
                else if (this._selectedComboType.ToString() == "일")
                {
                    CalcDay();
                }
                else if (this._selectedComboType.ToString() == "주")
                {
                    CalcWeek();
                }
                else if (this._selectedComboType.ToString() == "년")
                {
                    CalcYear();
                }
            }
        }

        private string _day;
        public string Day
        {
            get => this._day;
            set => SetProperty(ref this._day, value);
        }

        private string _week;
        public string Week
        {
            get => this._week;
            set => SetProperty(ref this._week, value);
        }

        private string _second;
        public string Second
        {
            get => this._second;
            set => SetProperty(ref this._second, value);
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
            InitControl();
        }

        private void InitControl()
        {
            this.InputCommand = new RelayCommand<string>(InputNumber);
            this.BackspaceCommand = new RelayCommand(BackspaceNumber);
            this.ClearCommand = new RelayCommand(ClearNumber);
        }


        private void CalcHour()
        {
            if (this.NumOutput != null)
            {
                _ = double.TryParse(this.NumOutput, out double result);
                this.Day = string.Concat((Math.Truncate((result / 24) * 1000) / 1000),"d");
                this.Week = string.Concat((Math.Truncate((result / 168) * 1000) / 1000), "w");
                this.Second = string.Concat((Math.Truncate((result * 3600) * 1000) / 1000), "s");
            }
            var temp = new List<TimeViewModel>
            {
                new TimeViewModel() { CalcValue = Day },
                new TimeViewModel() { CalcValue = Week },
                new TimeViewModel() { CalcValue = Second }
            };
            this.Results = temp;
        }

        private void CalcDay()
        {
            if (this.NumOutput != null)
            {
                double result;
                double.TryParse(this.NumOutput, out result);
                this.Day = string.Concat(this.NumOutput, "d");
                this.Week = string.Concat((Math.Truncate((result / 7) * 1000) / 1000), "w");
                this.Second = string.Concat((Math.Truncate((result * 86400) * 1000) / 1000), "s");
            }
            List<TimeViewModel> temp = new List<TimeViewModel>
            {
                new TimeViewModel() { CalcValue = Day },
                new TimeViewModel() { CalcValue = Week },
                new TimeViewModel() { CalcValue = Second }
            };
            this.Results = temp;
        }

        private void CalcWeek()
        {
            if (this.NumOutput != null)
            {
                double result;
                double.TryParse(this.NumOutput, out result);
                this.Day = string.Concat((Math.Truncate((result * 7) * 1000) / 1000), "d");
                this.Week = string.Concat(this.NumOutput, "w");
                this.Second = string.Concat((Math.Truncate((result * 604800) * 1000) / 1000), "s");
            }
            List<TimeViewModel> temp = new List<TimeViewModel>
            {
                new TimeViewModel() { CalcValue = Day },
                new TimeViewModel() { CalcValue = Week },
                new TimeViewModel() { CalcValue = Second }
            };
            this.Results = temp;
        }

        private void CalcYear()
        {
            if (this.NumOutput != null)
            {
                double result;
                double.TryParse(this.NumOutput, out result);
                this.Day = string.Concat((Math.Truncate((result * 365) * 1000) / 1000), "d");
                this.Week = string.Concat((Math.Truncate((result * 52.143) * 1000) / 1000), "w");
                this.Second = string.Concat((Math.Truncate((result * 3.154) * 1000) / 1000), "s");
            }
            List<TimeViewModel> temp = new List<TimeViewModel>
            {
                new TimeViewModel() { CalcValue = Day },
                new TimeViewModel() { CalcValue = Week },
                new TimeViewModel() { CalcValue = Second }
            };
            this.Results = temp;
        }

        //private void CalcMinute()
        //{
        //    if (NumOutput != null)
        //    {
        //        double result;
        //        double.TryParse(NumOutput, out result);
        //        Day = string.Concat((Math.Truncate((result / 1440) * 10000) / 10000), "d");
        //        Week = string.Concat((Math.Truncate((result * 52.143) * 1000) / 1000), "w");
        //        Second = string.Concat((Math.Truncate((result * 3.154) * 1000) / 1000), "s");
        //    }
        //}
    }
}
