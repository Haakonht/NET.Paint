using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace NET.Paint.View.Component
{
    /// <summary>
    /// Interaction logic for Workbench.xaml
    /// </summary>
    public partial class Desktop : UserControl
    {
        public Desktop()
        {
            InitializeComponent();

            /*if (File.Exists("Layout.config"))
            {
                var serializer = new XmlLayoutSerializer(DockingManager);
                serializer.Deserialize("Layout.config");
            }*/
        }

        private void ActiveContentChanged(object sender, EventArgs e)
        {
            var context = DataContext as XService;

            if (sender is DockingManager dockingManager)
            {
                var document = dockingManager.ActiveContent as LayoutDocument;

                if (document != null && context != null && document.Content is XImage image)
                {
                    context.ActiveImage = image;

                    if (context.ActiveImage != null)
                    {
                        context.ActiveImage.PropertyChanged += ActiveImage_PropertyChanged;
                        PropertiesAnchorable.IsVisible = context.ActiveImage.Selected != null;
                    }
                }
            }
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var context = DataContext as XService;

            if (context != null)
                context.Preferences.PropertyChanged += Service_PropertyChanged;
        }

        private void ActiveImage_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(XImage.Selected) && sender is XImage image)
                Dispatcher.Invoke(() => PropertiesAnchorable.IsVisible = image.Selected != null);
        }

        private void Service_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var context = DataContext as XService;
            if (sender is XPreferences service)
            {
                if (e.PropertyName == nameof(XService.Preferences.ToolboxVisible))
                    Dispatcher.Invoke(() => Toolbox.IsVisible = service.ToolboxVisible);

                if (e.PropertyName == nameof(XService.Preferences.OverviewVisible))
                {
                    Dispatcher.Invoke(() => ImageTree.IsVisible = service.OverviewVisible);
                    Dispatcher.Invoke(() => ProjectTree.IsVisible = service.OverviewVisible);

                    if (!service.OverviewVisible)
                        PropertiesAnchorable.IsVisible = service.OverviewVisible;
                    else
                        PropertiesAnchorable.IsVisible = context?.ActiveImage?.Selected != null;
                }
            }
        }
    }
}
