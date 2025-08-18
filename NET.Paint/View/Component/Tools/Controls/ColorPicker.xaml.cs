using NET.Paint.Drawing.Model.Utility;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NET.Paint.View.Component.Tools.Controls
{
    /// <summary>
    /// Interaction logic for Color.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public static readonly DependencyProperty SelectedColorProperty =
             DependencyProperty.Register("SelectedColor", typeof(XColor), typeof(ColorPicker),
                 new PropertyMetadata(new XSolidColor { Color = Colors.Black }));

        public XColor SelectedColor
        {
            get { return (XColor)GetValue(SelectedColorProperty); }
            set { SetValue(SelectedColorProperty, value); }
        }

        public static readonly DependencyProperty NameVisibilityProperty =
            DependencyProperty.Register("ShowName", typeof(Visibility), typeof(ColorPicker),
        new PropertyMetadata(Visibility.Visible));

        public Visibility NameVisibility
        {
            get { return (Visibility)GetValue(NameVisibilityProperty); }
            set { SetValue(NameVisibilityProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(ColorPicker),
                new PropertyMetadata(false));

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public ColorPicker()
        {
            InitializeComponent();
        }

        private void Popup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => e.Handled = true;
        private void Popup_MouseRightButtonDown(object sender, MouseButtonEventArgs e) => e.Handled = true;
        private void Popup_MouseMove(object sender, MouseEventArgs e) => e.Handled = true;
        private void Popup_MouseWheel(object sender, MouseWheelEventArgs e) => e.Handled = true;
        private void ColorPickerToggle_Click(object sender, RoutedEventArgs e)
        {
            IsOpen = true;
        }
    }
}
