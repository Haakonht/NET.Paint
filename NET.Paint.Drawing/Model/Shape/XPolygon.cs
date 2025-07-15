using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Interface;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace NET.Paint.Drawing.Model.Shape
{
    public abstract class XPolygon : XFilled, IRotateable
    {
        public override XToolType Type => XToolType.Polygon;
        public abstract XPolygonStyle Style { get; }

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
            OnPropertyChanged(nameof(Points));
            OnPropertyChanged(nameof(Center));
        }
    }

    public class XTriangle : XPolygon
    {
        public override XToolType Type => XToolType.Triangle;
        public override XPolygonStyle Style => XPolygonStyle.Triangle;

        public override object Clone() => new XTriangle
        {
            Points = new ObservableCollection<Point>(this.Points),
            FillColor = this.FillColor,
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };
    }

    public class XRegular : XPolygon
    {
        public override XPolygonStyle Style => XPolygonStyle.Regular;

        private int _corners = 5;
        public int Corners
        {
            get => _corners;
            set => SetProperty(ref _corners, value);
        }

        public override object Clone() => new XRegular
        {
            Points = new ObservableCollection<Point>(this.Points),
            FillColor = this.FillColor,
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };
    }

    public class XStar : XPolygon
    {
        public override XPolygonStyle Style => XPolygonStyle.Star;

        public override object Clone() => new XStar
        {
            Points = new ObservableCollection<Point>(this.Points),
            FillColor = this.FillColor,
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };
    }

    public class XHeart : XPolygon
    {
        public override XPolygonStyle Style => XPolygonStyle.Heart;

        public override object Clone() => new XHeart
        {
            Points = new ObservableCollection<Point>(this.Points),
            FillColor = this.FillColor,
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };
    }

    public class XSpiral : XPolygon
    {
        public override XPolygonStyle Style => XPolygonStyle.Spiral;
        public override object Clone() => new XSpiral
        {
            Points = new ObservableCollection<Point>(this.Points),
            FillColor = this.FillColor,
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };
    }

    public class XArrow : XPolygon
    {
        public override XPolygonStyle Style => XPolygonStyle.Arrow;
        public override object Clone() => new XArrow
        {
            Points = new ObservableCollection<Point>(this.Points),
            FillColor = this.FillColor,
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };
    }
}
