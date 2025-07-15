using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace NET.Paint.View.Component
{
    /// <summary>
    /// Interaction logic for Tools.xaml
    /// </summary>
    public partial class ToolBox : UserControl
    {
        public ToolBox()
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

        private void CloseGridQuickSelect(object sender, RoutedEventArgs e) => GridQuickSelect.IsOpen = false;
        private void CloseZoomQuickSelect(object sender, RoutedEventArgs e) => ZoomQuickSelect.IsOpen = false;
        private void CloseRulerQuickSelect(object sender, RoutedEventArgs e) => RulerQuickSelect.IsOpen = false;
    }
}
