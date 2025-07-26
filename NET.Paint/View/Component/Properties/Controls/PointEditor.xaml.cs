using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static NET.Paint.View.Component.Properties.Converters.ObjectToPropertyInfoConverter;

namespace NET.Paint.View.Component.Properties.Controls
{
    /// <summary>
    /// Interaction logic for NumberEditor.xaml
    /// </summary>
    public partial class PointEditor : UserControl
    {
        public PointEditor()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #region Points Template 


        #endregion
    }
}
