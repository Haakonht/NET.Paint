using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Navigation;
using NET.Paint.Drawing.Command;
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
            set => SetProperty(ref _width, value);
        }

        private double _height = 1080;
        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        private Color _background = Colors.White;
        public Color Background
        {
            get => _background;
            set => SetProperty(ref _background, value);
        }

        private ObservableCollection<XLayer> _layers = new ObservableCollection<XLayer>() { new XLayer { Title = "Background" } };
        public ObservableCollection<XLayer> Layers => _layers;

        #region Volatile

        private object? _selected = null;
        [Browsable(false)]
        public object? Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }

        public XLayer? _activeLayer = null;
        [Browsable(false)]
        public XLayer? ActiveLayer
        {
            get => _activeLayer == null ? _layers.First() : _activeLayer;
            set => SetProperty(ref _activeLayer, value);
        }

        [Browsable(false)]
        public XUndo Undo { get; } = new XUndo();
        [Browsable(false)]
        public XTools Tools { get; } = new XTools();
        [Browsable(false)]
        public XCommand Command => new XCommand(this);

        #endregion
    }
}
