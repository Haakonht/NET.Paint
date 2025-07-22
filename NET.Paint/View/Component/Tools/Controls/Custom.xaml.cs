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
        public ShapePreviewViewModel RegularPreview { get; set; } = new ShapePreviewViewModel();
        public ShapePreviewViewModel ArrowPreview { get; set; } = new ShapePreviewViewModel();
        public ShapePreviewViewModel StarPreview { get; set; } = new ShapePreviewViewModel();
        public ShapePreviewViewModel SpiralPreview { get; set; } = new ShapePreviewViewModel();
        public ShapePreviewViewModel HeartPreview { get; set; } = new ShapePreviewViewModel();
        public ShapePreviewViewModel CloudPreview { get; set; } = new ShapePreviewViewModel();

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
            if (DataContext is not null && DataContext is DesktopViewModel service)
                if (sender is Button && sender is Button button)
                    if (button.Content is Image image)
                        service.Tools.ActiveBitmap = image.Source;
        }

        private void RegularPreviewchanged(object sender, RoutedPropertyChangedEventArgs<double> e) => CreateRegularPreview();
        private void CreateRegularPreview()
        {
            RegularPreview.Shape = new RegularPolygonViewModel
            {
                Points = ShapeFactory.CreateRegularPolygon(
                    new Point(0, 10),
                    new Point(50, 50),
                    ToolsViewModel.Instance.PolygonCorners
                ),
                FillBrush = Brushes.LightGray,
                StrokeBrush = Brushes.Black,
                StrokeThickness = ToolsViewModel.Instance.StrokeThickness,
                StrokeStyle = ToolsViewModel.Instance.StrokeStyle
            };
        }

        private void ArrowPreviewChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => CreateArrowPreview();
        private void CreateArrowPreview()
        {
            ArrowPreview.Shape = new ArrowViewModel
            {
                Points = ShapeFactory.CreateArrow(
                    new Point(0, 0 + (ToolsViewModel.Instance.HeadWidth / 2)),
                    new Point(120, 0 + (ToolsViewModel.Instance.HeadWidth / 2)),
                    ToolsViewModel.Instance.HeadLength,
                    ToolsViewModel.Instance.HeadWidth,
                    ToolsViewModel.Instance.TailWidth
                ),
                FillBrush = Brushes.LightGray,
                StrokeBrush = Brushes.Black,
                StrokeThickness = ToolsViewModel.Instance.StrokeThickness,
                StrokeStyle = ToolsViewModel.Instance.StrokeStyle
            };
        }

        private void SpiralPreviewChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => CreateSpiralPreview();
        private void CreateSpiralPreview()
        {
            SpiralPreview.Shape = new SpiralViewModel
            {
                Points = ShapeFactory.CreateSpiral(
                    new Point(0, 0),
                    new Point(70, 70),
                    ToolsViewModel.Instance.Turns,
                    ToolsViewModel.Instance.SpiralSamples
                ),
                FillBrush = Brushes.LightGray,
                StrokeBrush = Brushes.Black,
                StrokeThickness = ToolsViewModel.Instance.StrokeThickness,
                StrokeStyle = ToolsViewModel.Instance.StrokeStyle
            };
        }

        private void StarPreviewChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => CreateStarPreview();
        private void CreateStarPreview()
        {
            StarPreview.Shape = new StarViewModel
            {
                Points = ShapeFactory.CreateStar(
                    new Point(0, 10),
                    new Point(60, 60),
                    ToolsViewModel.Instance.StarPoints,
                    ToolsViewModel.Instance.StarInnerRadiusRatio
                ),
                FillBrush = Brushes.LightGray,
                StrokeBrush = Brushes.Black,
                StrokeThickness = ToolsViewModel.Instance.StrokeThickness,
                StrokeStyle = ToolsViewModel.Instance.StrokeStyle
            };
        }

        private void HeartPreviewChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => CreateHeartPreview();
        private void CreateHeartPreview()
        {
            HeartPreview.Shape = new HeartViewModel
            {
                Points = ShapeFactory.CreateHeart(
                    new Point(0, 0),
                    new Point(60, 60),
                    ToolsViewModel.Instance.HeartSamples
                ),
                FillBrush = Brushes.LightGray,
                StrokeBrush = Brushes.Black,
                StrokeThickness = ToolsViewModel.Instance.StrokeThickness,
                StrokeStyle = ToolsViewModel.Instance.StrokeStyle
            };
        }

        private void CloudPreviewChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => CreateCloudPreview();
        private void CreateCloudPreview()
        {
            CloudPreview.Shape = new CloudVIewModel
            {
                Points = ShapeFactory.CreateCloud(
                    new Point(0, 0),
                    new Point(60, 60),
                    ToolsViewModel.Instance.CloudBumps,
                    ToolsViewModel.Instance.BumpVariance
                ),
                FillBrush = Brushes.LightGray,
                StrokeBrush = Brushes.Black,
                StrokeThickness = ToolsViewModel.Instance.StrokeThickness,
                StrokeStyle = ToolsViewModel.Instance.StrokeStyle
            };
        }
    }
}
