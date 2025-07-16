using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using System.Globalization;
using System.Windows.Data;

namespace NET.Paint.View.Component.Overview.Converters
{
    public class ShapeNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is XRenderable renderable)
            {
                switch (renderable)
                {
                    case XPencil:
                        return "Pencil";
                    case XLine:
                        return "Line";
                    case XBezier:
                        return "Bezier";
                    case XCurve:
                        return "Curve";
                    case XCircle:
                        return "Circle";
                    case XEllipse:
                        return "Ellipse";
                    case XTriangle:
                        return "Triangle";
                    case XSquare:
                        return "Square";
                    case XRectangle:
                        return "Rectangle";
                    case XRegular regular:
                        switch (regular.Corners)
                        {
                            case 5: return "Pentagon";
                            case 6: return "Hexagon";
                            case 7: return "Heptagon";
                            case 8: return "Octagon";
                        } break;
                    case XHeart:
                        return "Heart";
                    case XStar:
                        return "Star";
                    case XArrow:
                        return "Arrow";
                    case XCloud:
                        return "Cloud";
                    case XSpiral:
                        return "Spiral";
                    case XText:
                        return "Text";
                    case XBitmap:
                        return "Bitmap";
                }
            }

            return "Unknown Shape";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
