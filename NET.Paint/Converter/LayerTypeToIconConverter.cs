using System.Globalization;
using System.Windows.Data;

namespace NET.Paint.Converter
{
    public class LayerTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "LayerGroup";
            var type = value.GetType().Name;
            return type switch
            {
                "XVectorLayer" => "LayerGroup",
                "XRasterLayer" => "Image",
                _ => "LayerGroup"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
