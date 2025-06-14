using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Shape;
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
                    case ToolType.Line:
                        return renderable as XLine;
                    case ToolType.Rectangle:
                        return renderable as XRectangle;
                    case ToolType.Ellipse:
                        return renderable as XEllipse;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
