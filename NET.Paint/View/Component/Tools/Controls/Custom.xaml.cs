using NET.Paint.Drawing.Factory;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NET.Paint.View.Component.Tools.Controls
{
    /// <summary>
    /// Interaction logic for Contextual.xaml
    /// </summary>
    public partial class Custom : UserControl
    {
        public XPreview RegularPreview { get; set; } = new XPreview();
        public XPreview ArrowPreview { get; set; } = new XPreview();
        public XPreview StarPreview { get; set; } = new XPreview();
        public XPreview SpiralPreview { get; set; } = new XPreview();
        public XPreview HeartPreview { get; set; } = new XPreview();
        public XPreview CloudPreview { get; set; } = new XPreview();

        public Custom()
        {
            InitializeComponent();
            CreateRegularPreview();
            CreateArrowPreview();
            CreateSpiralPreview();
            CreateStarPreview();
            CreateHeartPreview();
            CreateCloudPreview();
        }

        private void SelectBitmap(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is not null && DataContext is XService service)
                if (sender is Button && sender is Button button)
                    if (button.Content is Image image)
                        service.Tools.ActiveBitmap = image.Source;
        }

        private void RegularPreviewchanged(object sender, RoutedPropertyChangedEventArgs<double> e) => CreateRegularPreview();
        private void CreateRegularPreview()
        {
            RegularPreview.Shape = new XRegular
            {
                Points = XFactory.CreateRegularPolygon(
                    new Point(0, 10),
                    new Point(50, 50),
                    XTools.Instance.PolygonCorners
                ),
                FillBrush = Brushes.LightGray,
                StrokeBrush = Brushes.Black,
                StrokeThickness = XTools.Instance.StrokeThickness,
                StrokeStyle = XTools.Instance.StrokeStyle
            };
        }

        private void ArrowPreviewChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => CreateArrowPreview();
        private void CreateArrowPreview()
        {
            ArrowPreview.Shape = new XArrow
            {
                Points = XFactory.CreateArrow(
                    new Point(0, 0 + (XTools.Instance.HeadWidth / 2)),
                    new Point(120, 0 + (XTools.Instance.HeadWidth / 2)),
                    XTools.Instance.HeadLength,
                    XTools.Instance.HeadWidth,
                    XTools.Instance.TailWidth
                ),
                FillBrush = Brushes.LightGray,
                StrokeBrush = Brushes.Black,
                StrokeThickness = XTools.Instance.StrokeThickness,
                StrokeStyle = XTools.Instance.StrokeStyle
            };
        }

        private void SpiralPreviewChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => CreateSpiralPreview();
        private void CreateSpiralPreview()
        {
            SpiralPreview.Shape = new XSpiral
            {
                Points = XFactory.CreateSpiral(
                    new Point(0, 0),
                    new Point(70, 70),
                    XTools.Instance.Turns,
                    XTools.Instance.SpiralSamples
                ),
                FillBrush = Brushes.LightGray,
                StrokeBrush = Brushes.Black,
                StrokeThickness = XTools.Instance.StrokeThickness,
                StrokeStyle = XTools.Instance.StrokeStyle
            };
        }

        private void StarPreviewChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => CreateStarPreview();
        private void CreateStarPreview()
        {
            StarPreview.Shape = new XStar
            {
                Points = XFactory.CreateStar(
                    new Point(0, 10),
                    new Point(60, 60),
                    XTools.Instance.StarPoints,
                    XTools.Instance.StarInnerRadiusRatio
                ),
                FillBrush = Brushes.LightGray,
                StrokeBrush = Brushes.Black,
                StrokeThickness = XTools.Instance.StrokeThickness,
                StrokeStyle = XTools.Instance.StrokeStyle
            };
        }

        private void HeartPreviewChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => CreateHeartPreview();
        private void CreateHeartPreview()
        {
            HeartPreview.Shape = new XHeart
            {
                Points = XFactory.CreateHeart(
                    new Point(0, 0),
                    new Point(60, 60),
                    XTools.Instance.HeartSamples
                ),
                FillBrush = Brushes.LightGray,
                StrokeBrush = Brushes.Black,
                StrokeThickness = XTools.Instance.StrokeThickness,
                StrokeStyle = XTools.Instance.StrokeStyle
            };
        }

        private void CloudPreviewChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => CreateCloudPreview();
        private void CreateCloudPreview()
        {
            CloudPreview.Shape = new XCloud
            {
                Points = XFactory.CreateCloud(
                    new Point(0, 0),
                    new Point(60, 60),
                    XTools.Instance.CloudBumps,
                    XTools.Instance.BumpVariance
                ),
                FillBrush = Brushes.LightGray,
                StrokeBrush = Brushes.Black,
                StrokeThickness = XTools.Instance.StrokeThickness,
                StrokeStyle = XTools.Instance.StrokeStyle
            };
        }
    }
}
