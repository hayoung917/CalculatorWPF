namespace CalculatorApp.Models
{
    using System;
    using System.Collections.Generic;

    public static class TimeCalculator
    {
        public static List<TimeViewModel> ConvertAll(double input, TimeUnit baseUnit)
        {
            Array unitArray = Enum.GetValues(typeof(TimeUnit));
            var resultList = new List<TimeViewModel>();
            foreach (TimeUnit timeUnit in unitArray)
            {
                double calculatedValue = Convert(input, baseUnit, timeUnit);
                calculatedValue = Math.Round(calculatedValue, 3);
                var result = new TimeViewModel() { CalcValue = calculatedValue, OuputTimeUnit = timeUnit.ToString() };
                resultList.Add(result);
            }
            return resultList;
        }
        private static double Convert(double input, TimeUnit baseUnit, TimeUnit targetUnit)
        {
            return input * (GetConversionFactor(targetUnit) / GetConversionFactor(baseUnit));
        }
        private static double GetConversionFactor(TimeUnit targetUnit)
        {
            switch (targetUnit)
            {
                case TimeUnit.마이크로초:
                    return 1;
                case TimeUnit.밀리초:
                    return Math.Pow(10, -3);
                case TimeUnit.초:
                    return Math.Pow(10, -6);
                case TimeUnit.분:
                    return Math.Pow(10, -6) / 60;
                case TimeUnit.시간:
                    return Math.Pow(10, -6) / (Math.Pow(60, 2));
                case TimeUnit.일:
                    return Math.Pow(10, -6) / (Math.Pow(60, 2) * 24);
                case TimeUnit.주:
                    return Math.Pow(10, -6) / (Math.Pow(60, 2) * 24 * 7);
                case TimeUnit.년:
                    return Math.Pow(10, -6) / (Math.Pow(60, 2) * 24 * 365.25);
                default:
                    break;
            }
            return (double)targetUnit;
        }
    }
}
