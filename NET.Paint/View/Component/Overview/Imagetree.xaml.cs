using NET.Paint.Drawing.Model.Structure;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
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

namespace NET.Paint.View.Component
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class Imagetree : UserControl
    {
        public Imagetree()
        {
            InitializeComponent();
        }

        private void SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var context = DataContext as XImage;

            if (context != null)
            {
                context.Selected = e.NewValue;

                if (e.NewValue is XLayer)
                    context.ActiveLayer = e.NewValue as XLayer;
            }
        }

        private void Unselect(object sender, MouseButtonEventArgs e)
        {
            var context = DataContext as XImage;

            if (context != null)
                context.Selected = null;             
        }

        private void SelectImage(object sender, MouseButtonEventArgs e)
        {
            var context = DataContext as XImage;

            if (context != null)
                context.Selected = context;

            e.Handled = true;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XImage;

            if (context != null)
                context.Layers.Add(new XLayer() { Title = $"Layer {context.Layers.Count}" });
        }
    }
}
