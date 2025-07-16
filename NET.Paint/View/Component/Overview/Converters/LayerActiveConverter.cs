using System.Globalization;
using System.Windows.Data;

namespace NET.Paint.View.Component.Overview.Converters
{
    public class LayerActiveConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // values[0] = current layer
            // values[1] = parent image's ActiveLayer
            // values[2] = parent image
            // values[3] = global ActiveImage
            return values.Length == 4
                && ReferenceEquals(values[0], values[1])
                && ReferenceEquals(values[2], values[3]);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}