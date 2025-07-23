using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using NET.Paint.Helper;
using NET.Paint.View.Component.Tools;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AvalonDock;
using AvalonDock.Layout;

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
                if (dockingManager.ActiveContent is XImage document)
                {
                    if (document != null && context != null)
                    {
                        context.ActiveImage = document;

                        if (context.ActiveImage != null)
                        {
                            PreferencesAnchorable.IsVisible = context.Preferences.PreferencesVisible;
                            PropertiesAnchorable.IsVisible = context.ActiveImage.Selected.Count > 0;
                            ClipboardAnchorable.IsVisible = context.Preferences.ClipboardVisible;
                            UndoAnchorable.IsVisible = context.Preferences.UndoVisible;
                            ProjectTree.SetActiveImage(context.ActiveImage);

                            context.ActiveImage.Selected.CollectionChanged += (s, e) =>
                            {
                                Dispatcher.Invoke(() => PropertiesAnchorable.IsVisible = context.ActiveImage.Selected.Count > 0);
                            };
                        }
                    }
                }
                else
                {
                    PreferencesAnchorable.IsVisible = false;
                    PropertiesAnchorable.IsVisible = false;
                    ClipboardAnchorable.IsVisible = false;
                    UndoAnchorable.IsVisible = false;
                }
            }
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) => (DataContext as XService).Preferences.PropertyChanged += Service_PropertyChanged;

        private void Service_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var service = DataContext as XService;
            if (sender is XPreferences preferences)
            {
                switch(e.PropertyName)
                {
                    case nameof(XService.Preferences.ClipboardVisible):
                        Dispatcher.Invoke(() => ClipboardAnchorable.IsVisible = preferences.ClipboardVisible);
                        break;
                    case nameof(XService.Preferences.UndoVisible):
                        Dispatcher.Invoke(() => UndoAnchorable.IsVisible = preferences.UndoVisible);
                        break;
                    case nameof(XService.Preferences.ToolboxVisible):
                        Dispatcher.Invoke(() => Toolbox.IsVisible = preferences.ToolboxVisible);
                        break;
                    case nameof(XService.Preferences.PreferencesVisible):
                        Dispatcher.Invoke(() =>
                        {
                            if (preferences.PreferencesVisible)
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
                        break;
                    case nameof(XService.Preferences.OverviewVisible):               
                        Dispatcher.Invoke(() => ProjectTreeAnchorable.IsVisible = preferences.OverviewVisible);
                        if (!preferences.OverviewVisible)
                        {
                            PropertiesAnchorable.IsVisible = false;
                            preferences.UndoVisible = false;
                            preferences.ClipboardVisible = false;
                        }
                        else
                            PropertiesAnchorable.IsVisible = service?.ActiveImage?.Selected.Count > 0;
                        break;
                }
            }
        }

        private void OpenToolContextQuickSelect(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is XService service && service.Tools.ActiveTool == XToolType.Selector)
                ToolContextMenu.IsOpen = true;
            else
                ToolContextQuickSelect.IsOpen = true;
            e.Handled = true;
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

        private void OnOpenToolContextRequested(object sender, RoutedEventArgs e)
        {
            ToolContextMenu.IsOpen = true;
            ToolContextQuickSelect.IsOpen = false;
        }

        private void OnOpenSpecificToolContext(object sender, RoutedEventArgs e)
        {
            if (e is ToolContextEventArgs args && int.TryParse(args.TabIndex?.ToString(), out int index))
            {
                ToolContext.ContextIndex = index;
            }

            ToolContextMenu.IsOpen = true;
            ToolContextQuickSelect.IsOpen = false;
        }

        private void OnCloseToolContextRequested(object sender, RoutedEventArgs e)
        {
            ToolContextMenu.IsOpen = false;
        }

        private void CloseToolContext(object sender, MouseEventArgs e)
        {
            ToolContextMenu.IsOpen = false;
            e.Handled = true;
        }
    }
}
