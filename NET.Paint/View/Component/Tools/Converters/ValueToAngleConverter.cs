using System.Globalization;
using System.Windows.Data;

namespace NET.Paint.View.Component.Tools.Converters
{
    public class ValueToAngleConverter : IMultiValueConverter
    {
        public static readonly ValueToAngleConverter Instance = new ValueToAngleConverter();

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 &&
                values[0] is double value &&
                values[1] is double maximum &&
                values[2] is double minimum)
            {
                double range = maximum - minimum;
                double normalizedValue = (value - minimum) / range;
                return normalizedValue * 360.0;
            }
            return 0.0;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
