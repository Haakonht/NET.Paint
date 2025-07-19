using System.Windows;
using System.Windows.Controls;
using NET.Paint.Drawing.Constant;

namespace NET.Paint.Selector
{
    public class QuickSelectTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SingleTemplate { get; set; }
        public DataTemplate DoubleTemplate { get; set; }
        public DataTemplate TripleTemplate { get; set; }
        public DataTemplate QuadrupleTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is XToolType type)
            {
                switch (type)
                {
                    case XToolType.Bitmap:
                        return QuadrupleTemplate;
                    case XToolType.Line:
                    case XToolType.Curve:
                    case XToolType.Bezier:
                    case XToolType.Pencil:
                        return DoubleTemplate;
                    case XToolType.Selector:
                        return SingleTemplate;
                    default:
                        return TripleTemplate;
                }
            }

            return base.SelectTemplate(item, container);
        }
    }
}