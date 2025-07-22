using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.ViewModels.Drawing.Structure;
using NET.Paint.ViewModels.Interface;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace NET.Paint.ViewModels.Drawing.Shape
{
    public class EllipseViewModel : FilledShapeViewModel, IRotateable
    {
        public required XEllipse Model { get; set; }

        public override XToolType Type => XToolType.Ellipse;
        public XEllipseStyle Style => Model.Style;

        public override Point Location => new Point(Points[0].X - RadiusX, Points[0].Y - RadiusY);

        [Browsable(false)]
        public Point Center => Points[0];

        [Category("Dimensions")]
        public double RadiusX => Math.Abs(Points[1].X - Points[0].X);
        
        [Category("Dimensions")]
        public double RadiusY => Math.Abs(Points[2].Y - Points[0].Y);
        
        public override double Width => RadiusX * 2;
        public override double Height => RadiusY * 2;

        public double Rotation
        {
            get => Model.Rotation;
            set => SetProperty(ref Model.Rotation, value);
        }

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Center));
            OnPropertyChanged(nameof(RadiusX));
            OnPropertyChanged(nameof(RadiusY));
            OnPropertyChanged(nameof(Location));
        }

        public override object Clone() => new EllipseViewModel
        {
            Model = new XEllipse
            {
                Style = XEllipseStyle.Ellipse,
                StrokeThickness = this.StrokeThickness,
                StrokeStyle = this.StrokeStyle,
                StrokeBrush = this.StrokeBrush,
                FillBrush = this.FillBrush,
                Points = new List<Point>(this.Points)
            }
        };
    }

    public class CircleViewModel : FilledShapeViewModel
    {
        public required XEllipse Model { get; set; }

        public override XToolType Type => XToolType.Ellipse;
        public XEllipseStyle Style => Model.Style;

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

        public override object Clone() => new CircleViewModel
        {
            Model = new XEllipse
            {
                Style = XEllipseStyle.Circle,
                StrokeThickness = this.StrokeThickness,
                StrokeStyle = this.StrokeStyle,
                StrokeBrush = this.StrokeBrush,
                FillBrush = this.FillBrush,
                Points = new List<Point>(this.Points)
            }
        };
    }
}
