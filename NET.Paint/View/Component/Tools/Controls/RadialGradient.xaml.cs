using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Helper;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace NET.Paint.View.Component.Tools.Controls
{
    public partial class RadialGradient : UserControl
    {
        private Point _thumbMouseDownPosition;
        private bool _isThumbDragging = false;

        public RadialGradient()
        {
            InitializeComponent();
        }

        #region Direction

        private void PreviewEllipse_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                SetRadiusOrCenter(sender);
            else if (e.RightButton == MouseButtonState.Pressed)
                SetRadiusOrCenter(sender, true);
        }
        private void PreviewEllipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) => SetRadiusOrCenter(sender);
        private void PreviewEllipse_MouseRightButtonDown(object sender, MouseButtonEventArgs e) => SetRadiusOrCenter(sender, true);
        private void SetRadiusOrCenter(object sender, bool center = false)
        {
            if (sender is Ellipse ellipse)
            {
                if (DataContext is RadialGradientViewModel radialFill)
                {
                    Point clickedPoint = Mouse.GetPosition(ellipse);

                    if (center)
                    {
                        // Set center as normalized coordinates (0,0 to 1,1)
                        radialFill.Center = new Point
                        {
                            X = clickedPoint.X / ellipse.ActualWidth,
                            Y = clickedPoint.Y / ellipse.ActualHeight
                        };
                    }
                    else
                    {
                        // Calculate radius as distance from center to clicked point
                        Point centerPixels = new Point(
                            radialFill.Center.X * ellipse.ActualWidth,
                            radialFill.Center.Y * ellipse.ActualHeight
                        );

                        // Calculate distance from center to clicked point
                        double deltaX = clickedPoint.X - centerPixels.X;
                        double deltaY = clickedPoint.Y - centerPixels.Y;
                        double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

                        // Normalize radius to ellipse size (use the smaller dimension to ensure it fits)
                        double maxRadius = Math.Min(ellipse.ActualWidth, ellipse.ActualHeight) / 2;
                        radialFill.Radius = Math.Min(distance / maxRadius, 1.0);
                    }

                    UpdatePreview();
                }
            }
        }

        #endregion

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (DataContext is RadialGradientViewModel)
                UpdatePreview();
        }

        private void UpdatePreview()
        {
            if (DataContext is RadialGradientViewModel radialFill)
            {
                if (PreviewEllipse == null) return;

                PreviewEllipse.Fill = new RadialGradientBrush
                {
                    Center = radialFill.Center,
                    RadiusX = radialFill.Radius,
                    RadiusY = radialFill.Radius,
                    GradientStops = new GradientStopCollection(
                        radialFill.GradientStops.Select(gs => new GradientStop(gs.Color, gs.Offset)))
                };

                if (PreviewBorder == null) return;

                PreviewBorder.Background = new LinearGradientBrush
                {
                    StartPoint = new Point(0,0),
                    EndPoint = new Point(1,0),
                    GradientStops = new GradientStopCollection(
                        radialFill.GradientStops.Select(gs => new GradientStop(gs.Color, gs.Offset)))
                };
            }
        }

        private void Thumb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Thumb thumb)
            {
                _isThumbDragging = false;
                _thumbMouseDownPosition = e.GetPosition(thumb);
                thumb.MouseMove += Thumb_MouseMove;
            }
        }

        private void Thumb_MouseMove(object sender, MouseEventArgs e)
        {
            if (sender is Thumb thumb)
            {
                Point currentPosition = e.GetPosition(thumb);
                Vector diff = currentPosition - _thumbMouseDownPosition;

                // Check if the mouse has moved more than the minimum drag distance
                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    _isThumbDragging = true;
                    // Once dragging starts, we no longer need this specific handler
                    thumb.MouseMove -= Thumb_MouseMove;
                }
            }
        }

        private void Thumb_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is Thumb thumb)
            {
                // Unsubscribe from the MouseMove event to clean up
                thumb.MouseMove -= Thumb_MouseMove;

                // If the thumb was not being dragged, treat it as a click
                if (!_isThumbDragging)
                {
                    var colorPicker = GeneralHelper.FindVisualChild<ColorPicker>(thumb);
                    if (colorPicker != null)
                    {
                        colorPicker.IsOpen = !colorPicker.IsOpen;
                        e.Handled = true; // Prevent the thumb from processing the click further
                    }
                }
            }
        }

        private void Thumb_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is Thumb thumb && thumb.DataContext is XGradientStop gradientStop)
            {
                if (DataContext is GradientViewModel gradientFill)
                {
                    gradientFill.GradientStops.Remove(gradientStop);
                    UpdatePreview();
                    e.Handled = true;
                }
            }
        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e) => UpdatePreview();

        private void PreviewBorder_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is GradientViewModel gradientFill)
            {
                double offset = e.GetPosition((IInputElement)sender).X / ((Border)sender).ActualWidth;

                gradientFill.GradientStops.Add(new XGradientStop
                {
                    Color = Colors.White,
                    Offset = offset
                });
                UpdatePreview();
                e.Handled = true;
            }
        }

    }
}
