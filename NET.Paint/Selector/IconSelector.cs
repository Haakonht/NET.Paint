using System.Windows;
using System.Windows.Controls;
using NET.Paint.Drawing.Constant;

namespace NET.Paint.Selector
{
    public class IconSelector : DataTemplateSelector
    {
        public DataTemplate PointerTemplate { get; set; }
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
            if (item is ToolType type)
            {
                switch (type)
                {
                    case ToolType.Pointer:
                        return PointerTemplate;
                    case ToolType.Selector:
                        return SelectorTemplate;
                    case ToolType.Pencil:
                        return PencilTemplate;
                    case ToolType.Line:
                        return LineTemplate;
                    case ToolType.Curve:
                        return CurveTemplate;
                    case ToolType.Bezier:
                        return BezierTemplate;
                    case ToolType.Ellipse:
                        return EllipseTemplate;
                    case ToolType.Triangle:
                        return TriangleTemplate;
                    case ToolType.Rectangle:
                        return RectangleTemplate;
                    case ToolType.Polygon:
                        return PolygonTemplate;
                    case ToolType.Text:
                        return TextTemplate;
                    case ToolType.Bitmap:
                        return BitmapTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}
