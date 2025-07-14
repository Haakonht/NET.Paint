using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NET.Paint.View.Component
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

        #region Edit Handlers

        private void Undo(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
                service.Command.Operations.Undo();
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
                service.Command.Operations.Redo();
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
            {
                if (service.ActiveImage != null && service.ActiveImage is XImage image)
                    if (image.Selected.All(x => x is XRenderable renderable))
                        service.Command.Operations.Cut(image.Selected);
            }
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
            {
                if (service.ActiveImage != null && service.ActiveImage is XImage image)
                    if (image.Selected != null)
                        service.Command.Operations.Copy(image.Selected);
            }

        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
            {
                if (service.ActiveImage != null)
                    service.Command.Operations.Paste();
            }
        }

        #endregion

        #region Project Handlers

        private void NewProject(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
                service.Command.Operations.CreateProject();
        }

        private void OpenProject(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
                service.Command.Operations.OpenProject();
        }

        private void SaveProject(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
                service.Command.Operations.SaveProject();
        }

        #endregion

        #region Image Handlers 

        private void AddImage(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
                service.Command.Operations.CreateImage(new XImage
                {
                    Title = $"Image {service.Project.Images.Count + 1}"
                });
        }

        private void ExportImage(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
                service.Command.Operations.ExportImage(service.ActiveImage);
        }

        #endregion

        #region Layer Handlers

        private void AddVectorLayer(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
                service.Command.Operations.CreateLayer(new XVectorLayer
                {
                    Title = $"Layer {service.ActiveImage.Layers.Count + 1}"
                });
        }

        private void AddHybridLayer(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
                service.Command.Operations.CreateLayer(new XHybridLayer
                {
                    Title = $"Layer {service.ActiveImage.Layers.Count + 1}"
                });
        }

        private void AddRasterLayer(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
                service.Command.Operations.CreateLayer(new XRasterLayer
                {
                    Title = $"Layer {service.ActiveImage.Layers.Count + 1}"
                });
        }

        private void RemoveActiveLayer(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
                service.Command.Operations.RemoveLayer(service.ActiveImage.ActiveLayer);
        }

        #endregion

    }
}
