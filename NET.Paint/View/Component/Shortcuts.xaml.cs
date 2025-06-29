using NET.Paint.Drawing.Model.Dialog;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using System.Windows;
using System.Windows.Controls;

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
                    if (image.Selected is XRenderable renderable)
                        service.Command.Operations.Cut(renderable);
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

        private void ExportImage(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
                service.Command.Operations.ExportImage(service.ActiveImage);
        }

        #endregion
    }
}
