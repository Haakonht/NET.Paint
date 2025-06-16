using System.IO;
using System.Windows;
using System.Windows.Input;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace NET.Paint.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                ToggleMaximizeRestore();
            else
                DragMove();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;       
        private void MaximizeRestore_Click(object sender, RoutedEventArgs e) => ToggleMaximizeRestore();        
        private void Close_Click(object sender, RoutedEventArgs e) => Close();     
        private void ToggleMaximizeRestore() => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var serializer = new XmlLayoutSerializer(Desktop.DockingManager);
            serializer.Serialize("Layout.config");
        }
    }
}