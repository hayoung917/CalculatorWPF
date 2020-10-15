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
                case TimeUnit.Microsecond:
                    return 1;
                case TimeUnit.Millisecond:
                    return Math.Pow(10, -3);
                case TimeUnit.Second:
                    return Math.Pow(10, -6);
                case TimeUnit.Minute:
                    return Math.Pow(10, -6) / 60;
                case TimeUnit.Hour:
                    return Math.Pow(10, -6) / (Math.Pow(60, 2));
                case TimeUnit.Day:
                    return Math.Pow(10, -6) / (Math.Pow(60, 2) * 24);
                case TimeUnit.Week:
                    return Math.Pow(10, -6) / (Math.Pow(60, 2) * 24 * 7);
                case TimeUnit.Year:
                    return Math.Pow(10, -6) / (Math.Pow(60, 2) * 24 * 365.25);
                default:
                    break;
            }
            return (double)targetUnit;
        }
    }
}
