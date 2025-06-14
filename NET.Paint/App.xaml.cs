using NET.Paint.View;
using System.Windows;

namespace NET.Paint
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /*
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Show splash window
            var splash = new SplashWindow();
            splash.Show();

            // Perform loading on background thread
            Task.Run(() =>
            {
                // Simulate load delay or do initialization
                Thread.Sleep(1500);
                splash.LoadingText = "Loading configuration";

                Thread.Sleep(1500);
                splash.LoadingText = "Loading user preferences";

                Thread.Sleep(1000);
                splash.LoadingText = "Getting ready";

                // Switch to UI thread to create main window
                Dispatcher.Invoke(() =>
                {
                    var mainWindow = new MainWindow();
                    mainWindow.Show();

                    // Close splash
                    splash.Close();
                });
            });
        }
        */
    }
}
