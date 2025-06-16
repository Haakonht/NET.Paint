using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock;
using NET.Paint.Drawing.Model;

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
            var dockingManager = sender as DockingManager;
            var document = dockingManager.ActiveContent as LayoutDocument;

            if (document != null)
            {
                context.ActiveImage = document.Content as XImage;
                context.ActiveImage.PropertyChanged += ActiveImage_PropertyChanged;
                PropertiesAnchorable.IsVisible = context.ActiveImage.Selected != null;
            }
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var context = DataContext as XService;

            if (context != null)
                context.Preferences.PropertyChanged += Service_PropertyChanged;
        }

        private void ActiveImage_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(XImage.Selected) && sender is XImage image)
                Dispatcher.Invoke(() => PropertiesAnchorable.IsVisible = image.Selected != null);
        }

        private void Service_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
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
                        PropertiesAnchorable.IsVisible = context.ActiveImage.Selected != null;
                }
            }
        }
    }
}
