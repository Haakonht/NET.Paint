using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace NET.Paint.Converter
{
    public class EnumToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value?.Equals(Enum.Parse(value.GetType(), parameter?.ToString())) == true)
                return Visibility.Visible;
                    
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
