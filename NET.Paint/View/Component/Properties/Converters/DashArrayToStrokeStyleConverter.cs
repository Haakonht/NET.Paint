using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Utility;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace NET.Paint.View.Component.Properties.Converters
{
    public class DashArrayToStrokeStyleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DoubleCollection dashArray)
            {
                // Find matching stroke style based on DashArray
                return XOptions.StrokeStyleOptions.FirstOrDefault(style =>
                    AreDashArraysEqual(style.DashArray, dashArray)) ?? XOptions.StrokeStyleOptions[0];
            }
            return XOptions.StrokeStyleOptions[0]; // Default to first (Solid)
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is XStrokeStyle strokeStyle)
            {
                return strokeStyle.DashArray;
            }
            return null;
        }

        private bool AreDashArraysEqual(DoubleCollection dash1, DoubleCollection dash2)
        {
            // Handle null cases
            if (dash1 == null && dash2 == null) return true;
            if (dash1 == null || dash2 == null) return false;

            // Compare counts
            if (dash1.Count != dash2.Count) return false;

            // Compare values
            for (int i = 0; i < dash1.Count; i++)
            {
                if (Math.Abs(dash1[i] - dash2[i]) > 0.001) // Small tolerance for floating point comparison
                    return false;
            }

            return true;
        }
    }
}