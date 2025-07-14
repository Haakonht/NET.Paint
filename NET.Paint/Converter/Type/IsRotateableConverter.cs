using NET.Paint.Drawing.Interface;
using System.Globalization;
using System.Windows.Data;

namespace NET.Paint.Converter
{
    public class IsRotateableConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
                return value is IRotateable;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}