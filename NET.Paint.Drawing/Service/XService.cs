using NET.Paint.Drawing.Command;
using NET.Paint.Drawing.Helper;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Mvvm;
using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Factory;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Service
{
    public class XService : PropertyNotifier
    {
        private XProject _project = new XProject
        {
            Title = "Test Project",
            Description = "This is a test project",
            Author = "Håkon Torgersen",
            Images = new ObservableCollection<XImage>{ new XImage { Title = "Test 1" }, new XImage { Title = "Test 2" } },
            Created = DateTime.Now
        };
        public XProject Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }

        private XImage? _activeImage = null;
        public XImage? ActiveImage
        {
            get => _activeImage;
            set => SetProperty(ref _activeImage, value);
        }

        public ObservableCollection<XNotification> Notifications { get; } = new ObservableCollection<XNotification>() { new XNotification { Source = XNotificationSource.Clipboard, Message = "Items have been added to the clipboard"} };
        public XClipboard Clipboard { get; } = XClipboard.Instance; 
        public XTools Tools { get; } = XTools.Instance;
        public XPreferences Preferences { get; } = new XPreferences();
        public XCommand Command { get; }

        private Color _test = Colors.Green;
        public Color Test
        {
            get => _test;
            set => SetProperty(ref _test, value);
        }

        public XService()
        {
            // Create sample layers and shapes
            var sampleLayer1 = new XVectorLayer { Title = "Demo Layer" };

            // Row 1: Basic Stroked Shapes
            sampleLayer1.Shapes.Add(new XLine
            {
                Points = new ObservableCollection<Point> { new Point(50, 50), new Point(120, 80) },
                StrokeBrush = Brushes.Purple,
                StrokeThickness = 3
            });

            sampleLayer1.Shapes.Add(new XCurve
            {
                Points = XFactory.CreateCurve(new Point(150, 50), new Point(220, 80)),
                StrokeBrush = Brushes.Orange,
                StrokeThickness = 2
            });

            sampleLayer1.Shapes.Add(new XBezier
            {
                Points = XFactory.CreateBezier(new Point(250, 50), new Point(320, 80)),
                StrokeBrush = Brushes.Magenta,
                StrokeThickness = 2
            });

            sampleLayer1.Shapes.Add(new XPolyline
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
            sampleLayer1.Shapes.Add(new XRectangle
            {
                Points = new ObservableCollection<Point> { new Point(50, 120), new Point(120, 170) },
                StrokeBrush = Brushes.Blue,
                StrokeThickness = 2,
                FillBrush = Brushes.LightBlue,
                CornerRadius = 5
            });

            sampleLayer1.Shapes.Add(new XSquare
            {
                Points = new ObservableCollection<Point> { new Point(150, 120), new Point(200, 170) },
                StrokeBrush = Brushes.Green,
                StrokeThickness = 2,
                FillBrush = Brushes.LightGreen,
                CornerRadius = 0
            });

            sampleLayer1.Shapes.Add(new XEllipse
            {
                Points = new ObservableCollection<Point> { new Point(230, 120), new Point(300, 120), new Point(230, 170) },
                StrokeBrush = Brushes.Orange,
                StrokeThickness = 2,
                FillBrush = Brushes.LightYellow
            });

            sampleLayer1.Shapes.Add(new XCircle
            {
                Points = new ObservableCollection<Point> { new Point(330, 120), new Point(400, 120) },
                StrokeBrush = Brushes.Red,
                StrokeThickness = 2,
                FillBrush = Brushes.Pink
            });

            // Row 3: Polygons
            sampleLayer1.Shapes.Add(new XTriangle
            {
                Points = XFactory.CreateRegularPolygon(new Point(50, 200), new Point(120, 250), 3),
                StrokeBrush = Brushes.Green,
                StrokeThickness = 2,
                FillBrush = Brushes.LightGreen
            });

            // Pentagon (5-sided regular polygon)
            sampleLayer1.Shapes.Add(new XRegular
            {
                Points = XFactory.CreateRegularPolygon(new Point(150, 200), new Point(220, 250), 5),
                StrokeBrush = Brushes.DarkBlue,
                StrokeThickness = 2,
                FillBrush = Brushes.CornflowerBlue,
                Corners = 5
            });

            // Hexagon (6-sided regular polygon)
            sampleLayer1.Shapes.Add(new XRegular
            {
                Points = XFactory.CreateRegularPolygon(new Point(250, 200), new Point(320, 250), 6),
                StrokeBrush = Brushes.DarkGreen,
                StrokeThickness = 2,
                FillBrush = Brushes.LightSeaGreen,
                Corners = 6
            });

            // Octagon (8-sided regular polygon)
            sampleLayer1.Shapes.Add(new XRegular
            {
                Points = XFactory.CreateRegularPolygon(new Point(350, 200), new Point(420, 250), 8),
                StrokeBrush = Brushes.DarkRed,
                StrokeThickness = 2,
                FillBrush = Brushes.LightSalmon,
                Corners = 8
            });

            // Row 4: Special Polygons
            sampleLayer1.Shapes.Add(new XStar
            {
                Points = XFactory.CreateStar(new Point(50, 280), new Point(120, 330), 5, 0.4),
                StrokeBrush = Brushes.Gold,
                StrokeThickness = 2,
                FillBrush = Brushes.Yellow
            });

            sampleLayer1.Shapes.Add(new XHeart
            {
                Points = XFactory.CreateHeart(new Point(150, 280), new Point(220, 330), 32),
                StrokeBrush = Brushes.DarkRed,
                StrokeThickness = 2,
                FillBrush = Brushes.Red
            });

            sampleLayer1.Shapes.Add(new XSpiral
            {
                Points = XFactory.CreateSpiral(new Point(250, 280), new Point(320, 330), 3, 50),
                StrokeBrush = Brushes.Indigo,
                StrokeThickness = 2,
                FillBrush = Brushes.Lavender
            });

            sampleLayer1.Shapes.Add(new XCloud
            {
                Points = XFactory.CreateCloud(new Point(350, 280), new Point(420, 330), 6, 0.4),
                StrokeBrush = Brushes.Gray,
                StrokeThickness = 2,
                FillBrush = Brushes.LightGray
            });

            // Row 5: Arrow and Text
            sampleLayer1.Shapes.Add(new XArrow
            {
                Points = XFactory.CreateArrow(new Point(50, 360), new Point(150, 390), 15, 8, 3),
                StrokeBrush = Brushes.DarkSlateBlue,
                StrokeThickness = 2,
                FillBrush = Brushes.SlateBlue
            });

            sampleLayer1.Shapes.Add(new XText
            {
                Points = new ObservableCollection<Point> { new Point(200, 360) },
                Text = "Sample Text",
                FontFamily = new FontFamily("Arial"),
                FontSize = 16,
                IsBold = true,
                TextColor = Brushes.DarkBlue
            }); 

            // Create sample images with layers
            var sampleImage1 = new XImage
            {
                Title = "Demo Image",
                Width = 600,
                Height = 600,
                Background = Colors.White,
                ActiveLayer = sampleLayer1
            };
            sampleImage1.Layers.Clear();
            sampleImage1.Layers.Add(sampleLayer1);

            var sampleImage2 = new XImage
            {
                Title = "Test Image",
                Width = 1000,
                Height = 800,
                Background = Colors.White
            };

            // Set up the project with images
            Project = new XProject
            {
                Title = "Design-Time Project",
                Description = "Sample project for design-time preview",
                Author = "Designer",
                Images = new ObservableCollection<XImage> { sampleImage1, sampleImage2 }
            };

            // Add some random bitmaps
            for (int i = 0; i < 5; i++)
                Project.Bitmaps.Add(XHelper.CreateRandomBitmap(200, 200));

            // Add a bitmap shape if random bitmaps are available
            sampleLayer1.Shapes.Add(new XBitmap
            {
                Points = new ObservableCollection<Point> { new Point(350, 360), new Point(420, 410) },
                Source = Project.Bitmaps.First(), // Will reference the first bitmap in the project
                Scaling = XScalingMode.Fit
            });

            // Set the active image and layer
            ActiveImage = sampleImage1;

            // Set preferences if needed for visibility
            Preferences = new XPreferences
            {
                OverviewVisible = true,
                ToolboxVisible = true
            };

            // Initialize the command object
            Command = new XCommand(this);
        }
    }
}
