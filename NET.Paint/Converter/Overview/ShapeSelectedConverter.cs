using NET.Paint.Drawing.Model.Structure;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace NET.Paint.Converter
{
    public class ShapeSelectedConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // values[0] = current shape
            // values[1] = selected collection from XImage.Selected
            
            if (values == null || values.Length < 2)
                return false;
            
            if (values[0] == null)
                return false;

            // Handle case where values[1] is the Selected collection directly
            if (values[1] is IEnumerable<object> collection)
            {
                foreach(var item in collection)
                {
                    if (ReferenceEquals(item, values[0]))
                        return true;
                }
            }

            return false;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}