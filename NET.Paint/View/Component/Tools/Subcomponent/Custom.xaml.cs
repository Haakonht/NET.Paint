using NET.Paint.Drawing.Service;
using System.Windows.Controls;

namespace NET.Paint.View.Component.Tools.Subcomponent
{
    /// <summary>
    /// Interaction logic for Contextual.xaml
    /// </summary>
    public partial class Custom : UserControl
    {
        public Custom()
        {
            InitializeComponent();
        }

        private void SelectBitmap(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is not null && DataContext is XService service)
                if (sender is Button && sender is Button button)
                    if (button.Content is Image image)
                        service.Tools.ActiveBitmap = image.Source;
        }
    }
}
