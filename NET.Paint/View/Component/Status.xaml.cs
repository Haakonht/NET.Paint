using NET.Paint.Drawing.Service;
using System.Windows;
using System.Windows.Controls;

namespace NET.Paint.View.Component
{
    /// <summary>
    /// Interaction logic for Status.xaml
    /// </summary>
    public partial class Status : UserControl
    {
        public Status()
        {
            InitializeComponent();
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
