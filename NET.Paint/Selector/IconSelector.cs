using System.Windows;
using System.Windows.Controls;
using NET.Paint.Drawing.Constant;

namespace NET.Paint.Selector
{
    public class IconSelector : DataTemplateSelector
    {
        public DataTemplate SelectorTemplate { get; set; }
        public DataTemplate PencilTemplate { get; set; }
        public DataTemplate EraserTemplate { get; set; }
        public DataTemplate LineTemplate { get; set; }
        public DataTemplate BezierTemplate { get; set; }
        public DataTemplate CircleTemplate { get; set; }
        public DataTemplate EllipseTemplate { get; set; }
        public DataTemplate TriangleTemplate { get; set; }
        public DataTemplate RectangleTemplate { get; set; }
        public DataTemplate RoundedRectangleTemplate { get; set; }
        public DataTemplate PentagonTemplate { get; set; }
        public DataTemplate HexagonTemplate { get; set; }
        public DataTemplate OctagonTemplate { get; set; }
        public DataTemplate TextTemplate { get; set; }
        public DataTemplate BitmapTemplate { get; set; }
        public DataTemplate StarTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ToolType type)
            {
                switch (type)
                {
                    case ToolType.Selector:
                        return SelectorTemplate;
                    case ToolType.Eraser:
                        return EraserTemplate;
                    case ToolType.Pencil:
                        return PencilTemplate;
                    case ToolType.Line:
                        return LineTemplate;
                    case ToolType.Bezier:
                        return BezierTemplate;
                    case ToolType.Circle:
                        return CircleTemplate;
                    case ToolType.Ellipse:
                        return EllipseTemplate;
                    case ToolType.Triangle:
                        return TriangleTemplate;
                    case ToolType.Rectangle:
                        return RectangleTemplate;
                    case ToolType.RoundedRectangle:
                        return RoundedRectangleTemplate;
                    case ToolType.Pentagon:
                        return PentagonTemplate;
                    case ToolType.Hexagon:
                        return HexagonTemplate;
                    case ToolType.Octagon:
                        return OctagonTemplate;
                    case ToolType.Star:
                        return StarTemplate;
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
