using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace NET.Paint.View.Component.Drawing.Converters
{
    public class SmoothCurveConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var points = value as ObservableCollection<Point>;
            if (points == null || points.Count < 2)
                return new PointCollection();

            // Generate proper Bezier control points for PolyBezierSegment
            var bezierPoints = GenerateBezierControlPoints(points);
            return new PointCollection(bezierPoints);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private List<Point> GenerateBezierControlPoints(ObservableCollection<Point> points)
        {
            if (points.Count < 2)
                return new List<Point>(points);

            var result = new List<Point>();

            // PolyBezierSegment expects groups of 3 points: control1, control2, endpoint
            // Starting point is handled by PathFigure.StartPoint

            for (int i = 0; i < points.Count; i++)
            {
                Point current = points[i];
                Point next = points[(i + 1) % points.Count]; // Wrap around for closed shape
                Point prev = points[i == 0 ? points.Count - 1 : i - 1];
                Point afterNext = points[(i + 2) % points.Count];

                // Calculate smooth tangent direction
                Vector tangent = (next - prev);
                if (tangent.Length > 0)
                    tangent.Normalize();

                // Distance to next point
                double distance = (next - current).Length;
                double controlDistance = distance * 0.25; // Control points at 25% of segment length

                // First control point (leaving current point)
                Point control1 = new Point(
                    current.X + tangent.X * controlDistance,
                    current.Y + tangent.Y * controlDistance
                );

                // Calculate tangent for arriving at next point
                Vector nextTangent = (afterNext - current);
                if (nextTangent.Length > 0)
                    nextTangent.Normalize();

                // Second control point (arriving at next point)  
                Point control2 = new Point(
                    next.X - nextTangent.X * controlDistance,
                    next.Y - nextTangent.Y * controlDistance
                );

                // Add the three points for this Bezier segment
                result.Add(control1);
                result.Add(control2);
                result.Add(next);
            }

            return result;
        }
    }
}