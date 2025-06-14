using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NET.Paint.Converter
{
    public class InverseScaleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double zoom && zoom != 0)
            {
                return new ScaleTransform(1.0 / zoom, 1.0 / zoom);
            }
            return new ScaleTransform(1, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
