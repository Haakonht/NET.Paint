using NET.Paint.Drawing.Constant;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text.Json.Serialization;
using System.Windows;

namespace NET.Paint.Drawing.Model.Shape
{
    public abstract class XPolygon : XFilled
    {
        public override ToolType Type => ToolType.Polygon;
        public abstract PolygonType Style { get; }

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Points));
            OnPropertyChanged(nameof(Center));
        }
    }

    public class XTriangle : XPolygon
    {
        public override ToolType Type => ToolType.Triangle;
        public override PolygonType Style => PolygonType.Triangle;

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
        public override PolygonType Style => PolygonType.Regular;

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

    public class XPentagon : XPolygon
    {
        public override PolygonType Style => PolygonType.Pentagon;

        public override object Clone() => new XPentagon
        {
            Points = new ObservableCollection<Point>(this.Points),
            FillColor = this.FillColor,
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };
    }


    public class XHexagon : XPolygon
    {
        public override PolygonType Style => PolygonType.Hexagon;

        public override object Clone() => new XHexagon
        {
            Points = new ObservableCollection<Point>(this.Points),
            FillColor = this.FillColor,
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Rotation = this.Rotation
        };
    }

    public class XOctagon : XPolygon
    {
        public override PolygonType Style => PolygonType.Octagon;

        public override object Clone() => new XOctagon
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
        public override PolygonType Style => PolygonType.Star;

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
        public override PolygonType Style => PolygonType.Heart;

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
        public override PolygonType Style => PolygonType.Spiral;
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
        public override PolygonType Style => PolygonType.Arrow;
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
