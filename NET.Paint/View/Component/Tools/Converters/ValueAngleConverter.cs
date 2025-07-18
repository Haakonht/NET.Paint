using NET.Paint.Drawing.Helper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NET.Paint.View.Component.Tools.Converters
{
    public class ValueAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double val)
            {
                // Convert value (0-1) to angle along semicircle (π to 0)
                double angle = Math.PI - (val * Math.PI);
                
                // Convert to degrees and subtract 90 degrees so triangle points toward center
                double degrees = (angle * 180.0 / Math.PI) - 90;
                
                return degrees;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double angle)
            {
                // Convert angle back to value (0-1)
                double radians = (angle + 90) * Math.PI / 180.0;
                return (Math.PI - radians) / Math.PI;
            }
            return 0;
        }
    }

    public class ValueToRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double val)
            {
                double radius = 50; // Radius of the semicircle
                double centerX = 75; // Center X position in the 150-wide grid
                double centerY = 75; // Center Y position in the 150-high grid
                
                // Convert value (0-1) to angle (π to 0 for upward semicircle)
                double angle = Math.PI - (val * Math.PI);
                
                if (parameter?.ToString() == "X")
                {
                    return centerX + radius * Math.Cos(angle);
                }
                else if (parameter?.ToString() == "Y")
                {
                    return centerY + radius * Math.Sin(angle);
                }
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ValueToCanvasPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double val)
            {
                double radius = 50; // Radius of the semicircle
                double centerX = 75; // Center X position in the 150-wide grid
                double centerY = 75; // Center Y position in the 150-high grid
                
                // Convert value (0-1) to angle (π to 0 for upward semicircle)
                double angle = Math.PI - (val * Math.PI);
                
                if (parameter?.ToString() == "X")
                {
                    return centerX + radius * Math.Cos(angle) - 5; // -5 to center the 10-wide thumb
                }
                else if (parameter?.ToString() == "Y")
                {
                    return centerY + radius * Math.Sin(angle) - 12.5; // -12.5 to center the 25-high thumb
                }
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
