using NET.Paint.Drawing.Model.Utility;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NET.Paint.View.Component.Drawing.Converters
{
    public class XColorToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is XColor xColor)
                return xColor.ToBrush();

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Converting back from Brush to XColor is complex and context-dependent
            // This would require additional information about gradient points, etc.
            throw new NotImplementedException("Converting from Brush to XColor is not supported.");
        }
    }
}
