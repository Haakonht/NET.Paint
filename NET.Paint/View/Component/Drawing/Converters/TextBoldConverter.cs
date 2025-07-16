using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace NET.Paint.View.Component.Drawing.Converters
{
    public class TextBoldConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool isBold && isBold ? FontWeights.Bold : FontWeights.Normal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
