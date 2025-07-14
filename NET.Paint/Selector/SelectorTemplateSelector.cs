using NET.Paint.Drawing.Constant;
using System.Windows;
using System.Windows.Controls;
using SelectionMode = NET.Paint.Drawing.Constant.SelectionMode;

namespace NET.Paint.Selector
{
    public class SelectorTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PointerTemplate { get; set; }
        public DataTemplate RectangleTemplate { get; set; }
        public DataTemplate LassoTemplate { get; set; }
        public DataTemplate ManipulatorTemplate { get; set; }
        public DataTemplate EmptyTemplate { get; set; } = new DataTemplate();

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is SelectionMode mode)
            {
                switch (mode)
                {
                    case SelectionMode.Pointer:
                        return PointerTemplate;
                    case SelectionMode.Rectangle:
                        return RectangleTemplate;
                    case SelectionMode.Manipulator:
                        return ManipulatorTemplate;
                    case SelectionMode.Lasso:
                        return LassoTemplate;
                }
            }
            return EmptyTemplate;
        }
    }
}
