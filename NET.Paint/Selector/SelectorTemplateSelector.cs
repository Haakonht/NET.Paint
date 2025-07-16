using NET.Paint.Drawing.Constant;
using System.Windows;
using System.Windows.Controls;
using XSelectionMode = NET.Paint.Drawing.Constant.XSelectionMode;

namespace NET.Paint.Selector
{
    public class SelectorTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PointerTemplate { get; set; }
        public DataTemplate RectangleTemplate { get; set; }
        public DataTemplate LassoTemplate { get; set; }
        public DataTemplate MoveTemplate { get; set; }
        public DataTemplate RotateTemplate { get; set; }
        public DataTemplate EmptyTemplate { get; set; } = new DataTemplate();

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is XSelectionMode mode)
            {
                switch (mode)
                {
                    case XSelectionMode.Pointer:
                        return PointerTemplate;
                    case XSelectionMode.Rectangle:
                        return RectangleTemplate;
                    case XSelectionMode.Move:
                        return MoveTemplate;
                    case XSelectionMode.Rotate:
                        return RotateTemplate;
                    case XSelectionMode.Lasso:
                        return LassoTemplate;
                }
            }
            return EmptyTemplate;
        }
    }
}
