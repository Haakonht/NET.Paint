using NET.Paint.Drawing.Model.Structure;
using System.Globalization;
using System.Windows.Data;

namespace NET.Paint.Converter
{
    public class AnchorableEnabledConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is XProject project)
                if (project.Images.Count > 0)
                    return true;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new NotImplementedException();
        }
    }
}
