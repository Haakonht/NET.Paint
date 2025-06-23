using NET.Paint.Drawing.Constant;
using System.Globalization;
using System.Windows.Media;

namespace NET.Paint.Converter
{
    public class ImageScalingToStretchConverter
    {
        public object? Convert(object value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is ImageScaling scaling)
            {
                return scaling switch
                {
                    ImageScaling.Original => Stretch.None,
                    ImageScaling.Fit => Stretch.Uniform,
                    ImageScaling.Clip => Stretch.Fill,
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
                    Stretch.None => ImageScaling.Original,
                    Stretch.Uniform => ImageScaling.Fit,
                    Stretch.Fill => ImageScaling.Clip,
                    _ => ImageScaling.Original
                };
            }
            return ImageScaling.Original;
        }
    }
}
