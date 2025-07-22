using NET.Paint.Helper;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace NET.Paint.View.Component.Tools.Controls
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public static readonly DependencyProperty SelectedColorProperty =
            DependencyProperty.Register("SelectedColor", typeof(Color), typeof(ColorPicker), 
                new PropertyMetadata(Colors.Black, OnSelectedColorChanged));

        public Color SelectedColor 
        {
            get { return (Color)GetValue(SelectedColorProperty); }
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

        private static void OnSelectedColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ColorPicker colorPicker)
            {
                colorPicker.UpdateUIFromSelectedColor();
            }
        }

        public ColorPicker()
        {
            InitializeComponent();
        }

        private void SLSelector_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border)
            {
                UpdateSLFromMousePosition(border, e.GetPosition(border));
                border.CaptureMouse();
            }
        }

        private void SLSelector_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Border border)
            {
                if (border.IsMouseCaptured)
                    if (e.LeftButton == MouseButtonState.Pressed)
                        UpdateSLFromMousePosition(border, e.GetPosition(border));
                    else
                        border.ReleaseMouseCapture();
            }
        }

        private void UpdateSLFromMousePosition(Border border, Point position)
        {
            // Calculate saturation (0 to 1, left to right)
            double saturation = Math.Max(0, Math.Min(1, position.X / border.ActualWidth));

            // Calculate lightness (1 to 0, top to bottom)
            double lightness = Math.Max(0, Math.Min(1, 1.0 - (position.Y / border.ActualHeight)));

            // Update the indicator position
            Canvas.SetLeft(SLIndicator, position.X - 5);
            Canvas.SetTop(SLIndicator, position.Y - 5);

            // Get current hue and alpha
            double hue = HueSlider.Value;
            double alpha = AlphaSlider.Value;

            SelectedColor = ColorHelper.HSLtoARGB(hue, saturation, lightness, alpha);
        }

        private void HueSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsLoaded)
            {
                UpdateSelectedColorFromUI();
            }
        }

        private void AlphaSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (IsLoaded)
            {
                UpdateSelectedColorFromUI();
            }
        }

        private void UpdateSelectedColorFromUI()
        {
            // Get current HSL values
            var hsl = SelectedColor.ARGBtoHSLA();
            
            // Use current UI values
            double hue = HueSlider.Value;
            double alpha = AlphaSlider.Value;
            
            // Keep current saturation and lightness
            SelectedColor = ColorHelper.HSLtoARGB(hue, hsl.S, hsl.L, alpha);
        }

        private void HueSlider_Loaded(object sender, RoutedEventArgs e)
        {
            // Defer the initial positioning until the layout is complete
            this.Dispatcher.BeginInvoke(new Action(() => 
            {
                UpdateUIFromSelectedColor();
            }), System.Windows.Threading.DispatcherPriority.Loaded);
        }

        private void UpdateUIFromSelectedColor()
        {
            if (SelectedColor != null && SLBorder.ActualWidth > 0 && SLBorder.ActualHeight > 0)
            {
                var color = SelectedColor.ARGBtoHSLA();
                
                // Set slider values
                HueSlider.Value = color.H;
                AlphaSlider.Value = color.A;

                // Calculate SL indicator position
                double x = color.S * SLBorder.ActualWidth;
                double y = (1.0 - color.L) * SLBorder.ActualHeight;

                // Update the indicator position (subtract 5 to center the 10px indicator)
                Canvas.SetLeft(SLIndicator, x - 5);
                Canvas.SetTop(SLIndicator, y - 5);
            }
        }

        private void Popup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => e.Handled = true;
        private void Popup_MouseRightButtonDown(object sender, MouseButtonEventArgs e) => e.Handled = true;
        private void Popup_MouseMove(object sender, MouseEventArgs e) => e.Handled = true;
        private void Popup_MouseWheel(object sender, MouseWheelEventArgs e) => e.Handled = true;
    }
}
