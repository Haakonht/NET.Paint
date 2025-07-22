using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Mvvm;

namespace NET.Paint.Drawing.Model.Structure
{
    public class ImageViewModel : PropertyNotifier, ICloneable
    {
        public XImage Model { get; set; }

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

        private ObservableCollection<LayerViewModel> _layers = new ObservableCollection<LayerViewModel>()
        public ObservableCollection<LayerViewModel> Layers
        {
            get => _layers;
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
            Title = this.Title,
            Width = this.Width,
            Height = this.Height,
            Background = this.Background,
            Layers = new ObservableCollection<LayerViewModel>(this.Layers.Select(layer => (LayerViewModel)layer.Clone())),
            IsEditing = this.IsEditing,
            ActiveLayer = this.ActiveLayer
        };

        #endregion
    }
}
