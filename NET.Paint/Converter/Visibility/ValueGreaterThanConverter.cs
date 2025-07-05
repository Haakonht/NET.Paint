using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NET.Paint.Converter
{
    public class ValueGreaterThanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return Visibility.Collapsed;

            if (double.TryParse(value.ToString(), out double val) &&
                double.TryParse(parameter.ToString(), out double param))
            {
                if (val >= param)
                    return Visibility.Visible;

                return Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not typically used for this type of comparison converter
            throw new NotImplementedException();
        }
    }
}
