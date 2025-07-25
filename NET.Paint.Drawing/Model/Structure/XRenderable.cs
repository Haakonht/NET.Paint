using MessagePack;
using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Utility;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Structure
{
    [Union(0, typeof(XText))]
    [Union(1, typeof(XBitmap))]
    [Union(2, typeof(XLine))]
    [Union(3, typeof(XPolyline))]
    [Union(4, typeof(XBezier))]
    [Union(5, typeof(XCurve))]
    [Union(6, typeof(XCircle))]
    [Union(7, typeof(XEllipse))]
    [Union(8, typeof(XTriangle))]
    [Union(9, typeof(XSquare))]
    [Union(10, typeof(XRectangle))]
    [Union(11, typeof(XRegular))]
    [Union(12, typeof(XStar))]
    [Union(13, typeof(XHeart))]
    [Union(14, typeof(XCloud))]
    [Union(15, typeof(XArrow))]
    [Union(16, typeof(XSpiral))]
    [MessagePackObject]
    public abstract class XRenderable : XObject, ICloneable
    {
        [IgnoreMember]
        [Browsable(false)]
        public abstract XToolType Type { get; }

        [Key(2)]
        [Category("Layout")]
        [DisplayName("Points")]
        public ObservableCollection<Point> Points
        {
            get => _points;
            init
            {
                SetProperty(ref _points, value);
                _points.CollectionChanged += CollectionChanged;
            }
        }
        private ObservableCollection<Point> _points;

        #region Volatile - Not Serialized

        public XRenderable()
        {
            _points = new ObservableCollection<Point>();
            _points.CollectionChanged += CollectionChanged;
        }

        public virtual void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(Points));

        public abstract object Clone();

        #endregion
    }

    [Union(0, typeof(XLine))]
    [Union(1, typeof(XPolyline))]
    [Union(2, typeof(XBezier))]
    [Union(3, typeof(XCurve))]
    [MessagePackObject]
    public abstract class XStrokedRenderable : XRenderable
    {
        [Key(3)]
        public XColor Stroke
        {
            get => _stroke;
            set => SetProperty(ref _stroke, value);
        }
        private XColor _stroke;

        [Key(4)]
        public double StrokeThickness
        {
            get => _strokeThickness;
            set => SetProperty(ref _strokeThickness, value);
        }
        private double _strokeThickness;


        [Key(5)]
        public DoubleCollection StrokeStyle
        {
            get => _strokeStyle;
            set => SetProperty(ref _strokeStyle, value);
        }
        private DoubleCollection _strokeStyle;
    }

    [Union(0, typeof(XCircle))]
    [Union(1, typeof(XEllipse))]
    [Union(2, typeof(XTriangle))]
    [Union(3, typeof(XSquare))]
    [Union(4, typeof(XRectangle))]
    [Union(5, typeof(XPolygon))]
    [MessagePackObject]
    public abstract class XFilledRenderable : XStrokedRenderable
    {
        [Key(6)]
        public XColor Fill
        {
            get => _fill;
            set => SetProperty(ref _fill, value);
        }
        private XColor _fill;

        #region Volatile - Not Serialized

        [IgnoreMember]
        public virtual Point Location => new Point(Points.Min(p => p.X), Points.Min(p => p.Y));

        [IgnoreMember]
        public virtual double Width => Points.Max(p => p.X) - Points.Min(p => p.X);

        [IgnoreMember]
        public virtual double Height => Points.Max(p => p.Y) - Points.Min(p => p.Y);

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
        }

        #endregion
    }
}
