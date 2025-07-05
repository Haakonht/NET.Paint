using NET.Paint.Drawing.Constant;
using System.Windows;
using System.Windows.Data;

namespace NET.Paint.Converter
{
    public class ToolTypeVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is ToolType val)
                if (parameter is ToolType param)
                    if (param == val)
                        return Visibility.Visible;

            return Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
