using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Shapes;
using XSelectionMode = NET.Paint.Drawing.Constant.XSelectionMode;

namespace NET.Paint.View.Component.Drawing.Control
{
    /// <summary>
    /// Interaction logic for Highlighter.xaml
    /// </summary>
    public partial class Highlighter : UserControl
    {
        public Highlighter()
        {
            InitializeComponent();
        }

        public ObservableCollection<Point> Points
        {
            get => (ObservableCollection<Point>)GetValue(PointsProperty);
            set => SetValue(PointsProperty, value);
        }

        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register(nameof(Points), typeof(ObservableCollection<Point>), typeof(Highlighter), new PropertyMetadata(new ObservableCollection<Point>()));

        #region Point Thumbs

        private XSelectionMode _lastSelectionMode;
        private void Thumb_MouseLeave(object sender, MouseEventArgs e) => XTools.Instance.SelectionMode = _lastSelectionMode;
        private void Thumb_MouseEnter(object sender, MouseEventArgs e)
        {
            _lastSelectionMode = XTools.Instance.SelectionMode;
            XTools.Instance.SelectionMode = XSelectionMode.Manipulator;
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb thumb && thumb.DataContext is Point pt)
            {
                var points = Points;

                int index = points.IndexOf(pt);
                if (index < 0) return;

                MovePoint(index, e.HorizontalChange, e.VerticalChange);
                e.Handled = true;
            }
        }
        private void MovePoint(int index, double dx, double dy)
        {
            if (index < 0 || index >= Points.Count)
                return;

            var oldPoint = Points[index];
            var newPoint = new Point(oldPoint.X + dx, oldPoint.Y + dy);

            Points[index] = newPoint;
        }

        #endregion

        #region Rotation Thumb

        private Point _dragStartMousePos;
        private double _initialRotation;

        private void RotateThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            if (sender is Thumb thumb && DataContext is XRenderable renderable)
            {
                if (renderable is IRotateable rotateable)
                {
                    // Get mouse position relative to the main Canvas (not affected by rotation)
                    _dragStartMousePos = Mouse.GetPosition(this);
                    _initialRotation = rotateable.Rotation;
                    
                    // Show the rotation guide line
                    ShowRotationGuideLine(rotateable.Center, _dragStartMousePos);
                }
            }
        }

        private void RotateThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb thumb && DataContext is XRenderable renderable)
            {
                if (renderable is IRotateable rotateable)
                {
                    var currentMousePos = Mouse.GetPosition(this);
                    var centerInCanvas = rotateable.Center;
                    
                    double deltaX = currentMousePos.X - centerInCanvas.X;
                    double deltaY = currentMousePos.Y - centerInCanvas.Y;
                    
                    // Use Atan2 to get angle, then adjust so 0 degrees points up
                    double currentAngleRadians = Math.Atan2(deltaX, -deltaY); // Note: -deltaY to make up = 0
                    double currentAngleDegrees = currentAngleRadians * (180.0 / Math.PI);
                    
                    // Normalize to 0-360 range
                    if (currentAngleDegrees < 0)
                        currentAngleDegrees += 360;
                    
                    // Set the rotation to the absolute angle (not relative to initial rotation)
                    rotateable.Rotation = currentAngleDegrees;
                    
                    // Update the guide line
                    UpdateRotationGuideLine(centerInCanvas, currentMousePos);
                }
            }
        }

        private void RotateThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            // Hide the rotation guide line
            HideRotationGuideLine();
        }

        private void ShowRotationGuideLine(Point center, Point thumbPosition)
        {
            var line = RotationGuideLine;
            line.X1 = center.X;
            line.Y1 = center.Y;
            line.X2 = thumbPosition.X;
            line.Y2 = thumbPosition.Y;
            line.Visibility = Visibility.Visible;
        }

        private void UpdateRotationGuideLine(Point center, Point currentPosition)
        {
            var line = RotationGuideLine;
            line.X1 = center.X;
            line.Y1 = center.Y;
            line.X2 = currentPosition.X;
            line.Y2 = currentPosition.Y;
        }

        private void HideRotationGuideLine()
        {
            RotationGuideLine.Visibility = Visibility.Collapsed;
        }

        #endregion

        #region Text Focus

        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox text)
            {
                text.Focusable = true;
                text.Focus();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox text)
            {
                if (text.DataContext is XText xText)
                    xText.Text = text.Text;
            }
        }

        #endregion

    }
}
