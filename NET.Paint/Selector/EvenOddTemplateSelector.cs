using NET.Paint.Drawing.Constant;
using System.Windows;
using System.Windows.Controls;

namespace NET.Paint.Selector
{
    public class EvenOddTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EvenTemplate { get; set; }
        public DataTemplate OddTemplate { get; set; }
        public DataTemplate EmptyTemplate { get; set; } = new DataTemplate();

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is XRectangleStyle rectangleStyle)
                switch (rectangleStyle)
                {
                    case XRectangleStyle.Square:
                        return EvenTemplate;
                    case XRectangleStyle.Rectangle:
                        return OddTemplate;
                }
            else if (item is XEllipseStyle ellipseStyle)
                switch (ellipseStyle)
                {
                    case XEllipseStyle.Circle:
                        return EvenTemplate;
                    case XEllipseStyle.Ellipse:
                        return OddTemplate;
                }
            else if (item is XPolylineMode pencilMode)
                switch (pencilMode)
                {
                    case XPolylineMode.Add:
                        return EvenTemplate;
                    case XPolylineMode.Remove:
                        return OddTemplate;
                }
            else if (item is bool boolean)
                switch (boolean)
                {
                    case true:
                        return OddTemplate;
                    case false:
                        return EvenTemplate;
                }
            else if (item is XColorType colorType)
                switch (colorType)
                {
                    case XColorType.Solid:
                        return EvenTemplate;
                    case XColorType.Gradient:
                        return OddTemplate;
            }

                return EmptyTemplate;
        }
    }
}
