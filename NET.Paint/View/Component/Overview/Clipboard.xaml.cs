using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
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

namespace NET.Paint.View.Component.Overview
{
    /// <summary>
    /// Interaction logic for Clipboard.xaml
    /// </summary>
    public partial class Clipboard : UserControl
    {
        public Clipboard()
        {
            InitializeComponent();
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            if (DataContext is DesktopViewModel service && service.Clipboard.Data != null)
                service.Command.Operations.Paste();
        }

        private void ClearClipboard(object sender, RoutedEventArgs e)
        {
            if (DataContext is DesktopViewModel service)
                service.Command.Operations.ClearClipboard();
        }
    }
}
