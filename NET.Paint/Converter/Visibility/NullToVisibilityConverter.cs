using System.Globalization;
using System.Windows.Data;
using System.Windows;
using NET.Paint.Drawing.Model.Structure;

namespace NET.Paint.Converter
{
    public class NullToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string param && param == "Invert")
            {
                if (value != null && value is XRenderable)
                    return Visibility.Collapsed;

                if (value != null && value is string valStr)
                    if (!string.IsNullOrEmpty(valStr))
                        return Visibility.Collapsed;

                return Visibility.Visible;
            }

            if (value != null && value is XRenderable)
                return Visibility.Visible;
                
            if (value != null && value is string str)
                if (!string.IsNullOrEmpty(str))
                    return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
