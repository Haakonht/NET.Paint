
using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Shape;
using System.Collections.ObjectModel;
using System.Windows;

namespace NET.Paint.Drawing.Factory
{
    public static class XFactory
    {
        public static XRenderable? CreateShape(XTools tools)
        {
            switch (tools.ActiveTool)
            {
                case ToolType.Pencil:
                    return new XPencil
                    {
                        Points = new ObservableCollection<Point> { tools.ClickLocation!.Value },
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Line:
                    return new XLine
                    {
                        Points = new ObservableCollection<Point>() { tools.ClickLocation!.Value, tools.MouseLocation!.Value },
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Bezier:
                    return new XBezier
                    {
                        Points = CreateBezier(tools.ClickLocation!.Value, tools.MouseLocation!.Value),
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Circle:
                    return new XCircle
                    {
                        Points = CreateCircle(tools.ClickLocation!.Value, tools.MouseLocation!.Value),
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Ellipse:
                    return new XEllipse
                    {
                        Points = new ObservableCollection<Point>() { tools.ClickLocation!.Value, tools.MouseLocation!.Value },
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Triangle:
                    return new XTriangle
                    {
                        Points = CreateRegularPolygon(tools.ClickLocation, tools.MouseLocation, 3),
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Rectangle:
                    return new XRectangle
                    {
                        Points = new ObservableCollection<Point>() { tools.ClickLocation!.Value, tools.MouseLocation!.Value },
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.RoundedRectangle:
                    return new XRoundedRectangle
                    {
                        Points = new ObservableCollection<Point>() { tools.ClickLocation!.Value, tools.MouseLocation!.Value },
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Pentagon:
                    return new XPentagon
                    {
                        Points = CreateRegularPolygon(tools.ClickLocation, tools.MouseLocation, 5),
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Hexagon:
                    return new XHexagon
                    {
                        Points = CreateRegularPolygon(tools.ClickLocation, tools.MouseLocation, 6),
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Octagon:
                    return new XOctagon
                    {
                        Points = CreateRegularPolygon(tools.ClickLocation, tools.MouseLocation, 8),
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Star:
                    return new XStar
                    {
                        Points = CreateStar(tools.ClickLocation, tools.MouseLocation, tools.Corners),
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle
                    };
                default:
                    return null;
            }
        }

        private static ObservableCollection<Point> CreateRegularPolygon(Point? start, Point? end, int corners)
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

        private static ObservableCollection<Point> CreateStar(Point? start, Point? end, int corners, double innerRadiusRatio = 0.5)
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

        public static ObservableCollection<Point> CreateBezier(Point start, Point end)
        {
            if (start == null || end == null)
                return new ObservableCollection<Point>();

            Point ctrl = new Point(start.X + 0.5 * (end.X - start.X), start.Y + 0.5 * (end.Y - start.Y));
            return new ObservableCollection<Point>() { start, end, ctrl };
        }

        public static ObservableCollection<Point> CreateCircle(Point start, Point end)
        {
            double horizontalDistance = end.X - start.X;
            double verticalDistance = Math.Sign(end.Y - start.Y) * Math.Abs(horizontalDistance);
            var secondPoint = new Point(end.X, start.Y + verticalDistance);
            return new ObservableCollection<Point>() { start, secondPoint };
        }
    }
}
