using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model.Structure;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Shape
{
    public abstract class PolygonViewModel : FilledShapeViewModel, IRotateable
    {
        public override XToolType Type => XToolType.Polygon;
        public abstract XPolygonStyle Style { get; }

        [Browsable(false)]
        public Point Center => new Point((Points.Min(p => p.X) + Points.Max(p => p.X)) / 2, (Points.Min(p => p.Y) + Points.Max(p => p.Y)) / 2);

        private Brush _fillBrush;

        [Category("Fill")]
        [DisplayName("Color")]
        public Brush FillBrush
        {
            get => _fillBrush;
            set => SetProperty(ref _fillBrush, value);
        }

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

    public class TriangleViewModel : PolygonViewModel
    {
        public override XToolType Type => XToolType.Triangle;
        public override XPolygonStyle Style => XPolygonStyle.Triangle;

        public override object Clone() => new TriangleViewModel
        {
            Points = new ObservableCollection<Point>(this.Points),
            FillBrush = this.FillBrush,
            StrokeBrush = this.StrokeBrush,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };
    }

    public class RegularPolygonViewModel : PolygonViewModel
    {
        public override XPolygonStyle Style => XPolygonStyle.Regular;

        private int _corners = 5;
        public int Corners
        {
            get => _corners;
            set => SetProperty(ref _corners, value);
        }

        public override object Clone() => new RegularPolygonViewModel
        {
            Points = new ObservableCollection<Point>(this.Points),
            FillBrush = this.FillBrush,
            StrokeBrush = this.StrokeBrush,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation,
            Corners = this.Corners
        };
    }

    public class StarViewModel : PolygonViewModel
    {
        public override XPolygonStyle Style => XPolygonStyle.Star;

        public override object Clone() => new StarViewModel
        {
            Points = new ObservableCollection<Point>(this.Points),
            FillBrush = this.FillBrush,
            StrokeBrush = this.StrokeBrush,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };
    }

    public class CloudVIewModel : PolygonViewModel
    {
        public override XPolygonStyle Style => XPolygonStyle.Cloud;

        public override object Clone() => new CloudVIewModel
        {
            Points = new ObservableCollection<Point>(this.Points),
            FillBrush = this.FillBrush,
            StrokeBrush = this.StrokeBrush,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };
    }

    public class HeartViewModel : PolygonViewModel
    {
        public override XPolygonStyle Style => XPolygonStyle.Heart;

        public override object Clone() => new HeartViewModel
        {
            Points = new ObservableCollection<Point>(this.Points),
            FillBrush = this.FillBrush,
            StrokeBrush = this.StrokeBrush,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };
    }

    public class SpiralViewModel : PolygonViewModel
    {
        public override XPolygonStyle Style => XPolygonStyle.Spiral;
        public override object Clone() => new SpiralViewModel
        {
            Points = new ObservableCollection<Point>(this.Points),
            FillBrush = this.FillBrush,
            StrokeBrush = this.StrokeBrush,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };
    }

    public class ArrowViewModel : PolygonViewModel
    {
        public override XPolygonStyle Style => XPolygonStyle.Arrow;
        public override object Clone() => new ArrowViewModel
        {
            Points = new ObservableCollection<Point>(this.Points),
            FillBrush = this.FillBrush,
            StrokeBrush = this.StrokeBrush,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };
    }
}
