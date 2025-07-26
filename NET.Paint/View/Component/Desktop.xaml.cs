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

            PreferencesAnchorable.IsVisible = false;
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
                        context.ActiveImage = document;

                        if (context.ActiveImage != null)
                        {
                            PreferencesAnchorable.IsVisible = context.Preferences.PreferencesVisible;
                            PropertiesAnchorable.IsVisible = context.ActiveImage.Selected.Count > 0;
                            ClipboardAnchorable.IsVisible = context.Preferences.ClipboardVisible;
                            UndoAnchorable.IsVisible = context.Preferences.UndoVisible;
                            ProjectTree.SetActiveImage(context.ActiveImage);
                            PropertiesAnchorable.IsVisible = context.ActiveImage.Selected.Count > 0;
                        }
                    }
                }
            }
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext is XService service)
            {
                service.Preferences.PropertyChanged += Service_PropertyChanged;

                if (service.ActiveImage != null)
                    service.ActiveImage.PropertyChanged += ActiveImage_PropertyChanged;
            }
        }

        private void ActiveImage_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (sender is XImage image)
            {
                switch (e.PropertyName)
                {
                    case nameof(XImage.Selected):
                        Dispatcher.Invoke(() => PropertiesAnchorable.IsVisible = image.Selected.Count > 0);
                        break;
                }
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
    }
}
