using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NET.Paint.View.Component.Overview.Controls
{
    /// <summary>
    /// Interaction logic for Layer.xaml
    /// </summary>
    public partial class Layer : UserControl
    {
        public Layer()
        {
            InitializeComponent();
        }

        private void OnAddComplete(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (tb.DataContext is XLayer layer)
                    layer.IsEditing = false;

                if (tb.DataContext is XImage image)
                    image.IsEditing = false;
            }
        }

        private void OnAddStarted(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                tb.Focusable = true;
                tb.Focus();
            }
        }

        private void OnAdd(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Escape) && sender is TextBox tb)
            {
                if (tb.DataContext is XLayer layer)
                    layer.IsEditing = false;

                if (tb.DataContext is XImage image)
                    image.IsEditing = false;

                e.Handled = true;
            }
        }
    }
}
