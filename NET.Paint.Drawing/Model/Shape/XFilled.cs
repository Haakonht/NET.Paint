using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Interface;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Shape
{
    public abstract class XFilled : XStroked
    {
        [DisplayName("Position")]
        public Point Location => new Point(Points.Min(p => p.X), Points.Min(p => p.Y));

        [Category("Dimensions")]
        public virtual double Width => Points.Max(p => p.X) - Points.Min(p => p.X);
        
        [Category("Dimensions")]
        public virtual double Height => Points.Max(p => p.Y) - Points.Min(p => p.Y);

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
        }

        private Color _fillColor;
        [Category("Fill")]
        [DisplayName("Color")]
        public Color FillColor
        {
            get => _fillColor;
            set => SetProperty(ref _fillColor, value);
        }
    }

    public class XEllipse : XFilled, IRotateable
    {
        public override XToolType Type => XToolType.Ellipse;
        public XEllipseStyle Style => XEllipseStyle.Ellipse;

        [Browsable(false)]
        public Point Center => new Point((Points.Min(p => p.X) + Points.Max(p => p.X)) / 2, (Points.Min(p => p.Y) + Points.Max(p => p.Y)) / 2);

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

        [Category("Dimensions")]
        public override double Height => Points.Max(p => p.X) - Points.Min(p => p.X);

        public override object Clone() => new XCircle
        {
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            FillColor = this.FillColor,
            Points = new ObservableCollection<Point>(this.Points)
        };
    }

    public class XRectangle : XFilled, IRotateable
    {
        public override XToolType Type => XToolType.Rectangle;
        public virtual XRectangleStyle Style => XRectangleStyle.Rectangle;

        [Category("Corner")]
        private double _cornerRadius = 0;
        public double Radius
        {
            get => _cornerRadius; 
            set => SetProperty(ref _cornerRadius, value);
        }

        [Browsable(false)]
        public Point Center => new Point((Points.Min(p => p.X) + Points.Max(p => p.X)) / 2, (Points.Min(p => p.Y) + Points.Max(p => p.Y)) / 2);

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
            OnPropertyChanged(nameof(Radius));
        }

        public override object Clone() => new XRectangle
        {
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Radius = this.Radius,
            FillColor = this.FillColor,
            Rotation = this.Rotation,
            Points = new ObservableCollection<Point>(this.Points)
        };
    }

    public class XSquare : XRectangle
    {
        public override XRectangleStyle Style => XRectangleStyle.Square;

        [Category("Dimensions")]
        public override double Height => Points.Max(p => p.X) - Points.Min(p => p.X);

        public override object Clone() => new XSquare
        {
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Radius = this.Radius,
            FillColor = this.FillColor,
            Rotation = this.Rotation,
            Points = new ObservableCollection<Point>(this.Points)
        };
    }

}
