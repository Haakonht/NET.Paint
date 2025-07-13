using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
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
                        ClipboardAnchorable.IsVisible = context.Clipboard.Data != null;
                        HistoryAnchorable.IsVisible = context.ActiveImage.Undo.History.Count > 0;
                    }
                }
            }
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var context = DataContext as XService;

            if (context != null)
            {
                context.Preferences.PropertyChanged += Service_PropertyChanged;
                context.Clipboard.PropertyChanged += Clipboard_PropertyChanged;
                context.ActiveImage.Undo.PropertyChanged += Undo_PropertyChanged;
            }
        }

        private void ActiveImage_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(XImage.Selected) && sender is XImage image)
                Dispatcher.Invoke(() => PropertiesAnchorable.IsVisible = image.Selected != null && image.Selected is not IEnumerable<object>);
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
                    //Dispatcher.Invoke(() => ImageTree.IsVisible = service.OverviewVisible);
                    Dispatcher.Invoke(() => ProjectTreeAnchorable.IsVisible = service.OverviewVisible);

                    if (!service.OverviewVisible)
                        PropertiesAnchorable.IsVisible = service.OverviewVisible;
                    else
                        PropertiesAnchorable.IsVisible = context?.ActiveImage?.Selected != null;
                }
            }
        }

        private void Clipboard_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var context = DataContext as XService;
            if (sender is XClipboard clipboard)
            {
                if (e.PropertyName == nameof(clipboard.Data))
                    ClipboardAnchorable.IsVisible = clipboard.Data != null;
            }
        }

        private void Undo_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var context = DataContext as XService;
            if (sender is XUndo undo)
            {
                if (e.PropertyName == nameof(XUndo.History))
                    HistoryAnchorable.IsVisible = undo.History.Count > 0;
            }
        }

        private void OpenContext(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Toolcontext.IsOpen = true;
            e.Handled = true;
        }

        private void CloseContext(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Toolcontext.IsOpen = false;
            e.Handled = true;
        }
    }
}
