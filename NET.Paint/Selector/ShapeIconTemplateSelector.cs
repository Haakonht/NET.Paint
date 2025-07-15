using System.Windows;
using System.Windows.Controls;
using NET.Paint.Drawing.Model.Shape;

namespace NET.Paint.Selector
{
    public class ShapeIconTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PencilTemplate { get; set; }
        public DataTemplate LineTemplate { get; set; }
        public DataTemplate CurveTemplate { get; set; }
        public DataTemplate BezierTemplate { get; set; }
        public DataTemplate CircleTemplate { get; set; }
        public DataTemplate EllipseTemplate { get; set; }
        public DataTemplate TriangleTemplate { get; set; }
        public DataTemplate SquareTemplate { get; set; }
        public DataTemplate RoundedSquareTemplate { get; set; }
        public DataTemplate RectangleTemplate { get; set; }
        public DataTemplate RoundedRectangleTemplate { get; set; }
        public DataTemplate PentagonTemplate { get; set; }
        public DataTemplate HeptagonTemplate { get; set; }
        public DataTemplate HexagonTemplate { get; set; }
        public DataTemplate OctagonTemplate { get; set; }
        public DataTemplate TextTemplate { get; set; }
        public DataTemplate BitmapTemplate { get; set; }
        public DataTemplate StarTemplate { get; set; }
        public DataTemplate ArrowTemplate { get; set; }
        public DataTemplate HeartTemplate { get; set; }
        public DataTemplate SpiralTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is XPencil)
                return PencilTemplate;
            if (item is XLine)
                return LineTemplate;
            if (item is XCurve)
                return CurveTemplate;
            if (item is XBezier)
                return BezierTemplate;
            if (item is XCircle)
                return CircleTemplate;
            if (item is XEllipse)
                return EllipseTemplate;
            if (item is XTriangle)
                return TriangleTemplate;
            if (item is XSquare square)
                if (square.Radius > 0)
                    return RoundedSquareTemplate;
                else
                    return SquareTemplate;
            if (item is XRectangle rectangle)
                if (rectangle.Radius > 0)
                    return RoundedRectangleTemplate;
                else
                    return RectangleTemplate;
            if (item is XRegular regular)
                if (regular.Corners == 7)
                    return HeptagonTemplate;
                else if (regular.Corners == 6)
                    return HexagonTemplate;
                else if (regular.Corners == 8)
                    return OctagonTemplate;
                else
                    return PentagonTemplate;
            if (item is XStar)
                return StarTemplate;
            if (item is XText)
                return TextTemplate;
            if (item is XBitmap)
                return BitmapTemplate;
            if (item is XArrow)
                return ArrowTemplate;
            if (item is XHeart)
                return HeartTemplate;
            if (item is XSpiral)
                return SpiralTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
