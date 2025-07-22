using System.Windows.Controls;
using System.Windows;
using NET.Paint.Drawing.Model.Structure;

namespace NET.Paint.Selector
{
    public class LayoutTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DocumentTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ImageViewModel)
                return DocumentTemplate;

            return base.SelectTemplate(item, container);
        }
    }
}
