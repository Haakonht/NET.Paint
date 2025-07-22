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

namespace NET.Paint.Drawing.Model.Structure
{
    public abstract class LayerViewModel : PropertyNotifier, ICloneable
    {
        public required XLayer Model { get; set; }

        public string Title
        {
            get => Model.Title;
            set => SetProperty(ref Model.Title, value);
        }
        public double OffsetX
        {
            get => Model.OffsetX;
            set
            {
                SetProperty(ref Model.OffsetX, value);
                OnPropertyChanged(nameof(Offset));
            }
        }
        public double OffsetY
        {
            get => Model.OffsetY;
            set
            {
                SetProperty(ref Model.OffsetY, value);
                OnPropertyChanged(nameof(Offset));
            }
        }
        public double Rotation
        {
            get => Model.Rotation;
            set => SetProperty(ref Model.Rotation, value);
        }

        [Browsable(false)]
        public Point Offset => new Point(OffsetX, OffsetY);

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

        private bool _isEditing = false;
        [Browsable(false)]
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }

        public abstract XLayerType Type { get; }
        public abstract bool CanUndo { get; }
        public abstract object Clone();
    }

    public class VectorLayerViewModel : LayerViewModel
    {
        public override XLayerType Type => XLayerType.Vector;

        private ObservableCollection<RenderableViewModel> _shapes;
        public ObservableCollection<RenderableViewModel> Shapes
        {
            get
            {
                if (_shapes == null)
                    _shapes = new ObservableCollection<RenderableViewModel>();
                return _shapes;
            }
            set => SetProperty(ref _shapes, value);
        }

        #region Volatile

        public override bool CanUndo => Shapes.Count > 0;
        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(CanUndo));
        public VectorLayerViewModel() => _shapes.CollectionChanged += CollectionChanged;

        public override object Clone() => new VectorLayerViewModel
        {
            Title = Title,
            OffsetX = OffsetX,
            OffsetY = OffsetY,
            Shapes = new ObservableCollection<RenderableViewModel>(Shapes.Select(shape => (RenderableViewModel)shape.Clone()))
        };

        #endregion
    }

    public class RasterLayerViewModel : LayerViewModel, IBitmapLayer
    {
        public override XLayerType Type => XLayerType.Raster;

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

        public override object Clone() => new RasterLayerViewModel
        {
            Title = this.Title,
            OffsetX = this.OffsetX,
            OffsetY = this.OffsetY,
            Bitmap = this.Bitmap.Clone()
        };

        #endregion
    }

    public class HybridLayerViewModel : LayerViewModel, IShapeLayer, IBitmapLayer
    {
        public override XLayerType Type => XLayerType.Hybrid;

        private ObservableCollection<RenderableViewModel> _shapes = new ObservableCollection<RenderableViewModel>();
        public ObservableCollection<RenderableViewModel> Shapes
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
        public HybridLayerViewModel() => _shapes.CollectionChanged += CollectionChanged;

        public override object Clone() => new HybridLayerViewModel
        {
            Title = Title,
            OffsetX = OffsetX,
            OffsetY = OffsetY,
            Shapes = new ObservableCollection<RenderableViewModel>(Shapes.Select(shape => (RenderableViewModel)shape.Clone())),
            Bitmap = this.Bitmap.Clone()
        };

        #endregion
    }
}
