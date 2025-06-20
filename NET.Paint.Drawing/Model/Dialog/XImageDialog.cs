using NET.Paint.Drawing.Mvvm;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Dialog
{
    public class XImageDialog : PropertyNotifier
    {
        private string _title = "";
        private double _width = 1600;
        private double _height = 900;
        private Color _background = Colors.White;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        public Color Background
        {
            get => _background;
            set => SetProperty(ref _background, value);
        }
    }
}
