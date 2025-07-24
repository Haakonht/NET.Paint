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
    [Union(0, typeof(XTriangle))]
    [Union(1, typeof(XRegular))]
    [Union(2, typeof(XStar))]
    [Union(3, typeof(XHeart))]
    [Union(4, typeof(XCloud))]
    [Union(5, typeof(XArrow))]
    [Union(6, typeof(XSpiral))]
    [MessagePackObject]
    public abstract class XPolygon : XFilledRenderable, IRotateable
    {
        [Key(1)]
        public override XToolType Type => XToolType.Polygon;

        [IgnoreMember]
        public abstract XPolygonStyle Style { get; }

        [Key(7)]
        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }
        private double _rotation = 0;

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Browsable(false)]
        public Point Center => new Point((Points.Min(p => p.X) + Points.Max(p => p.X)) / 2, (Points.Min(p => p.Y) + Points.Max(p => p.Y)) / 2);

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Points));
            OnPropertyChanged(nameof(Center));
        }

        #endregion
    }

    [MessagePackObject]
    public class XTriangle : XPolygon
    {
        [Key(1)]
        public override XToolType Type => XToolType.Triangle;

        [Key(8)]
        public override XPolygonStyle Style => XPolygonStyle.Triangle;

        #region Volatile - Not Serialized

        public override object Clone() => new XTriangle
        {
            Points = new ObservableCollection<Point>(this.Points),
            Fill = this.Fill,
            Stroke = this.Stroke,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };

        #endregion
    }

    [MessagePackObject]
    public class XRegular : XPolygon
    {
        [Key(8)]
        public override XPolygonStyle Style => XPolygonStyle.Regular;

        [Key(9)]
        public int Corners
        {
            get => _corners;
            set => SetProperty(ref _corners, value);
        }
        private int _corners = 5;

        #region Volatile - Not Serialized

        public override object Clone() => new XRegular
        {
            Points = new ObservableCollection<Point>(this.Points),
            Fill = this.Fill,
            Stroke = this.Stroke,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation,
            Corners = this.Corners
        };

        #endregion
    }

    [MessagePackObject]
    public class XStar : XPolygon
    {
        [Key(8)]
        public override XPolygonStyle Style => XPolygonStyle.Star;

        #region Volatile - Not Serialized

        public override object Clone() => new XStar
        {
            Points = new ObservableCollection<Point>(this.Points),
            Fill = this.Fill,
            Stroke = this.Stroke,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };

        #endregion
    }

    [MessagePackObject]
    public class XCloud : XPolygon
    {
        [Key(8)]
        public override XPolygonStyle Style => XPolygonStyle.Cloud;

        #region Volatile - Not Serialized

        public override object Clone() => new XCloud
        {
            Points = new ObservableCollection<Point>(this.Points),
            Fill = this.Fill,
            Stroke = this.Stroke,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };

        #endregion
    }

    [MessagePackObject]
    public class XHeart : XPolygon
    {
        [Key(8)]
        public override XPolygonStyle Style => XPolygonStyle.Heart;

        #region Volatile - Not Serialized

        public override object Clone() => new XHeart
        {
            Points = new ObservableCollection<Point>(this.Points),
            Fill = this.Fill,
            Stroke = this.Stroke,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };

        #endregion
    }

    [MessagePackObject]
    public class XSpiral : XPolygon
    {
        [Key(8)]
        public override XPolygonStyle Style => XPolygonStyle.Spiral;

        #region Volatile - Not Serialized

        public override object Clone() => new XSpiral
        {
            Points = new ObservableCollection<Point>(this.Points),
            Fill = this.Fill,
            Stroke = this.Stroke,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };

        #endregion
    }

    [MessagePackObject]
    public class XArrow : XPolygon
    {
        [Key(8)]
        public override XPolygonStyle Style => XPolygonStyle.Arrow;

        #region Volatile - Not Serialized

        public override object Clone() => new XArrow
        {
            Points = new ObservableCollection<Point>(this.Points),
            Fill = this.Fill,
            Stroke = this.Stroke,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };

        #endregion
    }
}
