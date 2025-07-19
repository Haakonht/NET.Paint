using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Structure
{
    public abstract class XRenderable : PropertyNotifier, ICloneable
    {
        [Browsable(false)]
        public abstract XToolType Type { get; }

        private Guid _id = Guid.NewGuid();
        public Guid Id => _id;

        private ObservableCollection<Point> _points;
        [Browsable(false)]

        public ObservableCollection<Point> Points
        {
            get => _points;
            init
            {
                SetProperty(ref _points, value);
                _points.CollectionChanged += CollectionChanged;
            }
        }

        public XRenderable()
        {
            _points = new ObservableCollection<Point>();
            _points.CollectionChanged += CollectionChanged;
        }

        public virtual void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(Points));

        public abstract object Clone();
    }

    public abstract class XStroked : XRenderable
    {
        private Brush _strokeBrush;
        [Category("Stroke")]
        [DisplayName("Color")]
        public Brush StrokeBrush
        {
            get => _strokeBrush;
            set => SetProperty(ref _strokeBrush, value);
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

    public abstract class XFilled : XStroked
    {
        [DisplayName("Position")]
        public virtual Point Location => new Point(Points.Min(p => p.X), Points.Min(p => p.Y));

        [Category("Dimensions")]
        public virtual double Width => Points.Max(p => p.X) - Points.Min(p => p.X);

        [Category("Dimensions")]
        public virtual double Height => Points.Max(p => p.Y) - Points.Min(p => p.Y);

        private Brush _fillBrush;

        [Category("Fill")]
        [DisplayName("Color")]
        public Brush FillBrush
        {
            get => _fillBrush;
            set => SetProperty(ref _fillBrush, value);
        }

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
        }
    }
}
