using CalculatorApp.Models;
using Saige.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CalculatorApp.ViewModels
{
    public class MainViewModel : ViewModel
    {

        #region properties
        public ICommand InputCommand { get; private set; }
        public ICommand BackspaceCommand { get; private set; }
        public ICommand ClearCommand { get; private set; }

        public ObservableCollection<string> ComboItems { get; set; }

        private string numOutput;
        public string NumOutput
        {
            get => numOutput;
            set
            {
                SetProperty(ref numOutput, value);
                CalcHour();
            }
        }

        private List<CalculatorModel> results;
        public List<CalculatorModel> Results
        {
            get => results;
            set => SetProperty(ref results, value);
        }

        private string selectedType = "시간";
        public string SelectedType
        {
            get => selectedType;
            set
            {
                SetProperty(ref selectedType, value);
                if (selectedType.ToString() == "일")
                {
                    CalcDay();
                }
                else if (selectedType.ToString() == "시간")
                {
                    CalcHour();
                }
                else if (selectedType.ToString() == "주")
                {
                    CalcWeek();
                }
                else if (selectedType.ToString() == "년")
                {
                    CalcYear();
                }
                //else if (selectedType.ToString() == "분")
                //{

                //}
            }
        }

        private string day;
        public string Day
        {
            get => day;
            set => SetProperty(ref day, value);
        }

        private string week;
        public string Week
        {
            get => week;
            set => SetProperty(ref week, value);
        }

        private string second;
        public string Second
        {
            get => second;
            set => SetProperty(ref second, value);
        }
        #endregion

        private void InputNumber(string BtnNum)
        {
            NumOutput += BtnNum;
        }

        private void ClearNumber()
        {
            NumOutput = " ";
        }

        private void BackspaceNumber()
        {
            int length = NumOutput.Length - 1;

            if (0 < length)

            {
                NumOutput = NumOutput.Substring(0, length);
            }
            else
            {
                NumOutput = " ";
            }
        }

        public MainViewModel()
        {
            InitControl();
        }

        private void InitControl()
        {
            InputCommand = new RelayCommand<string>(InputNumber);
            BackspaceCommand = new RelayCommand(BackspaceNumber);
            ClearCommand = new RelayCommand(ClearNumber);

            //콤보박스 아이템
            ComboItems = new ObservableCollection<string>
            {
                //"마이크로초",
                //"밀리초",
                //"분",
                "시간",
                "일",
                "주",
                "년"
            };
        }

        private void CalcHour()
        {
            if (NumOutput != null)
            {
                double result;
                double.TryParse(NumOutput, out result);
                Day = string.Concat((Math.Truncate((result / 24) * 1000) / 1000),"d");
                Week = string.Concat((Math.Truncate((result / 168) * 1000) / 1000), "w");
                Second = string.Concat((Math.Truncate((result * 3600) * 1000) / 1000), "s");
            }
            List<CalculatorModel> temp = new List<CalculatorModel>
            {
                new CalculatorModel() { CalcValue = Day },
                new CalculatorModel() { CalcValue = Week },
                new CalculatorModel() { CalcValue = Second }
            };
            Results = temp;
        }

        private void CalcDay()
        {
            if (NumOutput != null)
            {
                double result;
                double.TryParse(NumOutput, out result);
                Day = string.Concat(NumOutput, "d");
                Week = string.Concat((Math.Truncate((result / 7) * 1000) / 1000), "w");
                Second = string.Concat((Math.Truncate((result * 86400) * 1000) / 1000), "s");
            }
            List<CalculatorModel> temp = new List<CalculatorModel>
            {
                new CalculatorModel() { CalcValue = Day },
                new CalculatorModel() { CalcValue = Week },
                new CalculatorModel() { CalcValue = Second }
            };
            Results = temp;
        }

        private void CalcWeek()
        {
            if (NumOutput != null)
            {
                double result;
                double.TryParse(NumOutput, out result);
                Day = string.Concat((Math.Truncate((result * 7) * 1000) / 1000), "d");
                Week = string.Concat(NumOutput, "w");
                Second = string.Concat((Math.Truncate((result * 604800) * 1000) / 1000), "s");
            }
            List<CalculatorModel> temp = new List<CalculatorModel>
            {
                new CalculatorModel() { CalcValue = Day },
                new CalculatorModel() { CalcValue = Week },
                new CalculatorModel() { CalcValue = Second }
            };
            Results = temp;
        }

        private void CalcYear()
        {
            if (NumOutput != null)
            {
                double result;
                double.TryParse(NumOutput, out result);
                Day = string.Concat((Math.Truncate((result * 365) * 1000) / 1000), "d");
                Week = string.Concat((Math.Truncate((result * 52.143) * 1000) / 1000), "w");
                Second = string.Concat((Math.Truncate((result * 3.154) * 1000) / 1000), "s");
            }
            List<CalculatorModel> temp = new List<CalculatorModel>
            {
                new CalculatorModel() { CalcValue = Day },
                new CalculatorModel() { CalcValue = Week },
                new CalculatorModel() { CalcValue = Second }
            };
            Results = temp;
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
