using AvalonDock;
using AvalonDock.Layout;
using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using NET.Paint.Helper;
using NET.Paint.View.Component.Dialog;
using NET.Paint.View.Component.Tools;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Collections.Specialized;
using AvalonDock.Controls;

namespace NET.Paint.View.Component
{
    /// <summary>
    /// Interaction logic for Workbench.xaml
    /// </summary>
    public partial class Desktop : UserControl
    {
        private XImage _currentDocument;

        public Desktop()
        {
            InitializeComponent();

            // Dialogs
            PreferencesDialog.IsVisible = false;
            ImageDialog.IsVisible = false;
            ProjectDialog.IsVisible = false;

            // Anchorables
            PropertiesAnchorable.IsVisible = false;
            ClipboardAnchorable.IsVisible = false;
            UndoAnchorable.IsVisible = false;
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
                        // Unsubscribe from previous document's events
                        UnsubscribeFromCurrentDocument();

                        context.ActiveImage = document;
                        _currentDocument = document;

                        if (context.ActiveImage != null)
                        {
                            PreferencesDialog.IsVisible = context.Preferences.PreferencesDialogVisible;
                            PropertiesAnchorable.IsVisible = context.ActiveImage.Selected.Count > 0;
                            ClipboardAnchorable.IsVisible = context.Preferences.ClipboardVisible;
                            UndoAnchorable.IsVisible = context.Preferences.UndoVisible;
                            ProjectTree.SetActiveImage(context.ActiveImage);
                            
                            // Subscribe to new document's events
                            document.Selected.CollectionChanged += Document_Selected_CollectionChanged;
                        }
                    }
                }
                else
                {
                    // No active document, unsubscribe from current
                    UnsubscribeFromCurrentDocument();
                }
            }
        }

        private void Document_Selected_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var context = DataContext as XService;
            if (context?.ActiveImage != null)
            {
                PropertiesAnchorable.IsVisible = context.ActiveImage.Selected.Count > 0;
            }
        }

        private void UnsubscribeFromCurrentDocument()
        {
            if (_currentDocument != null)
            {
                _currentDocument.Selected.CollectionChanged -= Document_Selected_CollectionChanged;
                _currentDocument = null;
            }
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is XService service)
            {
                service.Preferences.PropertyChanged += Service_PropertyChanged;
            }
        }

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
                        Dispatcher.Invoke(() => ToolboxAnchorable.IsVisible = preferences.ToolboxVisible);
                        break;
                    case nameof(XService.Preferences.PreferencesDialogVisible):
                        Dispatcher.Invoke(() => PreferencesDialog.IsVisible = preferences.PreferencesDialogVisible);
                        break;
                    case nameof(XService.Preferences.ImageDialogVisible):
                        Dispatcher.Invoke(() => ImageDialog.IsVisible = preferences.ImageDialogVisible);
                        break;
                    case nameof(XService.Preferences.ProjectDialogVisible):
                        Dispatcher.Invoke(() => ProjectDialog.IsVisible = preferences.ProjectDialogVisible);
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

        private void Dialog_Closing(object sender, CancelEventArgs e)
        {
            if (DataContext is XService service && sender is LayoutAnchorable anchorable)
            {
                service.Preferences.PreferencesDialogVisible = false;
                service.Preferences.ImageDialogVisible = false;
                service.Preferences.ProjectDialogVisible = false;
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            UnsubscribeFromCurrentDocument();
        }
    }
}
