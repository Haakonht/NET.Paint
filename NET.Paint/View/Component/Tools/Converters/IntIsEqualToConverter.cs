using System.Globalization;
using System.Windows.Data;

namespace NET.Paint.View.Component.Tools.Converters
{
    public class IntIsEqualToConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
                return false;

            if (double.TryParse(value.ToString(), out double val) &&
                double.TryParse(parameter.ToString(), out double param))
            {
                if (val == param)
                    return true;
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not typically used for this type of comparison converter
            throw new NotImplementedException();
        }
    }
}
