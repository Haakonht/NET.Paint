using NET.Paint.Drawing.Mvvm;
using System.Windows;

namespace NET.Paint.Drawing.Model.Utility
{
    public class XPoint : PropertyNotifier
    {
        private double _x;
        public double X
        {
            get => _x;
            set => SetProperty(ref _x, value);
        }

        private double _y;
        public double Y
        {
            get => _y;
            set => SetProperty(ref _y, value);
        }

        public XPoint() { }
        public XPoint(Point point)
        {
            X = point.X;
            Y = point.Y;
        }
        public XPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Point(XPoint pvm)
        {
            return new Point(pvm.X, pvm.Y);
        }

        public static explicit operator XPoint(Point pt)
        {
            return new XPoint{ X = pt.X, Y = pt.Y };
        }
    }
}
