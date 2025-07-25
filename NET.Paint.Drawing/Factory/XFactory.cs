using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using XSelectionMode = NET.Paint.Drawing.Constant.XSelectionMode;

namespace NET.Paint.Drawing.Factory
{
    public static class XFactory
    {
        public static XRenderable? CreateShape(XTools tools)
        {
            switch (tools.ActiveTool)
            {
                case XToolType.Polyline:
                    if (tools.PolylineMode == XPolylineMode.Add)
                        return new XPolyline
                        {
                            Points = new ObservableCollection<Point> { tools.ClickLocation },
                            Stroke = tools.Stroke,
                            StrokeThickness = tools.StrokeThickness,
                            StrokeStyle = tools.StrokeStyle.DashArray,
                            PointSpacing = tools.PencilSpacing
                        };
                    return null;
                case XToolType.Line:
                    return new XLine
                    {
                        Points = new ObservableCollection<Point>() { tools.ClickLocation, tools.MouseLocation },
                        Stroke = tools.Stroke,
                        StrokeThickness = tools.StrokeThickness,
                        StrokeStyle = tools.StrokeStyle.DashArray
                    };
                case XToolType.Curve:
                    return new XCurve
                    {
                        Points = Tools.CreateCurve(tools.ClickLocation, tools.MouseLocation),
                        Stroke = tools.Stroke,
                        StrokeThickness = tools.StrokeThickness,
                        StrokeStyle = tools.StrokeStyle.DashArray
                    };
                case XToolType.Bezier:
                    return new XBezier
                    {
                        Points = Tools.CreateBezier(tools.ClickLocation, tools.MouseLocation),
                        Stroke = tools.Stroke,
                        StrokeThickness = tools.StrokeThickness,
                        StrokeStyle = tools.StrokeStyle.DashArray
                    };
                case XToolType.Ellipse:
                    Point center = new Point((tools.ClickLocation.X + tools.MouseLocation.X) / 2, (tools.ClickLocation.Y + tools.MouseLocation.Y) / 2);
                    if (tools.ActiveEllipse == XEllipseStyle.Circle)
                        return new XCircle
                        {
                            Points = new ObservableCollection<Point>() { center, new Point(tools.MouseLocation.X, center.Y) },
                            Stroke = tools.Stroke,
                            StrokeThickness = tools.StrokeThickness,
                            Fill = tools.Fill,
                            StrokeStyle = tools.StrokeStyle.DashArray
                        };
                    else
                        return new XEllipse
                        {
                            Points = new ObservableCollection<Point>() { center, new Point(tools.MouseLocation.X, center.Y), new Point(center.X, tools.MouseLocation.Y) },
                            Stroke = tools.Stroke,
                            StrokeThickness = tools.StrokeThickness,
                            Fill = tools.Fill,
                            StrokeStyle = tools.StrokeStyle.DashArray
                        };
                case XToolType.Triangle:
                    return new XTriangle
                    {
                        Points = Tools.CreateRegularPolygon(tools.ClickLocation, tools.MouseLocation, 3),
                        Stroke = tools.Stroke,
                        StrokeThickness = tools.StrokeThickness,
                        Fill = tools.Fill,
                        StrokeStyle = tools.StrokeStyle.DashArray
                    };
                case XToolType.Rectangle:
                    if (tools.ActiveRectangle == XRectangleStyle.Square)
                    {
                        center = new Point((tools.ClickLocation.X + tools.MouseLocation.X) / 2, (tools.ClickLocation.Y + tools.MouseLocation.Y) / 2);
                        return new XSquare
                        {
                            Points = new ObservableCollection<Point>() { center, new Point(tools.MouseLocation.X, center.Y) },
                            Stroke = tools.Stroke,
                            StrokeThickness = tools.StrokeThickness,
                            Fill = tools.Fill,
                            StrokeStyle = tools.StrokeStyle.DashArray,
                            CornerRadius = tools.Radius
                        };
                    }

                    else
                        return new XRectangle
                        {
                            Points = new ObservableCollection<Point>() { tools.ClickLocation, tools.MouseLocation },
                            Stroke = tools.Stroke,
                            StrokeThickness = tools.StrokeThickness,
                            Fill = tools.Fill,
                            StrokeStyle = tools.StrokeStyle.DashArray,
                            CornerRadius = tools.Radius
                        };
                case XToolType.Polygon:
                    switch (tools.ActivePolygon)
                    {
                        case XPolygonStyle.Regular:
                            return new XRegular
                            {
                                Points = Tools.CreateRegularPolygon(tools.ClickLocation, tools.MouseLocation, tools.PolygonCorners),
                                Stroke = tools.Stroke,
                                StrokeThickness = tools.StrokeThickness,
                                Fill = tools.Fill,
                                StrokeStyle = tools.StrokeStyle.DashArray,
                                Corners = tools.PolygonCorners
                            };
                        case XPolygonStyle.Star:
                            return new XStar
                            {
                                Points = Tools.CreateStar(tools.ClickLocation, tools.MouseLocation, tools.StarPoints, tools.StarInnerRadiusRatio),
                                Stroke = tools.Stroke,
                                StrokeThickness = tools.StrokeThickness,
                                Fill = tools.Fill,
                                StrokeStyle = tools.StrokeStyle.DashArray
                            };
                        case XPolygonStyle.Heart:
                            return new XHeart
                            {
                                Points = Tools.CreateHeart(tools.ClickLocation, tools.MouseLocation, tools.HeartSamples),
                                Stroke = tools.Stroke,
                                StrokeThickness = tools.StrokeThickness,
                                Fill = tools.Fill,
                                StrokeStyle = tools.StrokeStyle.DashArray
                            };
                        case XPolygonStyle.Spiral:
                            return new XSpiral
                            {
                                Points = Tools.CreateSpiral(tools.ClickLocation, tools.MouseLocation, tools.Turns, tools.SpiralSamples),
                                Stroke = tools.Stroke,
                                StrokeThickness = tools.StrokeThickness,
                                Fill = tools.Fill,
                                StrokeStyle = tools.StrokeStyle.DashArray
                            };
                        case XPolygonStyle.Cloud:
                            return new XCloud
                            {
                                Points = Tools.CreateCloud(tools.ClickLocation, tools.MouseLocation, tools.CloudBumps, tools.BumpVariance),
                                Stroke = tools.Stroke,
                                StrokeThickness = tools.StrokeThickness,
                                Fill = tools.Fill,
                                StrokeStyle = tools.StrokeStyle.DashArray
                            };
                        default:
                            return new XArrow
                            {
                                Points = Tools.CreateArrow(tools.ClickLocation, tools.MouseLocation, tools.HeadLength, tools.HeadWidth, tools.TailWidth),
                                Stroke = tools.Stroke,
                                StrokeThickness = tools.StrokeThickness,
                                Fill = tools.Fill,
                                StrokeStyle = tools.StrokeStyle.DashArray
                            };
                    }
                case XToolType.Text:
                    return new XText
                    {
                        Points = new ObservableCollection<Point> { tools.MouseLocation },
                        TextColor = tools.Stroke,
                        FontFamily = tools.FontFamily.Source,
                        FontSize = tools.FontSize,
                        IsBold = tools.IsBold,
                        IsItalic = tools.IsItalic,
                        IsStrikethrough = tools.IsStrikethrough,
                        IsUnderline = tools.IsUnderline,
                        Text = ""
                    };
                case XToolType.Bitmap:
                    if (tools.ActiveBitmap == null)
                        return null;
                    return new XBitmap
                    {
                        Source = tools.ActiveBitmap,
                        Scaling = tools.BitmapScaling,
                        Points = new ObservableCollection<Point> { tools.ClickLocation, tools.MouseLocation },
                    };
                case XToolType.Effect:
                    return new XEffect
                    {
                        Points = new ObservableCollection<Point>() { tools.ClickLocation, tools.MouseLocation },
                        Effect = new DropShadowEffect
                        {
                            Color = Colors.Red,
                            BlurRadius = 10,
                            ShadowDepth = 2,
                            Opacity = 1.0
                        }
                    };
                case XToolType.Selector:
                    switch (tools.SelectionMode)
                    {
                        case XSelectionMode.Rectangle:
                            return new XRectangle
                            {
                                Points = new ObservableCollection<Point>() { tools.ClickLocation, tools.MouseLocation },
                                Stroke = new XSolidColor { Color = Colors.Black },
                                StrokeThickness = 2,
                                Fill = new XSolidColor { Color = System.Windows.Media.Color.FromArgb(51, 255, 255, 255) },
                                StrokeStyle = XOptions.StrokeStyleOptions[2].DashArray,
                                CornerRadius = 0,
                            };
                        case XSelectionMode.Lasso:
                            return new XPolyline
                            {
                                Points = new ObservableCollection<Point> { tools.ClickLocation },
                                Stroke = new XSolidColor { Color = Colors.Black },
                                StrokeThickness = 2,
                                StrokeStyle = XOptions.StrokeStyleOptions[2].DashArray,
                                PointSpacing = 20
                            };
                        default:
                            return null;
                    }
                default:
                    return null;
            }
        }

        #region Tools

        public static class Tools
        {
            public static ObservableCollection<Point> CreateRegularPolygon(Point? start, Point? end, int corners)
            {
                if (start == null || end == null || corners < 3)
                    return new ObservableCollection<Point>();

                double x1 = Math.Min(start.Value.X, end.Value.X);
                double y1 = Math.Min(start.Value.Y, end.Value.Y);
                double x2 = Math.Max(start.Value.X, end.Value.X);
                double y2 = Math.Max(start.Value.Y, end.Value.Y);

                double centerX = (x1 + x2) / 2;
                double centerY = (y1 + y2) / 2;
                double width = x2 - x1;
                double height = y2 - y1;

                double angleStep = 2 * Math.PI / corners;
                double startAngle = -Math.PI / 2; // Pointing up

                double radiusBasedOnWidth = width / (2 * Math.Cos(Math.PI / corners));
                double radiusBasedOnHeight = height / (2 * Math.Sin(Math.PI / corners));

                double radius = Math.Min(radiusBasedOnWidth, radiusBasedOnHeight);

                return new ObservableCollection<Point>(
                    Enumerable.Range(0, corners).Select(i =>
                        new Point(
                            centerX + radius * Math.Cos(startAngle + i * angleStep),
                            centerY + radius * Math.Sin(startAngle + i * angleStep)
                        )
                    )
                );
            }

            public static ObservableCollection<Point> CreateStar(Point? start, Point? end, int corners, double innerRadiusRatio = 0.5)
            {
                if (start == null || end == null || corners < 2 || innerRadiusRatio <= 0 || innerRadiusRatio >= 1)
                    return new ObservableCollection<Point>();

                double x1 = Math.Min(start.Value.X, end.Value.X);
                double y1 = Math.Min(start.Value.Y, end.Value.Y);
                double x2 = Math.Max(start.Value.X, end.Value.X);
                double y2 = Math.Max(start.Value.Y, end.Value.Y);

                double centerX = (x1 + x2) / 2;
                double centerY = (y1 + y2) / 2;
                double width = x2 - x1;
                double height = y2 - y1;

                // Angle between the points (outer + inner), so total points = corners * 2
                double angleStep = Math.PI / corners;
                double startAngle = -Math.PI / 2; // Pointing up

                // Calculate outer radius same as polygon logic, ensuring star fits bounding box
                double outerRadiusBasedOnWidth = width / (2 * Math.Cos(Math.PI / corners));
                double outerRadiusBasedOnHeight = height / (2 * Math.Sin(Math.PI / corners));
                double outerRadius = Math.Min(outerRadiusBasedOnWidth, outerRadiusBasedOnHeight);

                // Inner radius is a ratio of outer radius
                double innerRadius = outerRadius * innerRadiusRatio;

                var points = new ObservableCollection<Point>();

                for (int i = 0; i < corners * 2; i++)
                {
                    double radius = (i % 2 == 0) ? outerRadius : innerRadius;
                    double angle = startAngle + i * angleStep;

                    points.Add(new Point(
                        centerX + radius * Math.Cos(angle),
                        centerY + radius * Math.Sin(angle)
                    ));
                }

                return points;
            }

            public static ObservableCollection<Point> CreateCloud(Point? start, Point? end, int bumpCount = 8, double bumpVariation = 0.3)
            {
                if (start == null || end == null || bumpCount < 3)
                    return new ObservableCollection<Point>();

                double x1 = Math.Min(start.Value.X, end.Value.X);
                double y1 = Math.Min(start.Value.Y, end.Value.Y);
                double x2 = Math.Max(start.Value.X, end.Value.X);
                double y2 = Math.Max(start.Value.Y, end.Value.Y);

                double centerX = (x1 + x2) / 2;
                double centerY = (y1 + y2) / 2;
                double width = x2 - x1;
                double height = y2 - y1;

                var points = new ObservableCollection<Point>();
                Random random = new Random();

                // Create base elliptical cloud body points
                double baseRadiusX = width * 0.3;
                double baseRadiusY = height * 0.3;

                // First, generate the base perimeter points
                var basePoints = new List<Point>();
                for (int i = 0; i < bumpCount; i++)
                {
                    double angle = 2 * Math.PI * i / bumpCount;
                    double x = centerX + baseRadiusX * Math.Cos(angle);
                    double y = centerY + baseRadiusY * Math.Sin(angle);
                    basePoints.Add(new Point(x, y));
                }

                // Now create the cloud outline with outward extensions
                for (int i = 0; i < basePoints.Count; i++)
                {
                    Point current = basePoints[i];
                    Point next = basePoints[(i + 1) % basePoints.Count]; // Wrap around to first point

                    // Add the current base point
                    points.Add(current);

                    // Calculate the midpoint of the line segment
                    Point midPoint = new Point(
                        (current.X + next.X) / 2,
                        (current.Y + next.Y) / 2
                    );

                    // Calculate the direction from cloud center to midpoint (outward direction)
                    Vector outwardDir = new Vector(
                        midPoint.X - centerX,
                        midPoint.Y - centerY
                    );
                    outwardDir.Normalize();

                    // Randomize the extension length
                    double extensionLength = (width + height) / 16 * (0.4 + random.NextDouble() * bumpVariation);

                    // Create the outward extension point
                    Point extensionPoint = new Point(
                        midPoint.X + outwardDir.X * extensionLength,
                        midPoint.Y + outwardDir.Y * extensionLength
                    );

                    // Add the extension point
                    points.Add(extensionPoint);
                }

                return points;
            }

            public static ObservableCollection<Point> CreateHeart(Point? start, Point? end, int samples = 64)
            {
                if (start == null || end == null)
                    return new ObservableCollection<Point>();

                double x1 = Math.Min(start.Value.X, end.Value.X);
                double y1 = Math.Min(start.Value.Y, end.Value.Y);
                double x2 = Math.Max(start.Value.X, end.Value.X);
                double y2 = Math.Max(start.Value.Y, end.Value.Y);

                double width = x2 - x1;
                double height = y2 - y1;
                double centerX = (x1 + x2) / 2;
                double centerY = (y1 + y2) / 2;

                var points = new ObservableCollection<Point>();
                // Parametric heart: x = 16 sin^3 t, y = 13 cos t - 5 cos 2t - 2 cos 3t - cos 4t
                // We'll normalize and scale to fit the bounding box
                for (int i = 0; i < samples; i++)
                {
                    double t = Math.PI - (2 * Math.PI * i / (samples - 1)); // t from PI to -PI for upright heart
                    double x = 16 * Math.Pow(Math.Sin(t), 3);
                    double y = 13 * Math.Cos(t) - 5 * Math.Cos(2 * t) - 2 * Math.Cos(3 * t) - Math.Cos(4 * t);

                    // Normalize to [-1,1] for x and y
                    x /= 17; // max abs(x) is 16
                    y /= 17; // max abs(y) is about 17

                    // Scale to bounding box
                    double px = centerX + x * width / 2;
                    double py = centerY - y * height / 2; // minus because y increases downward in screen coords

                    points.Add(new Point(px, py));
                }

                return points;
            }

            public static ObservableCollection<Point> CreateSpiral(Point? start, Point? end, int turns = 3, int samples = 200)
            {
                if (start == null || end == null || turns < 1 || samples < 2)
                    return new ObservableCollection<Point>();

                double x1 = Math.Min(start.Value.X, end.Value.X);
                double y1 = Math.Min(start.Value.Y, end.Value.Y);
                double x2 = Math.Max(start.Value.X, end.Value.X);
                double y2 = Math.Max(start.Value.Y, end.Value.Y);

                double width = x2 - x1;
                double height = y2 - y1;
                double centerX = (x1 + x2) / 2;
                double centerY = (y1 + y2) / 2;

                double maxRadius = Math.Min(width, height) / 2;

                var points = new ObservableCollection<Point>();
                for (int i = 0; i < samples; i++)
                {
                    double t = (double)i / (samples - 1); // 0 to 1
                    double angle = t * turns * 2 * Math.PI;
                    double radius = t * maxRadius;

                    double px = centerX + radius * Math.Cos(angle);
                    double py = centerY + radius * Math.Sin(angle);

                    points.Add(new Point(px, py));
                }

                return points;
            }

            public static ObservableCollection<Point> CreateArrow(Point? start, Point? end, double headLength = 20, double headWidth = 10, double tailWidth = 4)
            {
                if (start == null || end == null)
                    return new ObservableCollection<Point>();

                Point p0 = start.Value;
                Point p1 = end.Value;

                Vector dir = p1 - p0;
                double length = dir.Length;
                if (length < 1e-6)
                    return new ObservableCollection<Point> { p0, p1 };

                dir.Normalize();

                // Perpendicular vector for width
                Vector perp = new Vector(-dir.Y, dir.X);

                // Points for the stem rectangle
                Point stemStartLeft = p0 + perp * (tailWidth / 2);
                Point stemStartRight = p0 - perp * (tailWidth / 2);
                Point stemEndLeft = p1 - dir * headLength + perp * (tailWidth / 2);
                Point stemEndRight = p1 - dir * headLength - perp * (tailWidth / 2);

                // Points for the arrowhead
                Point headBaseLeft = p1 - dir * headLength + perp * (headWidth / 2);
                Point headBaseRight = p1 - dir * headLength - perp * (headWidth / 2);

                // Arrow polygon: stem rectangle + arrowhead triangle
                var points = new ObservableCollection<Point>
            {
                stemStartLeft,
                stemEndLeft,
                headBaseLeft,
                p1,
                headBaseRight,
                stemEndRight,
                stemStartRight,
                stemStartLeft // close the polygon
            };

                return points;
            }

            public static ObservableCollection<Point> CreateCurve(Point start, Point end)
            {
                if (start == null || end == null)
                    return new ObservableCollection<Point>();

                Point ctrl1 = new Point(start.X + 0.55 * (end.X - start.X), start.Y + 0.55 * (end.Y - start.Y));
                return new ObservableCollection<Point>() { start, end, ctrl1 };
            }

            public static ObservableCollection<Point> CreateBezier(Point start, Point end)
            {
                if (start == null || end == null)
                    return new ObservableCollection<Point>();

                Point ctrl1 = new Point(start.X + 0.33 * (end.X - start.X), start.Y + 0.33 * (end.Y - start.Y));
                Point ctrl2 = new Point(start.X + 0.66 * (end.X - start.X), start.Y + 0.66 * (end.Y - start.Y));
                return new ObservableCollection<Point>() { start, end, ctrl1, ctrl2 };
            }

            public static Point? CreatePolylinePoints(ObservableCollection<Point> points, Point? lastAddedPoint, Point currentPos, double spacing)
            {
                if (lastAddedPoint == null)
                {
                    points.Add(currentPos);
                    return currentPos;
                }

                var lastPoint = lastAddedPoint.Value;
                var distance = (currentPos - lastPoint).Length;

                if (distance >= spacing)
                {
                    int pointsToAdd = (int)(distance / spacing);
                    Vector direction = (currentPos - lastPoint);
                    direction.Normalize();

                    for (int i = 1; i <= pointsToAdd; i++)
                        points.Add(lastPoint + direction * (i * spacing));

                    return currentPos;
                }

                return lastAddedPoint;
            }

            public static void RemovePolylinePoints(XLayer layer, Point mouseLocation, double tolerance)
            {
                if (layer != null && layer is IShapeLayer shapeLayer)
                {
                    var pencilShapes = shapeLayer.Shapes.Where(x => x.Type == XToolType.Polyline).ToList();

                    foreach (var pencilShape in pencilShapes)
                    {
                        if (pencilShape is XPolyline pencil)
                        {
                            var pointsToRemove = pencil.Points.Where(p => (p - mouseLocation).Length <= tolerance).ToList();
                            foreach (var point in pointsToRemove)
                                pencil.Points.Remove(point);

                            if (pencil.Points.Count == 0)
                                shapeLayer.Shapes.Remove(pencil);
                        }
                    }
                }
            }


            public static void ResamplePoints(ObservableCollection<Point> points, double spacing)
            {
                if (points == null || points.Count < 2 || spacing <= 0)
                    return;

                // Calculate total length segments and accumulate lengths
                var segmentLengths = new List<double>();
                double totalLength = 0;

                for (int i = 0; i < points.Count - 1; i++)
                {
                    double segmentLength = (points[i + 1] - points[i]).Length;
                    segmentLengths.Add(segmentLength);
                    totalLength += segmentLength;
                }

                var newPoints = new List<Point>();
                newPoints.Add(points[0]); // always keep first point

                double distanceSoFar = 0;
                int segmentIndex = 0;
                double segmentStartDistance = 0;

                while (distanceSoFar + spacing <= totalLength)
                {
                    distanceSoFar += spacing;

                    // Move to correct segment
                    while (segmentIndex < segmentLengths.Count && distanceSoFar > segmentStartDistance + segmentLengths[segmentIndex])
                    {
                        segmentStartDistance += segmentLengths[segmentIndex];
                        segmentIndex++;
                    }

                    if (segmentIndex >= segmentLengths.Count)
                        break;

                    // Interpolate between points[segmentIndex] and points[segmentIndex+1]
                    double segmentDist = distanceSoFar - segmentStartDistance;
                    double segmentLength = segmentLengths[segmentIndex];

                    double t = segmentDist / segmentLength;

                    Point p0 = points[segmentIndex];
                    Point p1 = points[segmentIndex + 1];

                    Point interpolated = new Point(
                        p0.X + t * (p1.X - p0.X),
                        p0.Y + t * (p1.Y - p0.Y));

                    newPoints.Add(interpolated);
                }

                // Always keep the last point
                newPoints.Add(points[^1]);

                // Replace the points in the ObservableCollection in place
                points.Clear();
                foreach (var p in newPoints)
                    points.Add(p);
            }

            public static Point RotatePointAroundCenter(Point point, Point center, double angleDegrees)
            {
                double radians = angleDegrees * Math.PI / 180.0;
                double cos = Math.Cos(radians);
                double sin = Math.Sin(radians);

                double dx = point.X - center.X;
                double dy = point.Y - center.Y;

                double xNew = center.X + dx * cos - dy * sin;
                double yNew = center.Y + dx * sin + dy * cos;

                return new Point(xNew, yNew);
            }
        }

        #endregion

        #region Color

        public static class Color
        {
            public static XColor CreateColor(System.Windows.Media.Color primaryColor) => new XSolidColor { Color = primaryColor };
            public static XColor CreateColor(XColorType activeColorType, XGradientStyle activeGradientStyle, System.Windows.Media.Color primaryColor, System.Windows.Media.Color secondaryColor)
            {
                if (activeColorType == XColorType.Solid)
                    return new XSolidColor { Color = primaryColor };
                else
                    if (activeGradientStyle == XGradientStyle.Linear)
                {
                    return new XLinearGradient
                    {
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(1, 0),
                        GradientStops = new ObservableCollection<XGradientStop>
                        {
                            new XGradientStop { Color = secondaryColor, Offset = 0 },
                            new XGradientStop { Color = primaryColor, Offset = 1 }
                        }
                    };
                }
                else
                {
                    return new XRadialGradient
                    {
                        Center = new Point(0.5, 0.5),
                        Radius = 0.5,
                        GradientStops = new ObservableCollection<XGradientStop>
                        {
                            new XGradientStop { Color = secondaryColor, Offset = 0 },
                            new XGradientStop { Color = primaryColor, Offset = 1 }
                        }
                    };
                }
            }
        }

        #endregion

        #region Bitmap

        public static class Bitmap
        {
            public static RenderTargetBitmap FlattenLayerToBitmap(IEnumerable<XRenderable> shapes, double width, double height, double dpi = 96)
            {
                if (shapes == null)
                    throw new ArgumentNullException(nameof(shapes));

                var root = new Canvas
                {
                    Width = width,
                    Height = height,
                    Background = new SolidColorBrush(Colors.Transparent)
                };

                var itemsControl = new ItemsControl
                {
                    ItemsSource = shapes,
                    Width = width,
                    Height = height
                };

                var itemsPanelTemplate = new ItemsPanelTemplate(new FrameworkElementFactory(typeof(Canvas)));
                itemsControl.ItemsPanel = itemsPanelTemplate;

                root.Children.Add(itemsControl);

                // Use a pack URI if needed
                var resourceDictionary = new ResourceDictionary
                {
                    Source = new Uri("pack://application:,,,/Resources/Renderer.xaml", UriKind.Absolute)
                };
                itemsControl.Resources.MergedDictionaries.Add(resourceDictionary);

                root.Measure(new Size(width, height));
                root.Arrange(new Rect(0, 0, width, height));
                root.UpdateLayout();

                var renderTargetBitmap = new RenderTargetBitmap(
                    (int)width,
                    (int)height,
                    dpi,
                    dpi,
                    PixelFormats.Pbgra32);

                renderTargetBitmap.Render(itemsControl);

                return renderTargetBitmap;
            }

            public static RenderTargetBitmap AddShapeToBitmap(ImageSource bitmap, XRenderable shape, double width, double height, double dpi = 96)
            {
                if (shape == null)
                    throw new ArgumentNullException(nameof(shape));

                var root = new Canvas
                {
                    Width = width,
                    Height = height,
                    Background = bitmap == null ? Brushes.Transparent : new ImageBrush(bitmap)
                };

                var contentPresenter = new ContentPresenter
                {
                    Content = shape
                };
                root.Children.Add(contentPresenter);

                // Use a pack URI if needed
                var resourceDictionary = new ResourceDictionary
                {
                    Source = new Uri("pack://application:,,,/View/Component/Drawing/Resources/Renderer.xaml", UriKind.Absolute)
                };
                root.Resources.MergedDictionaries.Add(resourceDictionary);

                root.Measure(new Size(width, height));
                root.Arrange(new Rect(0, 0, width, height));
                root.UpdateLayout();

                var renderTargetBitmap = new RenderTargetBitmap(
                    (int)width,
                    (int)height,
                    dpi,
                    dpi,
                    PixelFormats.Pbgra32);

                renderTargetBitmap.Render(root);

                return renderTargetBitmap;
            }
        }

        #endregion
    }
}
