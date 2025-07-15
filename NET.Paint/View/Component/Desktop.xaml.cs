using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using NET.Paint.Helper;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Layout;

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
                        PreferencesAnchorable.IsVisible = context.Preferences.PreferencesVisible;
                        PropertiesAnchorable.IsVisible = context.ActiveImage.Selected.Count > 0;
                        ClipboardAnchorable.IsVisible = context.Clipboard.Data.Count > 0;
                        HistoryAnchorable.IsVisible = context.ActiveImage.Undo.History.Count > 0;

                        context.ActiveImage.Selected.CollectionChanged += (s, e) =>
                        {
                            Dispatcher.Invoke(() => PropertiesAnchorable.IsVisible = context.ActiveImage.Selected.Count > 0);
                        };
                        context.Clipboard.Data.CollectionChanged += (s, e) =>
                        {
                            Dispatcher.Invoke(() => ClipboardAnchorable.IsVisible = context.Clipboard.Data.Count > 0);
                        };
                        context.ActiveImage.Undo.History.CollectionChanged += (s, e) =>
                        {
                            Dispatcher.Invoke(() => HistoryAnchorable.IsVisible = context.ActiveImage.Undo.History.Count > 0);
                        };
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
            }
        }

        private void Service_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var context = DataContext as XService;
            if (sender is XPreferences service)
            {
                if (e.PropertyName == nameof(XService.Preferences.ToolboxVisible))
                    Dispatcher.Invoke(() => Toolbox.IsVisible = service.ToolboxVisible);

                if (e.PropertyName == nameof(XService.Preferences.PreferencesVisible))
                {
                    Dispatcher.Invoke(() =>
                    {
                        if (service.PreferencesVisible)
                        {
                            AnchorableHelper.CenterAnchorableOnApplication(PreferencesAnchorable);
                            PreferencesAnchorable.IsVisible = true;
                            if (!PreferencesAnchorable.IsFloating)
                                PreferencesAnchorable.Float();
                        }
                        else
                        {
                            PreferencesAnchorable.Dock();
                            PreferencesAnchorable.IsVisible = false;
                        }
                    });
                }

                if (e.PropertyName == nameof(XService.Preferences.OverviewVisible))
                {
                    Dispatcher.Invoke(() => ProjectTreeAnchorable.IsVisible = service.OverviewVisible);

                    if (!service.OverviewVisible)
                    {
                        PropertiesAnchorable.IsVisible = service.OverviewVisible;
                        HistoryAnchorable.IsVisible = service.OverviewVisible;
                        ClipboardAnchorable.IsVisible = service.OverviewVisible;
                    }
                    else
                        PropertiesAnchorable.IsVisible = context?.ActiveImage?.Selected.Count > 0;
                }
            }
        }

        private void OpenToolContextQuickSelect(object sender, MouseButtonEventArgs e)
        {
            ToolContextQuickSelect.IsOpen = true;
            e.Handled = true;
        }

        private void OpenToolContext(object sender, MouseButtonEventArgs e)
        {
            ToolContextMenu.IsOpen = true;
            e.Handled = true;
        }

        private void OpenToolContextSpecific(object sender, MouseEventArgs e)
        {
            if (sender is Image element && DataContext is XService service)
                if (int.TryParse(element.Tag.ToString(), out int index))
                    ToolContext.TabManager.SelectedIndex = index;

            ToolContextQuickSelect.IsOpen = false;
            ToolContextMenu.IsOpen = true;
        }

        private void CloseToolContextQuickSelect(object sender, MouseEventArgs e)
        {
            ToolContextQuickSelect.IsOpen = false;
            e.Handled = true;
        }

        private void CloseToolContextQuickSelect(object sender, MouseButtonEventArgs e)
        {
            ToolContextQuickSelect.IsOpen = false;
            e.Handled = true;
        }

        private void CloseToolContext(object sender, MouseEventArgs e)
        {
            ToolContextQuickSelect.IsOpen = false;
            e.Handled = true;
        }
    }
}
