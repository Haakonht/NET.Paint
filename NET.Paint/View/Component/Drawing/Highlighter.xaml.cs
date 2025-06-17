using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NET.Paint.View.Component.Fragment
{
    /// <summary>
    /// Interaction logic for Highlighter.xaml
    /// </summary>
    public partial class Highlighter : UserControl
    {
        private IRotateable? _rotateable = null;

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

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb thumb && thumb.DataContext is Point pt)
            {
                var points = Points; 

                int index = points.IndexOf(pt);
                if (index < 0) return;

                MovePoint(index, e.HorizontalChange, e.VerticalChange);
            }
        }

        void MovePoint(int index, double dx, double dy)
        {
            if (index < 0 || index >= Points.Count)
                return;

            var oldPoint = Points[index];
            var newPoint = new Point(oldPoint.X + dx, oldPoint.Y + dy);

            Points[index] = newPoint;
        }

        private double _dragStartY;

        private void RotateThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            if (sender is Thumb thumb)
            {
                // Get mouse position relative to the control or screen
                _dragStartY = Mouse.GetPosition(thumb).Y;
            }
        }

        private void RotateThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (sender is Thumb thumb && DataContext is XImage image)
            {
                if (image.Selected is IRotateable rotateable)
                {
                    double currentY = Mouse.GetPosition(thumb).Y;
                    double offset = _dragStartY - currentY;  // Positive if moved up, negative if moved down

                    // Rotate 1 degree per 10 pixels offset:
                    double rotationDegrees = offset / 4;

                    // Set absolute rotation relative to start rotation at drag start (optional)
                    rotateable.Rotation = rotationDegrees;
                }
            }
        }

        private bool isDragging = false;
        private Point lastMousePosition;

        private void ContentPresenter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            lastMousePosition = e.GetPosition((ContentPresenter)sender);
            ((ContentPresenter)sender).CaptureMouse();
        }

        private void ContentPresenter_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging) return;

            var presenter = (ContentPresenter)sender;
            Point currentPos = e.GetPosition(presenter);
            Vector delta = currentPos - lastMousePosition;
            lastMousePosition = currentPos;

            for (int i = 0; i < Points.Count; i++)
            {
                var p = Points[i];
                Points[i] = new Point(p.X + delta.X, p.Y + delta.Y);
            }           
        }

        private void ContentPresenter_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            ((ContentPresenter)sender).ReleaseMouseCapture();
        }

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
    }
}
