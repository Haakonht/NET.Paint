using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.ViewModels.Drawing.Structure
{
    public abstract class RenderableViewModel : PropertyNotifier, ICloneable
    {
        public XRenderable Model { get; private set; }

        public RenderableViewModel(XRenderable model) => Model = model;

        [Browsable(false)]
        public abstract XToolType Type { get; }

        public Guid Id => Model.Id;

        private ObservableCollection<Point> _points;
        [Browsable(false)]

        public ObservableCollection<Point> Points
        {
            get
            {
                if (_points == null)
                    _points = new ObservableCollection<Point>(Model.Points);
                return _points;
            }
            init
            {
                SetProperty(ref _points, value);
                _points.CollectionChanged += CollectionChanged;
            }
        }

        public RenderableViewModel()
        {
            _points = new ObservableCollection<Point>();
            _points.CollectionChanged += CollectionChanged;
        }

        public virtual void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(Points));

        public abstract object Clone();
    }

    public abstract class StrokedShapeViewModel : RenderableViewModel
    {
        private XStrokedShape Shape => (XStrokedShape)Model;

        [Category("Stroke")]
        [DisplayName("Color")]
        public Brush StrokeBrush
        {
            get => Shape.StrokeBrush;
            set => SetProperty(ref Shape.StrokeBrush, value);
        }

        [Category("Stroke")]
        [DisplayName("Thickness")]
        public double StrokeThickness
        {
            get => Shape.StrokeThickness;
            set => SetProperty(ref Shape.StrokeThickness, value);
        }

        [Category("Stroke")]
        [DisplayName("Style")]
        public DoubleCollection StrokeStyle
        {
            get => Shape.StrokeStyle;
            set => SetProperty(ref Shape.StrokeStyle, value);
        }
    }

    public abstract class FilledShapeViewModel : StrokedShapeViewModel
    {
        private XFilledShape Shape => (XFilledShape)Model;

        [DisplayName("Position")]
        public virtual Point Location => new Point(Points.Min(p => p.X), Points.Min(p => p.Y));

        [Category("Dimensions")]
        public virtual double Width => Points.Max(p => p.X) - Points.Min(p => p.X);

        [Category("Dimensions")]
        public virtual double Height => Points.Max(p => p.Y) - Points.Min(p => p.Y);

        [Category("Fill")]
        [DisplayName("Color")]
        public Brush FillBrush
        {
            get => Shape.FillBrush;
            set => SetProperty(ref Shape.FillBrush, value);
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
