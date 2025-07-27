using System.Windows;
using AvalonDock.Layout;

namespace NET.Paint.Helper
{
    public static class AnchorableHelper
    {
        public static Point CenterAnchorableOnApplication()
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

                return new Point(centerX, centerY);
            }
            return new Point(0, 0);
        }
    }
}
