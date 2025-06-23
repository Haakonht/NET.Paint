using NET.Paint.Drawing.Model.Dialog;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using System.Windows;
using System.Windows.Controls;

namespace NET.Paint.View.Component
{
    /// <summary>
    /// Interaction logic for Shortcuts.xaml
    /// </summary>
    public partial class Shortcuts : UserControl
    {
        public Shortcuts()
        {
            InitializeComponent();
        }

        private void Undo(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;

            if (context != null)
                context.Command.Undo();
        }

        private void Redo(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;

            if (context != null)
                context.Command.Redo();
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;

            if (context != null && context.ActiveImage != null)
                if (context.ActiveImage.Selected is XRenderable renderable)
                    context.Command.Cut(renderable);
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;

            if (context != null && context.ActiveImage != null)
                if (context.ActiveImage.Selected != null)
                    context.Command.Copy(context.ActiveImage.Selected);
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;

            if (context != null && context.ActiveImage != null)
                context.Command.Paste();
        }

        private void OpenProject(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
            {
                var dialog = new System.Windows.Forms.OpenFileDialog
                {
                    Title = "Open Project",
                    Filter = "NETPaint Project (*.paint)|*.paint|All Files (*.*)|*.*",
                    FileName = service.Project.Title
                };
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //service.Command.SaveProject(dialog.FileName);
                }
            }
        }

        private void SaveProject(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
            {
                var dialog = new System.Windows.Forms.SaveFileDialog
                {
                    Title = "Save Project",
                    Filter = "NETPaint Project (*.paint)|*.paint|All Files (*.*)|*.*",
                    FileName = service.Project.Title
                };
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //service.Command.SaveProject(dialog.FileName);
                }
            }
        }
    }
}
