using System.Collections.ObjectModel;
using System.Windows.Media;
using NET.XDraw.Utility;

namespace NET.XDraw.Models.Structure
{
    public class XImage : Notifier
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
        private ObservableCollection<XLayer> Layers
        {
            get => _layers;
        }
    }
}
