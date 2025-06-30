using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using NET.Paint.Drawing.Model.Shape;
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

        private double _width = 1920;
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

        private object? _selected = null;
        [Browsable(false)]
        public object? Selected
        {
            get => _selected;
            set
            {
                SetProperty(ref _selected, value);
                OnPropertyChanged(nameof(CanCut));
                OnPropertyChanged(nameof(CanCopy));
            }
        }

        public bool CanCut => Selected is XVectorLayer ? Layers.Count() > 1 : CanCopy;
        public bool CanCopy => Selected != null && Selected is not XImage;

        private XLayer? _activeLayer = null;
        [Browsable(false)]
        public XLayer? ActiveLayer
        {
            get => _activeLayer != null ? _activeLayer : _layers.Any() ? _layers.First() : null;
            set => SetProperty(ref _activeLayer, value);
        }

        [Browsable(false)]
        public XUndo History { get; } = new XUndo();
        [Browsable(false)]
        public XConfiguration Configuration { get; } = new XConfiguration();

        #endregion
    }
}
