using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NET.Paint.Converter
{
    public class ContrastColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                double luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;

                return luminance > 0.5 ? Brushes.Black : Brushes.White;
            }

            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}