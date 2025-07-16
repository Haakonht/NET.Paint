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
            else if (item is XPencilMode pencilMode)
                switch (pencilMode)
                {
                    case XPencilMode.Add:
                        return EvenTemplate;
                    case XPencilMode.Remove:
                        return OddTemplate;
                }
            else if (item is bool boolean)
                switch (boolean)
                {
                    case true:
                        return EvenTemplate;
                    case false:
                        return OddTemplate;
                }

            return EmptyTemplate;
        }
    }
}
