using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace NET.Paint.View.Component.Drawing.Converters
{
    public class GridViewportConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new Rect(0d, 0d, (int)values[0], (int)values[1]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
