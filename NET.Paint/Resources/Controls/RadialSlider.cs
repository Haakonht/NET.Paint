using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NET.Paint.Resources.Controls
    {
    public class RadialSlider : Slider
    {
        private bool _isDragging = false;

        public RadialSlider()
        {
            DefaultStyleKey = typeof(RadialSlider);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (GetTemplateChild("PART_Thumb") is FrameworkElement thumb)
            {
                thumb.MouseLeftButtonDown += Thumb_MouseLeftButtonDown;
                thumb.MouseLeftButtonUp += Thumb_MouseLeftButtonUp;
                thumb.MouseMove += Thumb_MouseMove;
            }

            // Handle clicks on the track
            MouseLeftButtonDown += CircularSlider_MouseLeftButtonDown;
        }

        private void CircularSlider_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!_isDragging)
            {
                UpdateValueFromPosition(e.GetPosition(this));
                CaptureMouse();
                _isDragging = true;
            }
        }

        private void Thumb_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CaptureMouse();
            _isDragging = true;
            e.Handled = true;
        }

        private void Thumb_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
            _isDragging = false;
            e.Handled = true;
        }

        private void Thumb_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && IsMouseCaptured)
            {
                UpdateValueFromPosition(e.GetPosition(this));
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_isDragging && IsMouseCaptured)
            {
                UpdateValueFromPosition(e.GetPosition(this));
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (_isDragging)
            {
                ReleaseMouseCapture();
                _isDragging = false;
            }
            base.OnMouseLeftButtonUp(e);
        }

        private void UpdateValueFromPosition(Point position)
        {
            double centerX = ActualWidth / 2;
            double centerY = ActualHeight / 2;

            double deltaX = position.X - centerX;
            double deltaY = position.Y - centerY;

            double angle = Math.Atan2(deltaY, deltaX);

            // Convert to degrees and normalize to 0-360
            double degrees = angle * 180 / Math.PI;
            degrees += 90; // Adjust so 0 degrees is at top

            if (degrees < 0) degrees += 360;
            if (degrees >= 360) degrees -= 360;

            // Convert to slider value range
            double range = Maximum - Minimum;
            double newValue = Minimum + degrees / 360.0 * range;

            // Ensure value is within bounds
            newValue = Math.Max(Minimum, Math.Min(Maximum, newValue));

            Value = newValue;
        }
    }
}
