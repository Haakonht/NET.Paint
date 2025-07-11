﻿using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
                        StrokeStyle = tools.StrokeStyle,
                        Spacing = tools.Spacing
                    };
                case ToolType.Line:
                    return new XLine
                    {
                        Points = new ObservableCollection<Point>() { tools.ClickLocation!.Value, tools.MouseLocation },
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Bezier:
                    return new XBezier
                    {
                        Points = CreateBezier(tools.ClickLocation!.Value, tools.MouseLocation ),
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Circle:
                    return new XCircle
                    {
                        Points = CreateCircle(tools.ClickLocation!.Value, tools.MouseLocation ),
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Ellipse:
                    return new XEllipse
                    {
                        Points = new ObservableCollection<Point>() { tools.ClickLocation!.Value, tools.MouseLocation },
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
                        Points = new ObservableCollection<Point>() { tools.ClickLocation!.Value, tools.MouseLocation },
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.RoundedRectangle:
                    return new XRoundedRectangle
                    {
                        Points = new ObservableCollection<Point>() { tools.ClickLocation!.Value, tools.MouseLocation },
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle,
                        RadiusX = tools.RadiusX,
                        RadiusY = tools.RadiusY
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
                        Points = CreateStar(tools.ClickLocation, tools.MouseLocation, tools.Points),
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Heart:
                    return new XHeart
                    {
                        Points = CreateHeart(tools.ClickLocation, tools.MouseLocation),
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Spiral:
                    return new XSpiral
                    {
                        Points = CreateSpiral(tools.ClickLocation, tools.MouseLocation, tools.Turns, 100),
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Arrow:
                    return new XArrow
                    {
                        Points = CreateArrow(tools.ClickLocation, tools.MouseLocation, tools.HeadLength, tools.HeadWidth, tools.TailWidth),
                        StrokeColor = tools.StrokeColor,
                        StrokeThickness = tools.StrokeThickness,
                        FillColor = tools.FillColor,
                        StrokeStyle = tools.StrokeStyle
                    };
                case ToolType.Text:
                    return new XText
                    {
                        Points = new ObservableCollection<Point> { tools.MouseLocation },
                        FontFamily = tools.FontFamily,
                        FontSize = tools.FontSize,
                        IsBold = tools.IsBold,
                        IsItalic = tools.IsItalic,
                        IsStrikethrough = tools.IsStrikethrough,
                        IsUnderline = tools.IsUnderline,
                        Text = ""
                    };
                case ToolType.Bitmap:
                    return new XBitmap
                    {
                        Bitmap = tools.ActiveBitmap,
                        Scaling = tools.BitmapScaling,
                        Points = new ObservableCollection<Point> { tools.ClickLocation.Value, tools.MouseLocation },
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

        private static ObservableCollection<Point> CreateHeart(Point? start, Point? end, int samples = 64)
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

        private static ObservableCollection<Point> CreateSpiral(Point? start, Point? end, int turns = 3, int samples = 200)
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

        private static ObservableCollection<Point> CreateArrow(Point? start, Point? end, double headLength = 20, double headWidth = 10, double tailWidth = 4)
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

        public static Point? CreatePencilPoints(ObservableCollection<Point> points, Point? lastAddedPoint, Point currentPos, double spacing)
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
}
