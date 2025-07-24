using MessagePack;
using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model.Structure;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace NET.Paint.Drawing.Model.Shape
{
    [MessagePackObject]
    public class XRectangle : XFilledRenderable, IRotateable
    {
        [Key(1)]
        public override XToolType Type => XToolType.Rectangle;

        [Key(7)]
        public virtual XRectangleStyle Style => XRectangleStyle.Rectangle;

        [Key(8)]
        public double CornerRadius
        {
            get => _cornerRadius; 
            set => SetProperty(ref _cornerRadius, value);
        }
        private double _cornerRadius = 0;

        [Key(9)]
        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }
        private double _rotation = 0;

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Browsable(false)]
        public virtual Point Center => new Point((Points.Min(p => p.X) + Points.Max(p => p.X)) / 2, (Points.Min(p => p.Y) + Points.Max(p => p.Y)) / 2);

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(Center));
            OnPropertyChanged(nameof(CornerRadius));
        }

        public override object Clone() => new XRectangle
        {
            Stroke = this.Stroke,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            CornerRadius = this.CornerRadius,
            Fill = this.Fill,
            Rotation = this.Rotation,
            Points = new ObservableCollection<Point>(this.Points)
        };

        #endregion
    }

    [MessagePackObject]
    public class XSquare : XRectangle
    {
        [Key(7)]
        public override XRectangleStyle Style => XRectangleStyle.Square;

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Browsable(false)]
        public override Point Center => Points[0];
        
        [IgnoreMember]
        [Browsable(false)]
        public override double Width => Math.Max(Math.Abs(Points[1].X - Points[0].X), Math.Abs(Points[1].Y - Points[0].Y)) * 2;

        [IgnoreMember]
        [Browsable(false)]
        public override double Height => Math.Max(Math.Abs(Points[1].X - Points[0].X), Math.Abs(Points[1].Y - Points[0].Y)) * 2;

        [IgnoreMember]
        [Browsable(false)]
        public override Point Location => new Point(Center.X - Width / 2, Center.Y - Width / 2);

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Call base but skip XRectangle's implementation to avoid conflicts
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(Center));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
            OnPropertyChanged(nameof(CornerRadius));
        }

        public override object Clone() => new XSquare
        {
            Stroke = this.Stroke,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            CornerRadius = this.CornerRadius,
            Fill = this.Fill,
            Rotation = this.Rotation,
            Points = new ObservableCollection<Point>(this.Points)
        };

        #endregion
    }

    [MessagePackObject]
    public class XEllipse : XFilledRenderable, IRotateable
    {
        [Key(1)]
        public override XToolType Type => XToolType.Ellipse;

        [Key(7)]
        public XEllipseStyle Style => XEllipseStyle.Ellipse;

        [Key(8)]
        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }
        private double _rotation = 0;

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Browsable(false)]
        public override Point Location => new Point(Points[0].X - RadiusX, Points[0].Y - RadiusY);

        [IgnoreMember]
        [Browsable(false)]
        public override double Width => RadiusX * 2;

        [IgnoreMember]
        [Browsable(false)]
        public override double Height => RadiusY * 2;

        [IgnoreMember]
        [Browsable(false)]
        public Point Center => Points[0];

        [IgnoreMember]
        [Browsable(false)]
        public double RadiusX => Math.Abs(Points[1].X - Points[0].X);

        [IgnoreMember]
        [Browsable(false)]
        public double RadiusY => Math.Abs(Points[2].Y - Points[0].Y);

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
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Stroke = this.Stroke,
            Fill = this.Fill,
            Rotation = this.Rotation,
            Points = new ObservableCollection<Point>(this.Points)
        };

        #endregion
    }

    [MessagePackObject]
    public class XCircle : XFilledRenderable
    {
        [Key(1)]
        public override XToolType Type => XToolType.Ellipse;

        [Key(7)]
        public XEllipseStyle Style => XEllipseStyle.Circle;

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Browsable(false)]
        public override Point Location => new Point(Points[0].X - Radius, Points[0].Y - Radius);

        [IgnoreMember]
        [Browsable(false)]
        public override double Width => Radius * 2;

        [IgnoreMember]
        [Browsable(false)]
        public override double Height => Radius * 2;

        [IgnoreMember]
        [Browsable(false)]
        public double Radius => Math.Sqrt(Math.Pow(Points[1].X - Points[0].X, 2) + Math.Pow(Points[1].Y - Points[0].Y, 2));

        [IgnoreMember]
        [Browsable(false)]
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
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Stroke = this.Stroke,
            Fill = this.Fill,
            Points = new ObservableCollection<Point>(this.Points)
        };

        #endregion
    }
}
