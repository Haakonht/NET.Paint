using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.View.Component.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NET.Paint.View.Component.Fragment
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

            // Calculate delta since last mouse move
            Vector delta = currentPos - lastMousePosition;

            lastMousePosition = currentPos;

            // Translate all points by delta
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
    }
}
