using System;
using System.Globalization;
using System.Windows.Data;

namespace CalculatorApp.Converters
{
    class IsWidthLessThanHeightConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        => values switch
        {
            null => false,
            var v when IsIntegerType(v) == false => false,
            var v when v.Length != 2 => throw new ArgumentException("length of values must be 2"),
            var v => int.Parse(v[0].ToString()) < int.Parse(v[1].ToString()),
        };
        //{
        //    if (values == null)
        //        return false;

        //    if (values.Length != 2)
        //        throw new ArgumentException("length of values must be 2");

        //    if (int.TryParse(values[0].ToString(), out int width) == false)
        //        return false;

        //    if (int.TryParse(values[1].ToString(), out int height) == false)
        //        return false;

        //    return width < height;
        //}

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private bool IsIntegerType(params object[] values)
        {
            foreach (var value in values)
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

