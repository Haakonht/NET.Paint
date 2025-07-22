using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Mvvm;
using NET.Paint.ViewModels.Drawing;
using NET.Paint.ViewModels.Drawing.Utility;

namespace NET.Paint.Drawing.Model.Structure
{
    public class ImageViewModel : PropertyNotifier, ICloneable
    {
        public required XImage Model { get; set; }

        public string Title
        {
            get => Model.Title;
            set => SetProperty(ref Model.Title, value);
        }

        public double Width
        {
            get => Model.Width;
            set
            {
                SetProperty(ref Model.Width, value);
                OnPropertyChanged(nameof(Center));
            }
        }

        public double Height
        {
            get => Model.Height;
            set
            {
                SetProperty(ref Model.Height, value);
                OnPropertyChanged(nameof(Center));
            }
        }

        [Browsable(false)]
        public Point Center => new Point(Width / 2, Height / 2);

        public Color Background
        {
            get => Model.Background;
            set => SetProperty(ref Model.Background, value);
        }

        private ObservableCollection<LayerViewModel> _layers;
        public ObservableCollection<LayerViewModel> Layers
        {
            get
            {
                if (_layers == null)
                    _layers = new ObservableCollection<LayerViewModel>(Model.Layers.Select(x => x.Type == Constant.XLayerType.Vector ? new VectorLayerViewModel { Model = x } as LayerViewModel : x.Type == Constant.XLayerType.Hybrid ? new HybridLayerViewModel { Model = x } : new RasterLayerViewModel { Model = x }));
                return _layers;
            }
            init => SetProperty(ref _layers, value);
        }

        #region Volatile

        private bool _isEditing = false;
        
        [Browsable(false)]
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }

        private ObservableCollection<object> _selected = new ObservableCollection<object>();

        [Browsable(false)]
        public ObservableCollection<object> Selected
        {
            get => _selected;
        }

        public bool CanCut => Selected is IShapeLayer ? Layers.Count() > 1 : CanCopy;
        public bool CanCopy => Selected.Count > 0 && !Selected.Any(item => item is ImageViewModel);

        private LayerViewModel? _activeLayer = null;

        [Browsable(false)]
        public LayerViewModel? ActiveLayer
        {
            get => _activeLayer != null ? _activeLayer : _layers.Any() ? _layers.First() : null;
            set => SetProperty(ref _activeLayer, value);
        }

        [Browsable(false)]
        public UndoViewModel Undo { get; } = new UndoViewModel();

        [Browsable(false)]
        public ConfigurationViewModel Configuration { get; } = new ConfigurationViewModel();

        public ImageViewModel() => _selected.CollectionChanged += (s, e) => {
            OnPropertyChanged(nameof(Selected));
            OnPropertyChanged(nameof(CanCut));
            OnPropertyChanged(nameof(CanCopy));
        };

        public object Clone() => new ImageViewModel
        {
            Model = this.Model,
            Layers = new ObservableCollection<LayerViewModel>(Model.Layers.Select(x => x.Type == Constant.XLayerType.Vector ? new VectorLayerViewModel { Model = x } as LayerViewModel : x.Type == Constant.XLayerType.Hybrid ? new HybridLayerViewModel { Model = x } : new RasterLayerViewModel { Model = x })),
            IsEditing = this.IsEditing,
            ActiveLayer = this.ActiveLayer
        };

        #endregion
    }
}
