using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Service;
using System.Windows;
using System.Windows.Controls;

namespace NET.Paint.View.Component.Base.Controls
{
    /// <summary>
    /// Interaction logic for NotificationTray.xaml
    /// </summary>
    public partial class NotificationTray : UserControl
    {
        public NotificationTray()
        {
            InitializeComponent();
        }

        private void RemoveNotification(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is XNotification notification)
                if (DataContext is XService service)
                    service.Notifications.Remove(notification);
        }

        private void ToggleClipboard(object sender, RoutedEventArgs e)
        {
            if (DataContext is XService service)
                service.Preferences.ClipboardVisible = !service.Preferences.ClipboardVisible;
        }

        private void ToggleUndo(object sender, RoutedEventArgs e)
        {
            if (DataContext is XService service)
                service.Preferences.UndoVisible = !service.Preferences.UndoVisible;
        }
    }
}
