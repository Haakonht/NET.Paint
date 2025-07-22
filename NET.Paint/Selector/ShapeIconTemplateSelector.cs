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
        public DataTemplate RectangleTemplate { get; set; }
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
        public DataTemplate CloudTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is PolylineViewModel)
                return PencilTemplate;
            if (item is LineViewModel)
                return LineTemplate;
            if (item is BezierViewModel)
                return BezierTemplate;
            if (item is CurveViewModel)
                return CurveTemplate;
            if (item is CircleViewModel)
                return CircleTemplate;
            if (item is EllipseViewModel)
                return EllipseTemplate;
            if (item is TriangleViewModel)
                return TriangleTemplate;
            if (item is SquareViewModel square)
                    return SquareTemplate;
            if (item is RectangleViewModel rectangle)
                    return RectangleTemplate;
            if (item is RegularPolygonViewModel regular)
            {
                if (regular.Corners == 7)
                    return HeptagonTemplate;
                else if (regular.Corners == 6)
                    return HexagonTemplate;
                else if (regular.Corners == 8)
                    return OctagonTemplate;
                else
                    return PentagonTemplate;
            }
            if (item is StarViewModel)
                return StarTemplate;
            if (item is TextViewModel)
                return TextTemplate;
            if (item is BitmapViewModel)
                return BitmapTemplate;
            if (item is ArrowViewModel)
                return ArrowTemplate;
            if (item is HeartViewModel)
                return HeartTemplate;
            if (item is SpiralViewModel)
                return SpiralTemplate;
            if (item is CloudVIewModel)
                return CloudTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
