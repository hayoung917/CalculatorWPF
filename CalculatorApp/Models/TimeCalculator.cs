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
            return targetUnit switch
            {
                TimeUnit.마이크로초 => 1,
                TimeUnit.밀리초 => Math.Pow(10, -3),
                TimeUnit.초 => Math.Pow(10, -6),
                TimeUnit.분 => Math.Pow(10, -6) / 60,
                TimeUnit.시간 => Math.Pow(10, -6) / (Math.Pow(60, 2)),
                TimeUnit.일 => Math.Pow(10, -6) / (Math.Pow(60, 2) * 24),
                TimeUnit.주 => Math.Pow(10, -6) / (Math.Pow(60, 2) * 24 * 7),
                TimeUnit.년 => Math.Pow(10, -6) / (Math.Pow(60, 2) * 24 * 365.25),
                _ => throw new NotImplementedException(),
            };
        }
    }
}
