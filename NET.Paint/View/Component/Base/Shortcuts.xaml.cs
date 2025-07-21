using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NET.Paint.View.Component.Base
{
    /// <summary>
    /// Interaction logic for Shortcuts.xaml
    /// </summary>
    public partial class Shortcuts : UserControl
    {
        public Shortcuts()
        {
            InitializeComponent();
        }

        #region Popup Handlers

        private void OpenLayerQuickSelect(object sender, RoutedEventArgs e)
        {
            LayerQuickSelect.IsOpen = true;
            e.Handled = true;
        }

        private void CloseLayerQuickSelect(object sender, MouseEventArgs e)
        {
            LayerQuickSelect.IsOpen = false;
            e.Handled = true;
        }

        #endregion

        #region Object Creation Handlers (Keep these for creating new objects with calculated titles)

        private void AddImage(object sender, RoutedEventArgs e)
        {
            if (DataContext is XService service && service.Project != null)
                service.Command.Operations.CreateImage(new XImage
                {
                    Title = $"Image {service.Project.Images.Count + 1}"
                });
        }

        private void AddVectorLayer(object sender, RoutedEventArgs e)
        {
            if (DataContext is XService service && service.ActiveImage != null)
            {
                service.Command.Operations.CreateLayer(new XVectorLayer
                {
                    Title = $"Layer {service.ActiveImage.Layers.Count + 1}"
                });
                LayerQuickSelect.IsOpen = false;
            }
        }

        private void AddHybridLayer(object sender, RoutedEventArgs e)
        {
            if (DataContext is XService service && service.ActiveImage != null)
            {
                service.Command.Operations.CreateLayer(new XHybridLayer
                {
                    Title = $"Layer {service.ActiveImage.Layers.Count + 1}"
                });
                LayerQuickSelect.IsOpen = false;
            }
        }

        private void AddRasterLayer(object sender, RoutedEventArgs e)
        {
            if (DataContext is XService service && service.ActiveImage != null)
            {
                service.Command.Operations.CreateLayer(new XRasterLayer
                {
                    Title = $"Layer {service.ActiveImage.Layers.Count + 1}"
                });
                LayerQuickSelect.IsOpen = false;
            }
        }

        #endregion
    }
}
