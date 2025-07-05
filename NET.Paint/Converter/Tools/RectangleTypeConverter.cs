using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NET.Paint.Converter
{
    public class RectangleTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string str)
            {
                if (value is double dbl && dbl == 0)
                {
                    if (str.Equals("Inverted"))
                        return false;

                    return true;
                }
                else
                {
                    if (str.Equals("Inverted"))
                        return true;

                    return false;
                }
            }

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter is string str)
            {
                if (value is true)
                {
                    if (str.Equals("Inverted"))
                        return 1;

                    return 0;
                }
                else
                {
                    if (str.Equals("Inverted"))
                        return 0;

                    return 1;
                }
            }

            return 0;
        }
    }
}
