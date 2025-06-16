using NET.Paint.Drawing.Model.Shape;
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
    }
}
