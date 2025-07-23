using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using NET.Paint.Drawing.Model.Utility;
using MessagePack;
using NET.Paint.Drawing.Interface;

namespace NET.Paint.Drawing.Model.Structure
{
    [MessagePackObject]
    public class XImage : XObject
    {
        [Key(1)]
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _title = "Untitled";

        [Key(2)]
        public double Width
        {
            get => _width;
            set
            {
                SetProperty(ref _width, value);
                OnPropertyChanged(nameof(Center));
            }
        }
        private double _width = 1920;

        [Key(3)]
        public double Height
        {
            get => _height;
            set
            {
                SetProperty(ref _height, value);
                OnPropertyChanged(nameof(Center));
            }
        }
        private double _height = 1080;

        [Key(4)]
        public Color Background
        {
            get => _background;
            set => SetProperty(ref _background, value);
        }
        private Color _background = Colors.White;

        [Key(5)]
        public ObservableCollection<XLayer> Layers => _layers;
        private ObservableCollection<XLayer> _layers = new ObservableCollection<XLayer>() { new XVectorLayer { Title = "Background" } };

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Browsable(false)]
        public Point Center => new Point(Width / 2, Height / 2);

        [IgnoreMember]
        [Browsable(false)]
        public bool IsEditing
        {
            get => _isEditing;
            set => SetProperty(ref _isEditing, value);
        }
        private bool _isEditing = false;

        [IgnoreMember]
        [Browsable(false)]
        public ObservableCollection<object> Selected
        {
            get => _selected;
            set
            {
                SetProperty(ref _selected, value);
                OnPropertyChanged(nameof(CanCut));
                OnPropertyChanged(nameof(CanCopy));
            }
        }
        private ObservableCollection<object> _selected = new ObservableCollection<object>();

        [IgnoreMember]
        [Browsable(false)]
        public bool CanCut => Selected is IShapeLayer ? Layers.Count() > 1 : Selected is XRenderable ? true : false;
        
        [IgnoreMember]
        [Browsable(false)]
        public bool CanCopy => Selected != null;

        [IgnoreMember]
        [Browsable(false)]
        public XLayer? ActiveLayer
        {
            get => _activeLayer != null ? _activeLayer : _layers.Any() ? _layers.First() : null;
            set => SetProperty(ref _activeLayer, value);
        }
        private XLayer? _activeLayer = null;

        [IgnoreMember]
        [Browsable(false)]
        public XUndo Undo { get; } = new XUndo();
        
        [IgnoreMember]
        [Browsable(false)]
        public XConfiguration Configuration { get; } = new XConfiguration();

        #endregion
    }
}
