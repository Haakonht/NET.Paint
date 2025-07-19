using NET.Paint.Drawing.Constant;
using System.Windows;
using System.Windows.Controls;

namespace NET.Paint.Selector
{
    public class NotificationSourceTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ClipboardTemplate { get; set; }
        public DataTemplate HistoryTemplate { get; set; }
        public DataTemplate SelectionTemplate { get; set; }
        public DataTemplate MessageTemplate { get; set; }
        public DataTemplate EmptyTemplate { get; set; } = new DataTemplate();

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is XNotificationSource source)
                switch (source)
                {
                    case XNotificationSource.Clipboard:
                        return ClipboardTemplate;
                    case XNotificationSource.History:
                        return HistoryTemplate;
                    case XNotificationSource.Selection:
                        return SelectionTemplate;
                    default:
                        return MessageTemplate;
                }

            return EmptyTemplate;
        }
    }
}
