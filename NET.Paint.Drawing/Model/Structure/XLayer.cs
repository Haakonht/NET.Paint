using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace NET.Paint.Drawing.Model.Structure
{
    public abstract class XLayer : PropertyNotifier, ICloneable
    {
        [JsonIgnore]
        public abstract LayerType Type { get; }

        private string _title = "Layer";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private double _offsetX = 0;
        public double OffsetX
        {
            get => _offsetX;
            set
            {
                SetProperty(ref _offsetX, value);
                OnPropertyChanged(nameof(Offset));
            }
        }

        private double _offsetY = 0;
        public double OffsetY
        {
            get => _offsetY;
            set
            {
                SetProperty(ref _offsetY, value);
                OnPropertyChanged(nameof(Offset));
            }
        }

        [Browsable(false)]
        public Point Offset => new Point(OffsetX, OffsetY);

        private double _rotation = 0;
        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }

        private bool _isVisible = true;
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

        [Browsable(false)]
        public Visibility Visibility
        {
            get
            {
                if (!_isVisible) return Visibility.Collapsed;
                return Visibility.Visible;
            }
        }

        public abstract bool CanUndo { get; }
        public abstract object Clone();
    }

    public class XVectorLayer : XLayer, IShapeLayer
    {
        public override LayerType Type => LayerType.Vector;

        private ObservableCollection<XRenderable> _shapes = new ObservableCollection<XRenderable>();
        public ObservableCollection<XRenderable> Shapes
        {
            get => _shapes;
            set => SetProperty(ref _shapes, value);
        }

        #region Volatile

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

    public class XRasterLayer : XLayer, IBitmapLayer
    {
        public override LayerType Type => LayerType.Raster;

        private BitmapSource _bitmap = new RenderTargetBitmap(100, 100, 96, 96, PixelFormats.Pbgra32);
        [Browsable(false)]
        public BitmapSource Bitmap
        {
            get => _bitmap;
            set => SetProperty(ref _bitmap, value);
        }

        [Browsable(false)]
        public override bool CanUndo => false;

        #region Volatile

        public override object Clone() => new XRasterLayer
        {
            Title = this.Title,
            OffsetX = this.OffsetX,
            OffsetY = this.OffsetY,
            Bitmap = this.Bitmap.Clone()
        };

        #endregion
    }

    public class XHybridLayer : XLayer, IShapeLayer, IBitmapLayer
    {
        public override LayerType Type => LayerType.Hybrid;

        private ObservableCollection<XRenderable> _shapes = new ObservableCollection<XRenderable>();
        public ObservableCollection<XRenderable> Shapes
        {
            get => _shapes;
            set => SetProperty(ref _shapes, value);
        }

        private BitmapSource _bitmap = new RenderTargetBitmap(100, 100, 96, 96, PixelFormats.Pbgra32);
        [Browsable(false)]  
        public BitmapSource Bitmap
        {
            get => _bitmap;
            set => SetProperty(ref _bitmap, value);
        }

        [Category("Configuration")]
        private int _history = 5;
        public int History
        {
            get => _history;
            set => SetProperty(ref _history, value);
        }

        #region Volatile

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
            Bitmap = this.Bitmap.Clone()
        };

        #endregion
    }
}
