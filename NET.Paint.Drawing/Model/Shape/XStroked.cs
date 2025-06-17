using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Factory;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Shape
{
    public abstract class XStroked : XRenderable
    {
        private Color _strokeColor;
        [Category("Stroke")]
        [DisplayName("Color")]
        public Color StrokeColor
        {
            get => _strokeColor;
            set => SetProperty(ref _strokeColor, value);
        }

        private double _strokeThickness;
        [Category("Stroke")]
        [DisplayName("Thickness")]
        public double StrokeThickness
        {
            get => _strokeThickness;
            set => SetProperty(ref _strokeThickness, value);
        }

        private XStrokeStyle _strokeStyle;
        [Category("Stroke")]
        [DisplayName("Style")]
        public XStrokeStyle StrokeStyle
        {
            get => _strokeStyle;
            set => SetProperty(ref _strokeStyle, value);
        }
    }

    public class XPencil : XStroked
    {
        public override ToolType Type => ToolType.Pencil;

        protected double _spacing = 13.0;
        [DisplayName("Resolution")]
        public double Spacing
        {
            get => _spacing;
            set
            {
                if (value != _spacing && value > 9)
                {
                    XFactory.ResamplePoints(Points, value);
                    SetProperty(ref _spacing, value);
                }
            }
        }

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(Points));

        public override object Clone() => new XPencil
        {
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Points = new ObservableCollection<Point>(this.Points),
            Spacing = this.Spacing
        };

    }

    public class XLine : XStroked
    {
        public override ToolType Type => ToolType.Line;

        [Category("Position")]
        public Point Start
        {
            get => Points[0];
            set => Points[0] = value;
        }
        [Category("Position")]
        public Point End
        {
            get => Points[1];
            set => Points[1] = value;
        }
        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Start));
            OnPropertyChanged(nameof(End));
        }

        public override object Clone() => new XLine
        {
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Points = new ObservableCollection<Point>(this.Points)
        };
    }

    public class XBezier : XStroked
    {
        public override ToolType Type => ToolType.Bezier;

        [Category("Position")]
        public Point Start
        {
            get => Points[0];
            set => Points[0] = value;
        }

        [Category("Position")]
        public Point End
        {
            get => Points[1];
            set => Points[1] = value;
        }

        [Category("Position")]
        [DisplayName("Control")]
        public Point Ctrl1
        {
            get => Points[2];
            set => Points[2] = value;
        }

        [Browsable(false)]
        public Point Ctrl2 => Points[2];

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Start));
            OnPropertyChanged(nameof(End));
            OnPropertyChanged(nameof(Ctrl1));
            OnPropertyChanged(nameof(Ctrl2));
        }

        public override object Clone() => new XBezier
        {
            Start = Start,
            End = End,
            Ctrl1 = Ctrl1,
            StrokeColor = this.StrokeColor,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Points = new ObservableCollection<Point>(this.Points)
        };
    }
}
