using System.Collections.ObjectModel;
using NET.XDraw.Constants;
using NET.XDraw.Utility;

namespace NET.XDraw.Models.Shapes
{
    public abstract class XShape : Notifier
    {
        public XShape()
        {
            _points.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(TopLeft));
                OnPropertyChanged(nameof(TopRight));
                OnPropertyChanged(nameof(Center));
                OnPropertyChanged(nameof(BottomLeft));
                OnPropertyChanged(nameof(BottomRight));
                OnPropertyChanged(nameof(Width));
                OnPropertyChanged(nameof(Height));
            };
        }

        #region General

        public abstract ToolType Type { get; }

        private Guid _id = Guid.NewGuid();
        public Guid Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }


        #endregion

        #region Position

        private ObservableCollection<Point> _points = new ObservableCollection<Point>();
        public ObservableCollection<Point> Points
        {
            get => _points;
            set => SetProperty(ref _points, value);
        }
        public Point TopLeft => new Point(_points.Min(p => p.X), _points.Min(p => p.Y));
        public Point TopRight => new Point(_points.Max(p => p.X), _points.Min(p => p.Y));
        public Point Center => new Point(TopLeft.X + BottomRight.X / 2, TopLeft.Y + BottomRight.Y / 2);
        public Point BottomLeft => new Point(_points.Min(p => p.X), _points.Max(p => p.Y));
        public Point BottomRight => new Point(_points.Max(p => p.X), _points.Max(p => p.Y));
        public double Width => BottomRight.X - TopLeft.X;
        public double Height => BottomRight.Y - TopLeft.Y;

        private double _rotation = 0;
        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }

        #endregion

        #region Stroke



        #endregion
    }
}
