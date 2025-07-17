using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model.Structure;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace NET.Paint.Drawing.Model.Shape
{
    public class XEllipse : XFilled, IRotateable
    {
        public override XToolType Type => XToolType.Ellipse;
        public XEllipseStyle Style => XEllipseStyle.Ellipse;

        public override Point Location => new Point(Points[0].X - RadiusX, Points[0].Y - RadiusY);

        [Browsable(false)]
        public Point Center => Points[0];

        [Category("Dimensions")]
        public double RadiusX => Math.Abs(Points[1].X - Points[0].X);
        
        [Category("Dimensions")]
        public double RadiusY => Math.Abs(Points[2].Y - Points[0].Y);
        
        public override double Width => RadiusX * 2;
        public override double Height => RadiusY * 2;

        private double _rotation = 0;
        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Center));
            OnPropertyChanged(nameof(RadiusX));
            OnPropertyChanged(nameof(RadiusY));
            OnPropertyChanged(nameof(Location));
        }

        public override object Clone() => new XEllipse
        {
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            FillColor = this.FillColor,
            Rotation = this.Rotation,
            Points = new ObservableCollection<Point>(this.Points)
        };
    }

    public class XCircle : XFilled
    {
        public override XToolType Type => XToolType.Ellipse;
        public XEllipseStyle Style => XEllipseStyle.Circle;

        public override Point Location => new Point(Points[0].X - Radius, Points[0].Y - Radius);

        [Category("Dimensions")]
        public double Radius => Math.Sqrt(Math.Pow(Points[1].X - Points[0].X, 2) + Math.Pow(Points[1].Y - Points[0].Y, 2));
        public override double Width => Radius * 2;
        public override double Height => Radius * 2;

        public Point Center => Points[0];

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(Center));
            OnPropertyChanged(nameof(Radius));
        }

        public override object Clone() => new XCircle
        {
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            FillColor = this.FillColor,
            Points = new ObservableCollection<Point>(this.Points)
        };
    }
}
