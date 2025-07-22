using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NET.Paint.View.Component.Overview.Controls
{
    /// <summary>
    /// Interaction logic for Image.xaml
    /// </summary>
    public partial class Image : UserControl
    {
        public Image()
        {
            InitializeComponent();
        }
        private void OnAddComplete(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (tb.DataContext is LayerViewModel layer)
                    layer.IsEditing = false;

                if (tb.DataContext is ImageViewModel image)
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
                if (tb.DataContext is LayerViewModel layer)
                    layer.IsEditing = false;

                if (tb.DataContext is ImageViewModel image)
                    image.IsEditing = false;

                e.Handled = true;
            }
        }
    }
}
