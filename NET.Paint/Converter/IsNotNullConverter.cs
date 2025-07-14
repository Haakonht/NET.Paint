using NET.Paint.Drawing.Model.Structure;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace NET.Paint.Converter
{
    public class IsNotNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<XLayer> layers)
                return layers.Count > 1;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
