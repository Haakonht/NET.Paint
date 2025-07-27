using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static NET.Paint.View.Component.Properties.Converters.ObjectToPropertyInfoConverter;

namespace NET.Paint.View.Component.Dialog
{
    /// <summary>
    /// Interaction logic for Image.xaml
    /// </summary>
    public partial class Image : UserControl
    {
        public Image()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void IncrementButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DecrementButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
