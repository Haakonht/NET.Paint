using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Interface;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Shape
{
    public abstract class XFilled : XStroked, IRotateable
    {
        [DisplayName("Position")]
        public Point Location => new Point(Points.Min(p => p.X), Points.Min(p => p.Y));

        [Category("Dimensions")]
        public double Width => Points.Max(p => p.X) - Points.Min(p => p.X);
        
        [Category("Dimensions")]
        public double Height => Points.Max(p => p.Y) - Points.Min(p => p.Y);

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
            OnPropertyChanged(nameof(Center));
        }

        private Color _fillColor;
        [Category("Fill")]
        [DisplayName("Color")]
        public Color FillColor
        {
            get => _fillColor;
            set => SetProperty(ref _fillColor, value);
        }

        [Browsable(false)]
        public Point Center => new Point((Points.Min(p => p.X) + Points.Max(p => p.X)) / 2, (Points.Min(p => p.Y) + Points.Max(p => p.Y)) / 2);

        private double _rotation = 0;
        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }
    }

    public class XEllipse : XFilled
    {
        public override ToolType Type => ToolType.Ellipse;

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

    public class XRectangle : XFilled
    {
        public override ToolType Type => ToolType.Rectangle;

        [Category("Corner")]
        public double Radius { get; set; } = 0;

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
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
}
