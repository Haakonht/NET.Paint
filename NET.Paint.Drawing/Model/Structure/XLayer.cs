using MessagePack;
using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Interface;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Structure
{
    [Union(0, typeof(XVectorLayer))]
    [Union(1, typeof(XRasterLayer))]
    [Union(2, typeof(XHybridLayer))]
    [MessagePackObject]
    public abstract class XLayer : XObject, ICloneable
    {
        [IgnoreMember]
        public abstract XLayerType Type { get; }

        [Key(2)]
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _title = "Layer";

        [Key(3)]
        public double OffsetX
        {
            get => _offsetX;
            set
            {
                SetProperty(ref _offsetX, value);
                OnPropertyChanged(nameof(Offset));
            }
        }
        private double _offsetX = 0;

        [Key(4)]
        public double OffsetY
        {
            get => _offsetY;
            set
            {
                SetProperty(ref _offsetY, value);
                OnPropertyChanged(nameof(Offset));
            }
        }
        private double _offsetY = 0;

        [Key(5)]
        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }
        private double _rotation = 0;

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Browsable(false)]
        public Point Offset => new Point(OffsetX, OffsetY);

        [IgnoreMember]
        [Browsable(false)]
        public bool IsVisible
        {
            get => _isVisible;
            set
            {
                SetProperty(ref _isVisible, value);
                OnPropertyChanged(nameof(Visibility));
            }
        }
        private bool _isVisible = true;

        [IgnoreMember]
        [Browsable(false)]
        public Visibility Visibility
        {
            get
            {
                if (!_isVisible) return Visibility.Collapsed;
                return Visibility.Visible;
            }
        }

        [IgnoreMember]
        [Browsable(false)]
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }
        private bool _isEditing = false;

        [IgnoreMember]
        public abstract bool CanUndo { get; }
        public abstract object Clone();

        #endregion
    }

    [MessagePackObject]
    public class XVectorLayer : XLayer, IShapeLayer
    {
        [Key(1)]
        public override XLayerType Type => XLayerType.Vector;

        [Key(6)]
        public ObservableCollection<XRenderable> Shapes
        {
            get => _shapes;
            set => SetProperty(ref _shapes, value);
        }
        private ObservableCollection<XRenderable> _shapes = new ObservableCollection<XRenderable>();

        #region Volatile

        [IgnoreMember]
        [Browsable(false)]
        public override bool CanUndo => Shapes.Count > 0;
        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(CanUndo));
        public XVectorLayer() => _shapes.CollectionChanged += CollectionChanged;
        public override object Clone() => new XVectorLayer
        {
            Title = Title,
            OffsetX = OffsetX,
            OffsetY = OffsetY,
            Shapes = new ObservableCollection<XRenderable>(Shapes.Select(shape => (XRenderable)shape.Clone()))
        };

        #endregion
    }

    [MessagePackObject]
    public class XRasterLayer : XLayer, IBitmapLayer
    {
        [Key(1)]
        public override XLayerType Type => XLayerType.Raster;

        [Key(6)]
        [Browsable(false)]
        public ImageSource Bitmap
        {
            get => _bitmap;
            set => SetProperty(ref _bitmap, value);
        }
        private ImageSource _bitmap;

        #region Volatile - Not Serialized
        
        [IgnoreMember]
        [Browsable(false)]
        public override bool CanUndo => false;

        public override object Clone() => new XRasterLayer
        {
            Title = this.Title,
            OffsetX = this.OffsetX,
            OffsetY = this.OffsetY,
            Bitmap = this.Bitmap
        };

        #endregion
    }

    [MessagePackObject]
    public class XHybridLayer : XLayer, IShapeLayer, IBitmapLayer
    {
        [Key(1)]
        public override XLayerType Type => XLayerType.Hybrid;

        [Key(6)]
        public int History
        {
            get => _history;
            set => SetProperty(ref _history, value);
        }
        private int _history = 5;

        [Key(7)]
        public ObservableCollection<XRenderable> Shapes
        {
            get => _shapes;
            set => SetProperty(ref _shapes, value);
        }
        private ObservableCollection<XRenderable> _shapes = new ObservableCollection<XRenderable>();

        [Key(8)]
        [Browsable(false)]  
        public ImageSource Bitmap
        {
            get => _bitmap;
            set => SetProperty(ref _bitmap, value);
        }
        private ImageSource _bitmap;

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Browsable(false)]
        public override bool CanUndo => Shapes.Count > 0;

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(CanUndo));
        public XHybridLayer() => _shapes.CollectionChanged += CollectionChanged;

        public override object Clone() => new XHybridLayer
        {
            Title = Title,
            OffsetX = OffsetX,
            OffsetY = OffsetY,
            Shapes = new ObservableCollection<XRenderable>(Shapes.Select(shape => (XRenderable)shape.Clone())),
            Bitmap = this.Bitmap
        };

        #endregion
    }
}
