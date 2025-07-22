using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NET.Paint.View.Component.Tools.Converters
{
    public class CircularSliderThumbPositionConverter : IMultiValueConverter
    {
        public static readonly CircularSliderThumbPositionConverter Instance = new CircularSliderThumbPositionConverter();

        public static readonly CircularSliderThumbPositionConverter X = new CircularSliderThumbPositionConverter { IsXCoordinate = true };
        public static readonly CircularSliderThumbPositionConverter Y = new CircularSliderThumbPositionConverter { IsXCoordinate = false };
        public bool IsXCoordinate { get; set; }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length == 3 &&
                values[0] is double value &&
                values[1] is double maximum &&
                values[2] is double minimum &&
                parameter is string radiusParam &&
                double.TryParse(radiusParam, out double radius))
            {
                double range = maximum - minimum;
                double normalizedValue = (value - minimum) / range;
                double angleInRadians = normalizedValue * 2 * Math.PI - Math.PI / 2; // Start at top (12 o'clock)

                double center = 50; // Center of 100x100 circle
                double thumbOffset = 8; // Half of thumb size (16/2)

                if (IsXCoordinate)
                    return center + radius * Math.Cos(angleInRadians) - thumbOffset;
                else
                    return center + radius * Math.Sin(angleInRadians) - thumbOffset;
            }
            return 42.0; // Default position
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
