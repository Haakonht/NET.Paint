using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace NET.Paint.Resources.Controls
{
    public class GridCanvas : Canvas
    {
        public static readonly DependencyProperty GridWidthProperty =
            DependencyProperty.Register(nameof(GridWidth), typeof(double), typeof(GridCanvas),
                new FrameworkPropertyMetadata(20.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty GridHeightProperty =
            DependencyProperty.Register(nameof(GridHeight), typeof(double), typeof(GridCanvas),
                new FrameworkPropertyMetadata(20.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty IsGridEnabledProperty =
            DependencyProperty.Register(nameof(IsGridEnabled), typeof(bool), typeof(GridCanvas),
                new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty GridColorProperty =
            DependencyProperty.Register(nameof(GridColor), typeof(Brush), typeof(GridCanvas),
                new FrameworkPropertyMetadata(Brushes.LightGray, FrameworkPropertyMetadataOptions.AffectsRender));


        public double GridWidth
        {
            get => (double)GetValue(GridWidthProperty);
            set => SetValue(GridWidthProperty, value);
        }

        public double GridHeight
        {
            get => (double)GetValue(GridHeightProperty);
            set => SetValue(GridHeightProperty, value);
        }

        public bool IsGridEnabled
        {
            get => (bool)GetValue(IsGridEnabledProperty);
            set => SetValue(IsGridEnabledProperty, value);
        }


        public Brush GridColor
        {
            get => (Brush)GetValue(GridColorProperty);
            set => SetValue(GridColorProperty, value);
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            if (!IsGridEnabled)
                return;

            if (GridWidth <= 0 || GridHeight <= 0)
                return;

            Pen gridPen = new Pen(GridColor, 0.5);

            double width = ActualWidth;
            double height = ActualHeight;

            for (double x = 0; x < width; x += GridWidth)
                dc.DrawLine(gridPen, new Point(x, 0), new Point(x, height));

            for (double y = 0; y < height; y += GridHeight)
                dc.DrawLine(gridPen, new Point(0, y), new Point(width, y));
        }
    }
}
