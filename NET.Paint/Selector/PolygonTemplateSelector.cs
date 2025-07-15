using NET.Paint.Drawing.Constant;
using System.Windows;
using System.Windows.Controls;

namespace NET.Paint.Selector
{
    public class PolygonTemplateSelector : DataTemplateSelector
    {
        public DataTemplate RegularTemplate { get; set; }
        public DataTemplate HeartTemplate { get; set; }
        public DataTemplate StarTemplate { get; set; }
        public DataTemplate ArrowTemplate { get; set; }
        public DataTemplate SpiralTemplate { get; set; }
        public DataTemplate EmptyTemplate { get; set; } = new DataTemplate();

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is XPolygonStyle style)
            {
                switch (style)
                {
                    case XPolygonStyle.Regular:
                        return RegularTemplate;
                    case XPolygonStyle.Star:
                        return StarTemplate;
                    case XPolygonStyle.Arrow:
                        return ArrowTemplate;
                    case XPolygonStyle.Spiral:
                        return SpiralTemplate;
                    case XPolygonStyle.Heart:
                        return HeartTemplate;
                }
            }
            return EmptyTemplate;
        }
    }
}
