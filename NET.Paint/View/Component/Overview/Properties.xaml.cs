using NET.Paint.Drawing.Model.Structure;
using System.Windows;
using System.Windows.Controls;
using AvalonDock.Controls;

namespace NET.Paint.View.Component.Overview
{
    /// <summary>
    /// Interaction logic for Properties.xaml
    /// </summary>
    public partial class Properties : UserControl
    {
        public Properties()
        {
            InitializeComponent();
        }

        private void SetTabItemVisibility(object sender, RoutedEventArgs e)
        {
            if (sender is TabControl tabControl)
            {
                if (DataContext is ImageViewModel image)
                {
                    if (image.Selected.Count == 1)
                        foreach (TabItem item in tabControl.FindVisualChildren<TabItem>())
                            item.Visibility = Visibility.Collapsed;
                    else if (image.Selected.Count > 1)
                        foreach (TabItem item in tabControl.FindVisualChildren<TabItem>())
                            item.Visibility = Visibility.Visible;
                }

                tabControl.SelectedIndex = 0;
            }
        }
    }
}
