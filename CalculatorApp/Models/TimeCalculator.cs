namespace CalculatorApp.Models
{
    using System;
    using System.Collections.Generic;

    public static class TimeCalculator
    {
        //todo 2 : 이름변경
        //todo 3 : 타임사용
        public static List<Time> AddCalcList(double input, TimeUnit baseUnit)
        {
            Array unitArray = Enum.GetValues(typeof(TimeUnit));
            var resultList = new List<Time>();
            foreach (TimeUnit timeUnit in unitArray)
            {
                double calculatedValue = Convert(input, baseUnit, timeUnit);
                calculatedValue = Math.Round(calculatedValue, 3);
                var result = new Time() { Value = calculatedValue, Unit = timeUnit };
                resultList.Add(result);
            }
            return resultList;
        }

        private static double Convert(double input, TimeUnit baseUnit, TimeUnit targetUnit)
        {
            return input * (GetFactorToConvertTime(targetUnit) / GetFactorToConvertTime(baseUnit));
        }

        private static double GetFactorToConvertTime(TimeUnit targetUnit)
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
                    return (double)targetUnit;
            }
        }
    }
}
