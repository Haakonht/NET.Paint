using NET.Paint.Drawing.Model.Structure;
using System;
using System.Globalization;
using System.Windows.Data;

namespace NET.Paint.Converter
{
    public class ShapeSelectedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // values[0] = current shape
            // values[1] = selected shape
            return values[0] == values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}