using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Factory;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model.Structure;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows;

namespace NET.Paint.Drawing.Model.Shape
{
    public abstract class XPolygon : XFilled
    {
        public abstract override ToolType Type { get; }

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Points));
            OnPropertyChanged(nameof(Center));
        }
    }

    public class XTriangle : XPolygon
    {
        public override ToolType Type => ToolType.Triangle;

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

    public class XPentagon : XPolygon
    {
        public override ToolType Type => ToolType.Pentagon;

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
        public override ToolType Type => ToolType.Hexagon;

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
        public override ToolType Type => ToolType.Octagon;

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
        public override ToolType Type => ToolType.Star;

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
}
