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
            if (value is RenderableViewModel renderable)
            {
                switch (renderable)
                {
                    case PolylineViewModel:
                        return "Pencil";
                    case LineViewModel:
                        return "Line";
                    case BezierViewModel:
                        return "Bezier";
                    case CurveViewModel:
                        return "Curve";
                    case CircleViewModel:
                        return "Circle";
                    case EllipseViewModel:
                        return "Ellipse";
                    case TriangleViewModel:
                        return "Triangle";
                    case SquareViewModel:
                        return "Square";
                    case RectangleViewModel:
                        return "Rectangle";
                    case RegularPolygonViewModel regular:
                        switch (regular.Corners)
                        {
                            case 5: return "Pentagon";
                            case 6: return "Hexagon";
                            case 7: return "Heptagon";
                            case 8: return "Octagon";
                        } break;
                    case HeartViewModel:
                        return "Heart";
                    case StarViewModel:
                        return "Star";
                    case ArrowViewModel:
                        return "Arrow";
                    case CloudVIewModel:
                        return "Cloud";
                    case SpiralViewModel:
                        return "Spiral";
                    case TextViewModel:
                        return "Text";
                    case BitmapViewModel:
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
