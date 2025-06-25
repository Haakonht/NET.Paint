using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text.Json.Serialization;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            set => SetProperty(ref _offsetX, value);
        }

        private double _offsetY = 0;
        public double OffsetY
        {
            get => _offsetY;
            set => SetProperty(ref _offsetY, value);
        }

        public abstract object Clone();
    }

    public class XVectorLayer : XLayer
    {
        public override LayerType Type => LayerType.Vector;

        private ObservableCollection<XRenderable> _shapes = new ObservableCollection<XRenderable>();
        public ObservableCollection<XRenderable> Shapes
        {
            get => _shapes;
            set => SetProperty(ref _shapes, value);
        }

        #region Volatile

        public bool CanUndo => Shapes.Count > 0;
        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CanUndo));
        }
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

    public class XRasterLayer : XLayer
    {
        public override LayerType Type => LayerType.Raster;

        private WriteableBitmap _bitmap = new WriteableBitmap(100, 100, 96, 96, PixelFormats.Bgr32, BitmapPalettes.WebPaletteTransparent);
        public WriteableBitmap Bitmap
        {
            get => _bitmap;
            set => SetProperty(ref _bitmap, value);
        }

        #region Volatile

        public override object Clone() => new XRasterLayer
        {
            Title = Title,
            OffsetX = OffsetX,
            OffsetY = OffsetY,
            Bitmap = new WriteableBitmap(Bitmap)
        };

        #endregion
    }
}
