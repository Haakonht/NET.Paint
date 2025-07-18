using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace NET.Paint.Resources.Controls
{
    public partial class RadialSlider : UserControl
    {
        // Arc parameters (match XAML)
        private const double CenterX = 50;
        private const double CenterY = 50;
        private const double RadiusX = 50;
        private const double RadiusY = 50;
        private const double MinAngle = 0;
        private const double MaxAngle = 180;

        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(RadialSlider),
                new PropertyMetadata(0.0, OnRangeChanged));

        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(RadialSlider),
                new PropertyMetadata(1.0, OnRangeChanged));

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(RadialSlider),
                new PropertyMetadata(0.0, OnValueChanged, CoerceValue));

        public static readonly DependencyProperty TickFrequencyProperty =
            DependencyProperty.Register(nameof(TickFrequency), typeof(double), typeof(RadialSlider),
                new PropertyMetadata(0.01));

        public event RoutedPropertyChangedEventHandler<double> ValueChanged;


        // This is the static callback for the dependency property
        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (RadialSlider)d;
            control.SetThumbPosition(control.ValueToAngle(control.Value));

            // Raise the ValueChanged event
            var args = new RoutedPropertyChangedEventArgs<double>(
                (double)e.OldValue, (double)e.NewValue)
            {
                RoutedEvent = ValueChangedEvent
            };
            control.OnValueChanged(args);
        }

        // RoutedEvent registration (optional, for XAML event hookup)
        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent(
                nameof(ValueChanged),
                RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<double>),
                typeof(RadialSlider));

        // This is the instance method that raises the event
        protected virtual void OnValueChanged(RoutedPropertyChangedEventArgs<double> e)
        {
            ValueChanged?.Invoke(this, e);
        }

        public static readonly DependencyProperty ThumbBackgroundProperty =
            DependencyProperty.Register(
        nameof(ThumbBackground),
        typeof(Brush),
        typeof(RadialSlider),
        new PropertyMetadata(Brushes.DodgerBlue, OnThumbBackgroundChanged));

        public Brush ThumbBackground
        {
            get => (Brush)GetValue(ThumbBackgroundProperty);
            set => SetValue(ThumbBackgroundProperty, value);
        }

        private static void OnThumbBackgroundChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RadialSlider slider && slider.SliderThumb != null)
            {
                slider.SliderThumb.Background = (Brush)e.NewValue;
            }
        }

        public RadialSlider()
        {
            InitializeComponent();
            SetThumbPosition(ValueToAngle(Value));
            SliderThumb.DragDelta += SliderThumb_DragDelta;
            // Ensure initial background is set
            SliderThumb.Background = ThumbBackground;
        }

        public double Minimum
        {
            get => (double)GetValue(MinimumProperty);
            set => SetValue(MinimumProperty, value);
        }

        public double Maximum
        {
            get => (double)GetValue(MaximumProperty);
            set => SetValue(MaximumProperty, value);
        }

        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public double TickFrequency
        {
            get => (double)GetValue(TickFrequencyProperty);
            set => SetValue(TickFrequencyProperty, value);
        }

        private static void OnRangeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (RadialSlider)d;
            control.CoerceValue(ValueProperty);
            control.SetThumbPosition(control.ValueToAngle(control.Value));
        }

        private static object CoerceValue(DependencyObject d, object baseValue)
        {
            var control = (RadialSlider)d;
            double value = (double)baseValue;
            value = Math.Max(control.Minimum, Math.Min(control.Maximum, value));
            if (control.TickFrequency > 0)
            {
                value = control.Minimum + Math.Round((value - control.Minimum) / control.TickFrequency) * control.TickFrequency;
            }
            return value;
        }

        private void SliderThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            // Get mouse position relative to the center
            Point mouse = Mouse.GetPosition(RootCanvas);
            double dx = mouse.X - CenterX;
            double dy = mouse.Y - CenterY;

            // Elliptical angle calculation
            double angle = Math.Atan2(-dy * RadiusX, dx * RadiusY) * 180 / Math.PI;
            angle = Math.Max(MinAngle, Math.Min(MaxAngle, angle));

            // Map angle to value
            double value = AngleToValue(angle);

            // Snap to tick
            if (TickFrequency > 0)
            {
                value = Minimum + Math.Round((value - Minimum) / TickFrequency) * TickFrequency;
            }

            Value = value;
        }

        private void SetThumbPosition(double angle)
        {
            if (SliderThumb == null)
                return;

            double rad = angle * Math.PI / 180;
            double x = CenterX + RadiusX * Math.Cos(rad) - SliderThumb.Width / 2;
            double y = CenterY - RadiusY * Math.Sin(rad) - SliderThumb.Height / 2;

            Canvas.SetLeft(SliderThumb, x);
            Canvas.SetTop(SliderThumb, y);
        }

        private double ValueToAngle(double value)
        {
            double percent = (Maximum == Minimum) ? 0 : (value - Minimum) / (Maximum - Minimum);
            return MinAngle + percent * (MaxAngle - MinAngle);
        }

        private double AngleToValue(double angle)
        {
            double percent = (MaxAngle == MinAngle) ? 0 : (angle - MinAngle) / (MaxAngle - MinAngle);
            return Minimum + percent * (Maximum - Minimum);
        }
    }
}
