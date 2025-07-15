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
        public DataTemplate ManipulatorTemplate { get; set; }
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
                    case XSelectionMode.Manipulator:
                        return ManipulatorTemplate;
                    case XSelectionMode.Lasso:
                        return LassoTemplate;
                }
            }
            return EmptyTemplate;
        }
    }
}
