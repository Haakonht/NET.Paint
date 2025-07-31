using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Structure;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace NET.Paint.View.Component.Drawing.Converters
{
    public class ThumbVisibleConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value[0] is Point point)
                if (value[1] is XRenderable renderable)
                    if (renderable.Type == XToolType.Ellipse || (renderable.Type == XToolType.Rectangle))
                        if (renderable.Points.Count > 0 && renderable.Points.First().Equals(point))
                            return Visibility.Collapsed;

            return Visibility.Visible;
        }

        object[] IMultiValueConverter.ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
