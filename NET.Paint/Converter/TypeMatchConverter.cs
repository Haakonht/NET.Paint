using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NET.Paint.Converter
{
    public class TypeMatchConverter : IValueConverter
    {
        public Type TypeToMatch { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null && TypeToMatch.IsInstanceOfType(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
