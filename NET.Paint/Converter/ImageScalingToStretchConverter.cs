using NET.Paint.Drawing.Constant;
using System.Globalization;
using System.Windows.Media;

namespace NET.Paint.Converter
{
    public class ImageScalingToStretchConverter
    {
        public object? Convert(object value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is XScalingMode scaling)
            {
                return scaling switch
                {
                    XScalingMode.Original => Stretch.None,
                    XScalingMode.Fit => Stretch.Uniform,
                    XScalingMode.Clip => Stretch.Fill,
                    _ => Stretch.None
                };
            }
            return Stretch.None;
        }

        public object? ConvertBack(object value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Stretch stretch)
            {
                return stretch switch
                {
                    Stretch.None => XScalingMode.Original,
                    Stretch.Uniform => XScalingMode.Fit,
                    Stretch.Fill => XScalingMode.Clip,
                    _ => XScalingMode.Original
                };
            }
            return XScalingMode.Original;
        }
    }
}
