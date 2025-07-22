using NET.Paint.Drawing.Command;
using NET.Paint.Drawing.Helper;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Mvvm;
using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Factory;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using NET.Paint.ViewModels.Drawing.Utility;
using NET.Paint.ViewModels.Drawing;

namespace NET.Paint.ViewModels
{
    public class DrawingService : PropertyNotifier
    {
        private bool DEBUG = false;

        private ProjectViewModel _project;
        public ProjectViewModel Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }

        private ImageViewModel? _activeImage = null;
        public ImageViewModel? ActiveImage
        {
            get => _activeImage;
            set => SetProperty(ref _activeImage, value);
        }

        private XCommand _command = null;
        public XCommand Command
        {
            get
            {
                if (_command == null)
                    _command = new XCommand(this);
                return _command;
            }
            set => SetProperty(ref _command, value);
        }

        public ObservableCollection<NotificationViewModel> Notifications { get; } = new ObservableCollection<NotificationViewModel>();
        public ClipboardViewModel Clipboard { get; } = ClipboardViewModel.Instance; 
        public ToolsViewModel Tools { get; } = ToolsViewModel.Instance;
        public PreferencesViewModel Preferences { get; } = new PreferencesViewModel
        {
            OverviewVisible = true,
            ToolboxVisible = true
        };

        public DrawingService()
        {
            // Create sample layers and shapes
            var sampleLayer1 = new VectorLayerViewModel { Title = "Demo Layer" };

            // Row 1: Basic Stroked Shapes
            sampleLayer1.Shapes.Add(new LineViewModel
            {
                Points = new ObservableCollection<Point> { new Point(50, 50), new Point(120, 80) },
                StrokeBrush = Brushes.Purple,
                StrokeThickness = 3
            });

            sampleLayer1.Shapes.Add(new CurveViewModel
            {
                Points = ShapeFactory.CreateCurve(new Point(150, 50), new Point(220, 80)),
                StrokeBrush = Brushes.Orange,
                StrokeThickness = 2
            });

            sampleLayer1.Shapes.Add(new BezierViewModel
            {
                Points = ShapeFactory.CreateBezier(new Point(250, 50), new Point(320, 80)),
                StrokeBrush = Brushes.Magenta,
                StrokeThickness = 2
            });

            sampleLayer1.Shapes.Add(new PolylineViewModel
            {
                Points = new ObservableCollection<Point>
                {
                    new Point(350, 50), new Point(355, 55), new Point(365, 60),
                    new Point(375, 65), new Point(390, 70), new Point(400, 75), new Point(420, 80)
                },
                StrokeBrush = Brushes.Brown,
                StrokeThickness = 2,
                PointSpacing = 5
            });

            // Row 2: Basic Filled Shapes
            sampleLayer1.Shapes.Add(new RectangleViewModel
            {
                Points = new ObservableCollection<Point> { new Point(50, 120), new Point(120, 170) },
                StrokeBrush = Brushes.Blue,
                StrokeThickness = 2,
                FillBrush = Brushes.LightBlue,
                CornerRadius = 5
            });

            sampleLayer1.Shapes.Add(new SquareViewModel
            {
                Points = new ObservableCollection<Point> { new Point(150, 120), new Point(200, 170) },
                StrokeBrush = Brushes.Green,
                StrokeThickness = 2,
                FillBrush = Brushes.LightGreen,
                CornerRadius = 0
            });

            sampleLayer1.Shapes.Add(new EllipseViewModel
            {
                Points = new ObservableCollection<Point> { new Point(230, 120), new Point(300, 120), new Point(230, 170) },
                StrokeBrush = Brushes.Orange,
                StrokeThickness = 2,
                FillBrush = Brushes.LightYellow
            });

            sampleLayer1.Shapes.Add(new CircleViewModel
            {
                Points = new ObservableCollection<Point> { new Point(330, 120), new Point(400, 120) },
                StrokeBrush = Brushes.Red,
                StrokeThickness = 2,
                FillBrush = Brushes.Pink
            });

            // Row 3: Polygons
            sampleLayer1.Shapes.Add(new TriangleViewModel
            {
                Points = ShapeFactory.CreateRegularPolygon(new Point(50, 200), new Point(120, 250), 3),
                StrokeBrush = Brushes.Green,
                StrokeThickness = 2,
                FillBrush = Brushes.LightGreen
            });

            // Pentagon (5-sided regular polygon)
            sampleLayer1.Shapes.Add(new RegularPolygonViewModel
            {
                Points = ShapeFactory.CreateRegularPolygon(new Point(150, 200), new Point(220, 250), 5),
                StrokeBrush = Brushes.DarkBlue,
                StrokeThickness = 2,
                FillBrush = Brushes.CornflowerBlue,
                Corners = 5
            });

            // Hexagon (6-sided regular polygon)
            sampleLayer1.Shapes.Add(new RegularPolygonViewModel
            {
                Points = ShapeFactory.CreateRegularPolygon(new Point(250, 200), new Point(320, 250), 6),
                StrokeBrush = Brushes.DarkGreen,
                StrokeThickness = 2,
                FillBrush = Brushes.LightSeaGreen,
                Corners = 6
            });

            // Octagon (8-sided regular polygon)
            sampleLayer1.Shapes.Add(new RegularPolygonViewModel
            {
                Points = ShapeFactory.CreateRegularPolygon(new Point(350, 200), new Point(420, 250), 8),
                StrokeBrush = Brushes.DarkRed,
                StrokeThickness = 2,
                FillBrush = Brushes.LightSalmon,
                Corners = 8
            });

            // Row 4: Special Polygons
            sampleLayer1.Shapes.Add(new StarViewModel
            {
                Points = ShapeFactory.CreateStar(new Point(50, 280), new Point(120, 330), 5, 0.4),
                StrokeBrush = Brushes.Gold,
                StrokeThickness = 2,
                FillBrush = Brushes.Yellow
            });

            sampleLayer1.Shapes.Add(new HeartViewModel
            {
                Points = ShapeFactory.CreateHeart(new Point(150, 280), new Point(220, 330), 32),
                StrokeBrush = Brushes.DarkRed,
                StrokeThickness = 2,
                FillBrush = Brushes.Red
            });

            sampleLayer1.Shapes.Add(new SpiralViewModel
            {
                Points = ShapeFactory.CreateSpiral(new Point(250, 280), new Point(320, 330), 3, 50),
                StrokeBrush = Brushes.Indigo,
                StrokeThickness = 2,
                FillBrush = Brushes.Lavender
            });

            sampleLayer1.Shapes.Add(new CloudViewModel
            {
                Points = ShapeFactory.CreateCloud(new Point(350, 280), new Point(420, 330), 6, 0.4),
                StrokeBrush = Brushes.Gray,
                StrokeThickness = 2,
                FillBrush = Brushes.LightGray
            });

            // Row 5: Arrow and Text
            sampleLayer1.Shapes.Add(new ArrowViewModel
            {
                Points = ShapeFactory.CreateArrow(new Point(50, 360), new Point(150, 390), 15, 8, 3),
                StrokeBrush = Brushes.DarkSlateBlue,
                StrokeThickness = 2,
                FillBrush = Brushes.SlateBlue
            });

            sampleLayer1.Shapes.Add(new TextViewModel
            {
                Points = new ObservableCollection<Point> { new Point(200, 360) },
                Text = "Sample Text",
                FontFamily = new FontFamily("Arial"),
                FontSize = 16,
                IsBold = true,
                TextColor = Brushes.DarkBlue
            });

            // Create sample images with layers
            var sampleImage1 = new ImageViewModel
            {
                Title = "Demo Image",
                Width = 600,
                Height = 600,
                Background = Colors.White,
                ActiveLayer = sampleLayer1
            };
            sampleImage1.Layers.Clear();
            sampleImage1.Layers.Add(sampleLayer1);

            var sampleImage2 = new ImageViewModel
            {
                Title = "Test Image",
                Width = 1000,
                Height = 800,
                Background = Colors.White
            };

            // Set up the project with images and add some random bitmaps
            Project = new ProjectViewModel
            {
                Title = "Design-Time Project",
                Description = "Sample project for design-time preview",
                Author = "Designer",
                Images = new ObservableCollection<ImageViewModel> { sampleImage1, sampleImage2 }
            };

            //for (int i = 0; i < 5; i++)
            Project.Bitmaps.Add(XHelper.CreateRandomBitmap(200, 200));

            // Add a bitmap shape if random bitmaps are available
            sampleLayer1.Shapes.Add(new BitmapViewModel
            {
                Points = new ObservableCollection<Point> { new Point(350, 360), new Point(420, 410) },
                Source = Project.Bitmaps.First(), // Will reference the first bitmap in the project
                Scaling = XScalingMode.Fit
            });

            // Set the active image and layer
            ActiveImage = sampleImage1;

            // Initialize the command object
            Command = new XCommand(this);
        }
    }
}
