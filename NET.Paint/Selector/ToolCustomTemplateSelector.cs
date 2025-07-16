using System.Windows;
using System.Windows.Controls;
using NET.Paint.Drawing.Constant;

namespace NET.Paint.Selector
{
    public class ToolCustomTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SelectorTemplate { get; set; }
        public DataTemplate PencilTemplate { get; set; }
        public DataTemplate LineTemplate { get; set; }
        public DataTemplate CurveTemplate { get; set; }
        public DataTemplate BezierTemplate { get; set; }
        public DataTemplate EllipseTemplate { get; set; }
        public DataTemplate TriangleTemplate { get; set; }
        public DataTemplate RectangleTemplate { get; set; }
        public DataTemplate PolygonTemplate { get; set; }
        public DataTemplate TextTemplate { get; set; }
        public DataTemplate BitmapTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is XToolType type)
            {
                switch (type)
                {
                    case XToolType.Selector:
                        return SelectorTemplate;
                    case XToolType.Pencil:
                        return PencilTemplate;
                    case XToolType.Line:
                        return LineTemplate;
                    case XToolType.Curve:
                        return CurveTemplate;
                    case XToolType.Bezier:
                        return BezierTemplate;
                    case XToolType.Ellipse:
                        return EllipseTemplate;
                    case XToolType.Triangle:
                        return TriangleTemplate;
                    case XToolType.Rectangle:
                        return RectangleTemplate;
                    case XToolType.Polygon:
                        return PolygonTemplate;
                    case XToolType.Text:
                        return TextTemplate;
                    case XToolType.Bitmap:
                        return BitmapTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
