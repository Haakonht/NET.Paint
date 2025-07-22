using NET.Paint.Drawing.Mvvm;
using System.Windows.Media;

namespace NET.Paint.ViewModels.Drawing
{
    public class ConfigurationViewModel : PropertyNotifier
    {

        #region Zoom

        private double _zoom = 1.0;
        public double Zoom
        {
            get => _zoom;
            set => SetProperty(ref _zoom, value);
        }

        #endregion

        #region Grid

        private bool _gridEnabled = true;
        public bool GridEnabled
        {
            get => _gridEnabled;
            set => SetProperty(ref _gridEnabled, value);
        }

        private int _gridHeight = 10;
        public int GridHeight
        {
            get => _gridHeight;
            set => SetProperty(ref _gridHeight, value);
        }

        private int _gridWidth = 10;
        public int GridWidth
        {
            get => _gridWidth;
            set => SetProperty(ref _gridWidth, value);
        }

        private Color _gridColor = Colors.DimGray;
        public Color GridColor
        {
            get => _gridColor;
            set => SetProperty(ref _gridColor, value);
        }

        #endregion

        #region Ruler

        private bool _rulerEnabled = true;
        public bool RulerEnabled
        {
            get => _rulerEnabled;
            set => SetProperty(ref _rulerEnabled, value);
        }

        private double _rulerScale = 5;
        public double RulerScale
        {
            get => _rulerScale;
            set => SetProperty(ref _rulerScale, value);
        }

        #endregion
    }
}
