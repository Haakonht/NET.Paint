using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using XSelectionMode = NET.Paint.Drawing.Constant.XSelectionMode;

namespace NET.Paint.View.Component.Drawing.Controls
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
        private void Thumb_MouseLeave(object sender, MouseEventArgs e) => ToolsViewModel.Instance.SelectionMode = _lastSelectionMode;
        private void Thumb_MouseEnter(object sender, MouseEventArgs e)
        {
            _lastSelectionMode = ToolsViewModel.Instance.SelectionMode;
            ToolsViewModel.Instance.SelectionMode = XSelectionMode.Move;
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
            
            // Check if we're dealing with an XEllipse and apply constraints
            if (DataContext is EllipseViewModel ellipse)
            {
                Point newPoint;
                
                switch (index)
                {
                    case 0: // Center point - allow free movement
                        newPoint = new Point(oldPoint.X + dx, oldPoint.Y + dy);
                        break;
                        
                    case 1: // X-radius control point - only allow X movement
                        newPoint = new Point(oldPoint.X + dx, oldPoint.Y); // Y stays the same
                        break;
                        
                    case 2: // Y-radius control point - only allow Y movement  
                        newPoint = new Point(oldPoint.X, oldPoint.Y + dy); // X stays the same
                        break;
                        
                    default:
                        newPoint = new Point(oldPoint.X + dx, oldPoint.Y + dy);
                        break;
                }
                
                Points[index] = newPoint;
            }
            else if (DataContext is SquareViewModel square)
            {
                Point newPoint;
                
                switch (index)
                {
                    case 0: // Center point - allow free movement
                        newPoint = new Point(oldPoint.X + dx, oldPoint.Y + dy);
                        break;
                        
                    case 1:
                        newPoint = new Point(oldPoint.X + dx, oldPoint.Y); // Y stays the same
                        break;

                    default:
                        newPoint = new Point(oldPoint.X + dx, oldPoint.Y + dy);
                        break;
                }
                
                Points[index] = newPoint;
            }
            else
            {
                // For all other shapes, allow free movement
                var newPoint = new Point(oldPoint.X + dx, oldPoint.Y + dy);
                Points[index] = newPoint;
            }
        }

        #endregion

        #region Rotation Thumb

        private Point _dragStartMousePos;

        private void RotateThumb_MouseEnter(object sender, MouseEventArgs e)
        {
            _lastSelectionMode = ToolsViewModel.Instance.SelectionMode;
            ToolsViewModel.Instance.SelectionMode = XSelectionMode.Rotate;
        }

        private void RotateThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            if (sender is Thumb thumb && DataContext is RenderableViewModel renderable)
            {
                if (renderable is IRotateable rotateable)
                {
                    _dragStartMousePos = Mouse.GetPosition(this);
                    ShowRotationGuideLine(rotateable.Center, _dragStartMousePos);
                }
            }
        }

        private void RotateThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb thumb && DataContext is RenderableViewModel renderable)
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
            e.Handled = true;
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
                if (text.DataContext is TextViewModel xText)
                    xText.Text = text.Text;
            }
        }

        #endregion
    }
}
