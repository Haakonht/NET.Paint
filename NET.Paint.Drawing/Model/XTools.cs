using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Factory;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Mvvm;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model
{
    public class XTools : PropertyNotifier
    {
        private static readonly Lazy<XTools> _instance = new(() => new XTools());
        public static XTools Instance => _instance.Value;
        private XTools() { }

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

        private Point _mouseLocation = new Point(0, 0);
        public Point MouseLocation
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

        private double _resolution = 13.0;
        public double Resolution
        {
            get => _resolution;
            set => SetProperty(ref _resolution, value);
        }

        private double _headLength = 0.2;
        public double HeadLength
        {
            get => _headLength;
            set => SetProperty(ref _headLength, value);
        }

        private double _headWidth = 0.1;
        public double HeadWidth
        {
            get => _headWidth;
            set => SetProperty(ref _headWidth, value);
        }

        private int _turns = 3;
        public int Turns
        {
            get => _turns;
            set => SetProperty(ref _turns, value);
        }

        private double _radiusX = 10;
        public double RadiusX 
        { 
            get => _radiusX;
            set => SetProperty(ref _radiusX, value);
        }

        private double _radiusY = 10;
        public double RadiusY 
        {
            get => _radiusY;
            set => SetProperty(ref _radiusY, value);
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

    }
}
