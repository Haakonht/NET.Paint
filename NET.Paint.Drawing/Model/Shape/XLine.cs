using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Factory;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model.Structure;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace NET.Paint.Drawing.Model.Shape
{
    public class XLine : XStroked
    {
        public override XToolType Type => XToolType.Line;

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
            StrokeBrush = this.StrokeBrush,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Points = new ObservableCollection<Point>(this.Points)
        };
    }

    public class XPolyline : XStroked
    {
        public override XToolType Type => XToolType.Pencil;

        protected double _spacing = 13.0;
        [DisplayName("Resolution")]
        public double PointSpacing
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

        public override object Clone() => new XPolyline
        {
            StrokeBrush = this.StrokeBrush,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Points = new ObservableCollection<Point>(this.Points),
            PointSpacing = this.PointSpacing
        };

    }

    public class XCurve : XStroked, IControlPoints
    {
        public override XToolType Type => XToolType.Curve;

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

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Start));
            OnPropertyChanged(nameof(End));
            OnPropertyChanged(nameof(Ctrl1));
        }

        public override object Clone() => new XCurve
        {
            Start = this.Start,
            End = this.End,
            Ctrl1 = this.Ctrl1,
            StrokeBrush = this.StrokeBrush,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Points = new ObservableCollection<Point>(this.Points)
        };
    }

    public class XBezier : XCurve
    {
        [Browsable(false)]
        public Point Ctrl2
        {
            get => Points[3];
            set => Points[3] = value;
        }

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
            Start = this.Start,
            End = this.End,
            Ctrl1 = this.Ctrl1,
            Ctrl2 = this.Ctrl2,
            StrokeBrush = this.StrokeBrush,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Points = new ObservableCollection<Point>(this.Points)
        };
    }
}
