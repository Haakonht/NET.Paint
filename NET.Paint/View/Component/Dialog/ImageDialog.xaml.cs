using NET.Paint.Drawing.Model.Dialog;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NET.Paint.View.Component.Dialog
{
    /// <summary>
    /// Interaction logic for ImageDialog.xaml
    /// </summary>
    public partial class ImageDialog : Window
    {
        public XImageDialog Result { get; private set; }

        public ImageDialog(XImageDialog dialog)
        {
            InitializeComponent();
            DataContext = dialog;
            SourceInitialized += Dialog_SourceInitialized;
        }

        private void Dialog_SourceInitialized(object sender, EventArgs e)
        {
            var hwnd = new WindowInteropHelper(this).Handle;

            // DWMWA_WINDOW_CORNER_PREFERENCE = 33
            // DWMWCP_DONOTROUND = 1
            int DWMWA_WINDOW_CORNER_PREFERENCE = 33;
            int DWMWCP_DONOTROUND = 1;

            DwmSetWindowAttribute(hwnd, DWMWA_WINDOW_CORNER_PREFERENCE, ref DWMWCP_DONOTROUND, sizeof(int));
        }

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private void LayoutAnchorable_Closing(object sender, System.ComponentModel.CancelEventArgs e) => Close();

        private void Create(object sender, RoutedEventArgs e)
        {
            if (DataContext is XImageDialog dialog)
            {
                Result = dialog;
                DialogResult = true;
                Close();
            }
        }

        private void Cancel(object sender, RoutedEventArgs e) => Close();
    }
}
