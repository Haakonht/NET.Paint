using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Mvvm;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model
{
    public class XTools : PropertyNotifier
    {
        
        #region Shape

        private ToolType _activeTool = ToolType.Line;
        public ToolType ActiveTool
        {
            get => _activeTool;
            set => SetProperty(ref _activeTool, value);
        }

        private Point? _clickLocation = null;
        public Point? ClickLocation
        {
            get => _clickLocation;
            set => SetProperty(ref _clickLocation, value);  
        }

        private Point? _mouseLocation = null;
        public Point? MouseLocation
        {
            get => _mouseLocation;
            set => SetProperty(ref _mouseLocation, value);
        }

        private int _corners = 5;
        public int Corners
        {
            get => _corners;
            set => SetProperty(ref _corners, value);
        }

        #endregion

        #region Stroke

        private XStrokeStyle _strokeStyle = XConstants.StrokeStyleOptions.First(x => x.Name == "Solid");
        public XStrokeStyle StrokeStyle
        {
            get => _strokeStyle;
            set => SetProperty(ref _strokeStyle, value);
        }

        private Color _strokeColor = Colors.Black;
        public Color StrokeColor
        {
            get => _strokeColor;
            set => SetProperty(ref _strokeColor, value);
        }

        private double _strokeThickness = 1.0;
        public double StrokeThickness
        {
            get => _strokeThickness;
            set => SetProperty(ref _strokeThickness, value);
        }

        #endregion

        #region Fill

        private Color _fillColor = Colors.White;
        public Color FillColor
        {
            get => _fillColor;
            set => SetProperty(ref _fillColor, value);
        }


        #endregion

        #region Zoom

        private double _zoom = 1.0;
        public double Zoom
        {
            get => _zoom;
            set => SetProperty(ref _zoom, value);
        }

        #endregion

        #region Font

        private FontFamily _fontFamily = new FontFamily("Arial");
        public FontFamily FontFamily
        {
            get => _fontFamily;
            set => SetProperty(ref _fontFamily, value);
        }

        private double _fontSize = 11;
        public double FontSize
        {
            get => _fontSize;
            set => SetProperty(ref _fontSize, value);
        }

        private bool _isBold = false;
        public bool IsBold
        {
            get => _isBold;
            set => SetProperty(ref _isBold, value);
        }

        private bool _isItalic = false;
        public bool IsItalic
        {
            get => _isItalic;
            set => SetProperty(ref _isItalic, value);
        }

        private bool _isUnderline = false;
        public bool IsUnderline
        {
            get => _isUnderline;
            set => SetProperty(ref _isUnderline, value);
        }

        private bool _isStrikethrough = false;
        public bool IsStrikethrough
        {
            get => _isStrikethrough;
            set => SetProperty(ref _isStrikethrough, value);
        }

        private TextAlignment _textAlignment = TextAlignment.Left;
        public TextAlignment TextAlignment
        {
            get => _textAlignment;
            set => SetProperty(ref _textAlignment, value);
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

        private Color _gridColor = Colors.LightGray;
        public Color GridColor
        {
            get => _gridColor;
            set => SetProperty(ref _gridColor, value);
        }

        #endregion

        #region Ruler

        private bool _rulerEnabled = false;
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
