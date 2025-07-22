using NET.Paint.Drawing.Constant;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Structure
{
    public abstract class XRenderable
    {
        public abstract XToolType Type { get; }
        public Guid Id = Guid.NewGuid();
        public List<Point> Points = new List<Point>();
    }

    public abstract class XStrokedShape : XRenderable
    {
        public Brush StrokeBrush = Brushes.Black;
        public double StrokeThickness = 1.0;
        public DoubleCollection StrokeStyle = new DoubleCollection { 1 };
    }

    public abstract class XFilledShape : XStrokedShape
    {
        public Brush FillBrush = Brushes.White;
    }
}
