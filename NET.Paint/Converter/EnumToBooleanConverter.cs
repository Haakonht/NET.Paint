using System.Globalization;
using System.Windows.Data;

namespace NET.Paint.Converter
{
    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.Equals(Enum.Parse(value.GetType(), parameter?.ToString())) == true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b && b)
                return Enum.Parse(targetType, parameter?.ToString());
            return Binding.DoNothing;
        }
    }
}
