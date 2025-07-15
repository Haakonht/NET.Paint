using System.Windows;
using Xceed.Wpf.AvalonDock.Layout;

namespace NET.Paint.Helper
{
    public static class AnchorableHelper
    {
        public static void CenterAnchorableOnApplication(LayoutAnchorable anchorable)
        {
            var mainWindow = Application.Current.MainWindow;
            if (mainWindow != null)
            {
                // Calculate center position for a 250x570 window (from XAML)
                double centerX = mainWindow.Left + (mainWindow.ActualWidth / 2);
                double centerY = mainWindow.Top + (mainWindow.ActualHeight / 2);

                // Ensure the window doesn't go off-screen
                centerX = Math.Max(0, centerX);
                centerY = Math.Max(0, centerY);

                anchorable.FloatingLeft = centerX - (anchorable.FloatingWidth / 2);
                anchorable.FloatingTop = centerY - (anchorable.FloatingHeight / 2);
            }
        }
    }
}
