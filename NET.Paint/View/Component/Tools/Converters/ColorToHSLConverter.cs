using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NET.Paint.View.Component.Tools.Converters
{
    class ColorToHSLConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color color)
            {
                System.Drawing.Color converted = System.Drawing.Color.FromArgb(color.A, color.R, color.G, color.B);
                float hue = converted.GetHue();
                float saturation = converted.GetSaturation();
                float lightness = converted.GetBrightness();

                if (parameter is string param)
                {
                    if  (param == "H")
                        return hue;
                    else if (param == "S")
                        return saturation;
                    else if (param == "L")
                        return lightness;
                }
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("ConvertBack is not implemented for ColorToHSLConverter.");
        }
    }
}
