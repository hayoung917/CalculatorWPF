namespace CalculatorApp.Converters
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Windows.Data;

    [SuppressMessage(null, "CA1812", Justification ="감지되지 않는 XAML에서 사용합니다.")]
    public class IsWidthLessThanHeightConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        => values switch
        {
            null => false,
            var v when IsIntegerType(v) == false => false,
            var v when v.Length != 2 => throw new ArgumentException("length of values must be 2"),
            var v => int.Parse(v[0].ToString()) < int.Parse(v[1].ToString()),
        };

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static bool IsIntegerType(params object[] values)
        {
            foreach (object value in values)
            {
                if (int.TryParse(value.ToString(), out _) == false)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
