using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using NET.Paint.Drawing.Model.Dialog;

namespace NET.Paint.View.Component.Dialog
{
    /// <summary>
    /// Interaction logic for LayerDialog.xaml
    /// </summary>
    public partial class LayerDialog : Window
    {
        public XLayerDialog Result { get; private set; }

        public LayerDialog(XLayerDialog dialog)
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

        private void Create(object sender, RoutedEventArgs e)
        {
            if (DataContext is XLayerDialog dialog)
            {
                Result = dialog;
                DialogResult = true;
                Close();
            }
        }

        private void Cancel(object sender, RoutedEventArgs e) => Close();

        [DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        private void LayoutAnchorable_Closing(object sender, System.ComponentModel.CancelEventArgs e) => Close();
    }
}
