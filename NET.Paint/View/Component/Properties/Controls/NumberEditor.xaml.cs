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
    public partial class NumberEditor : UserControl
    {
        public NumberEditor()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #region Numbers Template 

        private void IncrementButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is PropertyWrapper wrapper)
                IncrementValue(wrapper);
        }

        private void DecrementButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is PropertyWrapper wrapper)
                DecrementValue(wrapper);
        }

        private void IncrementValue(PropertyWrapper wrapper)
        {
            if (wrapper.CanWrite && wrapper.Value != null)
            {
                var currentValue = wrapper.Value;

                if (currentValue is double doubleValue)
                    wrapper.Value = doubleValue + 1.0;
                else if (currentValue is int intValue)
                    wrapper.Value = intValue + 1;
                else if (currentValue is float floatValue)
                    wrapper.Value = floatValue + 1.0f;
                else if (currentValue is decimal decimalValue)
                    wrapper.Value = decimalValue + 1.0m;
            }
        }

        private void DecrementValue(PropertyWrapper wrapper)
        {
            if (wrapper.CanWrite && wrapper.Value != null)
            {
                var currentValue = wrapper.Value;

                if (currentValue is double doubleValue)
                    wrapper.Value = doubleValue - 1.0;
                else if (currentValue is int intValue)
                    wrapper.Value = intValue - 1;
                else if (currentValue is float floatValue)
                    wrapper.Value = floatValue - 1.0f;
                else if (currentValue is decimal decimalValue)
                    wrapper.Value = decimalValue - 1.0m;
            }
        }

        #endregion
    }
}
