using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Mvvm;

namespace NET.Paint.Drawing.Model.Structure
{
    public class XImage : PropertyNotifier
    {
        private string _title = "Untitled";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private double _width = 1900;
        public double Width
        {
            get => _width;
            set
            {
                SetProperty(ref _width, value);
                OnPropertyChanged(nameof(Center));
            }
        }

        private double _height = 1080;
        public double Height
        {
            get => _height;
            set
            {
                SetProperty(ref _height, value);
                OnPropertyChanged(nameof(Center));
            }
        }

        [Browsable(false)]
        public Point Center => new Point(Width / 2, Height / 2);

        private Color _background = Colors.White;
        public Color Background
        {
            get => _background;
            set => SetProperty(ref _background, value);
        }

        private ObservableCollection<XLayer> _layers = new ObservableCollection<XLayer>() { new XVectorLayer { Title = "Background" } };
        public ObservableCollection<XLayer> Layers => _layers;

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
        public bool CanCopy => Selected.Count > 0 && !Selected.Any(item => item is XImage);

        private XLayer? _activeLayer = null;

        [Browsable(false)]
        public XLayer? ActiveLayer
        {
            get => _activeLayer != null ? _activeLayer : _layers.Any() ? _layers.First() : null;
            set => SetProperty(ref _activeLayer, value);
        }

        [Browsable(false)]
        public XUndo Undo { get; } = new XUndo();

        [Browsable(false)]
        public XConfiguration Configuration { get; } = new XConfiguration();

        public XImage() => _selected.CollectionChanged += (s, e) => {
            OnPropertyChanged(nameof(Selected));
            OnPropertyChanged(nameof(CanCut));
            OnPropertyChanged(nameof(CanCopy));
        };

        #endregion
    }
}
