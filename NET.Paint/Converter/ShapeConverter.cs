using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using System.Globalization;
using System.Windows.Data;

namespace NET.Paint.Converter
{
    public class ShapeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is XRenderable renderable)
            {
                switch (renderable.Type)
                {
                    case XToolType.Line:
                        return renderable as XLine;
                    case XToolType.Rectangle:
                        return renderable as XRectangle;
                    case XToolType.Ellipse:
                        return renderable as XEllipse;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
