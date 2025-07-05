using NET.Paint.Drawing.Constant;
using System.Windows;
using System.Windows.Controls;

namespace NET.Paint.Selector
{
    public class PolygonSelector : DataTemplateSelector
    {
        public DataTemplate StarTemplate { get; set; }
        public DataTemplate ArrowTemplate { get; set; }
        public DataTemplate SpiralTemplate { get; set; }
        public DataTemplate EmptyTemplate { get; set; } = new DataTemplate();

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is PolygonType type)
            {
                switch (type)
                {
                    case PolygonType.Star:
                        return StarTemplate;
                    case PolygonType.Arrow:
                        return ArrowTemplate;
                    case PolygonType.Spiral:
                        return SpiralTemplate;
                }
            }
            return EmptyTemplate;
        }
    }
}
