using System.Windows;
using System.Windows.Controls;

namespace NET.Paint.Resources.Handlers
{
    public partial class PreviewHandler : ResourceDictionary
    {
        private void Preview_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox text)
            {
                text.Focusable = true;
                text.Focus();
            }
        }
    }
}
