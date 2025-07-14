using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace NET.Paint.Selector
{
    public class PropertiesTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? SingleTemplate { get; set; }
        public DataTemplate? MultipleTemplate { get; set; }

        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {
            var selected = item as ObservableCollection<object>;
            if (selected?.Count == 1)
                return SingleTemplate;
            if (selected?.Count > 1)
                return MultipleTemplate;
            return null;
        }
    }
}
