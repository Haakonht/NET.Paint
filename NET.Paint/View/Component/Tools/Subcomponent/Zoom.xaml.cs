using NET.Paint.Drawing.Model;
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

namespace NET.Paint.View.Component.Tools.Subcomponent
{
    /// <summary>
    /// Interaction logic for Zoom.xaml
    /// </summary>
    public partial class Zoom : UserControl
    {
        public Zoom()
        {
            InitializeComponent();
        }

        private void ZoomIn(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XTools;

            if (context != null && context.Zoom < 1.9)
                context.Zoom += 0.1;
        }

        private void ResetZoom(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XTools;

            if (context != null)
                context.Zoom = 1.0;
        }

        private void ZoomOut(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XTools;

            if (context != null && context.Zoom > 0.1)
                context.Zoom -= 0.1;
        }
    }
}
