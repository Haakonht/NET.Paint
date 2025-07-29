using NET.Paint.Drawing.Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NET.Paint.View.Component.Tools
{
    /// <summary>
    /// Interaction logic for Tools.xaml
    /// </summary>
    public partial class ToolBar : UserControl
    {
        public ToolBar()
        {
            InitializeComponent();
        }

        private void OpenPolygonQuickSelect(object sender, RoutedEventArgs e) => PolygonQuickSelect.IsOpen = true;
        private void ClosePolygonQuickSelect(object sender, MouseEventArgs e) => PolygonQuickSelect.IsOpen = false;
        private void OpenSelectorQuickSelect(object sender, RoutedEventArgs e) => SelectorQuickSelect.IsOpen = true;
        private void CloseSelectorQuickSelect(object sender, MouseEventArgs e) => SelectorQuickSelect.IsOpen = false;
        private void OpenRectangleQuickSelect(object sender, RoutedEventArgs e) => RectangleQuickSelect.IsOpen = true;
        private void CloseRectangleQuickSelect(object sender, MouseEventArgs e) => RectangleQuickSelect.IsOpen = false;
        private void OpenEllipseQuickSelect(object sender, RoutedEventArgs e) => EllipseQuickSelect.IsOpen = true;
        private void CloseEllipseQuickSelect(object sender, MouseEventArgs e) => EllipseQuickSelect.IsOpen = false;
        private void OpenPencilQuickSelect(object sender, RoutedEventArgs e) => PencilQuickSelect.IsOpen = true;
        private void ClosePencilQuickSelect(object sender, MouseEventArgs e) => PencilQuickSelect.IsOpen = false;
        private void CloseThicknessQuickSelect(object sender, RoutedEventArgs e) => ThicknessQuickSelect.IsOpen = false;
        private void CloseGridQuickSelect(object sender, RoutedEventArgs e) => GridQuickSelect.IsOpen = false;
        private void CloseZoomQuickSelect(object sender, RoutedEventArgs e) => ZoomQuickSelect.IsOpen = false;
        private void CloseRulerQuickSelect(object sender, RoutedEventArgs e) => RulerQuickSelect.IsOpen = false;

        private void SwapColor(object sender, RoutedEventArgs e)
        {
            if (DataContext is XService service)
            {
                var temp = service.Tools.PrimaryColor;
                service.Tools.PrimaryColor = service.Tools.SecondaryColor;
                service.Tools.SecondaryColor = temp;
            }
        }
    }
}
