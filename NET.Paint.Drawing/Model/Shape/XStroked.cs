using MessagePack;
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
    [MessagePackObject]
    public class XLine : XStrokedRenderable
    {
        [Key(1)]
        public override XToolType Type => XToolType.Line;

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("Start")]
        public Point Start
        {
            get => Points[0];
            set => Points[0] = value;
        }

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("End")]
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
            Stroke = this.Stroke,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Points = new ObservableCollection<Point>(this.Points)
        };

        #endregion
    }

    [MessagePackObject]
    public class XPolyline : XStrokedRenderable
    {
        [Key(1)]
        public override XToolType Type => XToolType.Polyline;

        [Key(6)]
        [Category("Shape")]
        [DisplayName("Spacing")]
        public double PointSpacing
        {
            get => _spacing;
            set
            {
                if (value != _spacing && value > 9)
                {
                    XFactory.Tools.ResamplePoints(Points, value);
                    SetProperty(ref _spacing, value);
                }
            }
        }
        protected double _spacing = 13.0;

        #region Volatile - Not Serialized

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(Points));

        public override object Clone() => new XPolyline
        {
            Stroke = this.Stroke,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Points = new ObservableCollection<Point>(this.Points),
            PointSpacing = this.PointSpacing
        };

        #endregion
    }

    [MessagePackObject]
    public class XCurve : XStrokedRenderable, IControlPoints
    {
        [Key(1)]
        public override XToolType Type => XToolType.Curve;

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("Start")]
        public Point Start
        {
            get => Points[0];
            set => Points[0] = value;
        }

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("End")]
        public Point End
        {
            get => Points[1];
            set => Points[1] = value;
        }

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("Ctrl 1")]
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
            Stroke = this.Stroke,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Points = new ObservableCollection<Point>(this.Points)
        };

        #endregion
    }

    [MessagePackObject]
    public class XBezier : XCurve
    {
        [Key(1)]
        public override XToolType Type => XToolType.Bezier;

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("Ctrl 2")]
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
            Stroke = this.Stroke,
            StrokeThickness = this.StrokeThickness,
            StrokeStyle = this.StrokeStyle,
            Points = new ObservableCollection<Point>(this.Points)
        };

        #endregion
    }
}
