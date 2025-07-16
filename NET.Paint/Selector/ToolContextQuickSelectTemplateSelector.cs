using System.Windows;
using System.Windows.Controls;
using NET.Paint.Drawing.Constant;

namespace NET.Paint.Selector
{
    public class ToolContextQuickSelectTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PolygonTemplate { get; set; }
        public DataTemplate SelectorTemplate { get; set; }
        public DataTemplate DefaultTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is XToolType type)
            {
                switch (type)
                {
                    case XToolType.Polygon:
                        return PolygonTemplate;
                    case XToolType.Selector:
                        return SelectorTemplate;
                    default:
                        return DefaultTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}