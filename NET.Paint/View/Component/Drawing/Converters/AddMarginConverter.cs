using System.Globalization;
using System.Windows.Data;

namespace NET.Paint.View.Component.Drawing.Converters
{
    public class AddMarginConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double originalValue && parameter is string marginString)
            {
                if (double.TryParse(marginString, out double margin))
                {
                    return originalValue + margin;
                }
                return originalValue;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
