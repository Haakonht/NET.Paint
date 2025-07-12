using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Mvvm;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model
{
    public class XTools : PropertyNotifier
    {
        private static readonly Lazy<XTools> _instance = new(() => new XTools());
        public static XTools Instance => _instance.Value;
        public XTools() { }

        #region Shape

        private ToolType _activeTool = ToolType.Polygon;
        public ToolType ActiveTool
        {
            get => _activeTool;
            set
            {
                SetProperty(ref _activeTool, value);
                if (value != ToolType.Bitmap)
                    SetProperty(ref _activeBitmap, null);
            }
        }

        private PolygonType _activePolygon = PolygonType.Regular;
        public PolygonType ActivePolygon
        {
            get => _activePolygon;
            set => SetProperty(ref _activePolygon, value);
        }

        private SelectionMode _selectionMode = SelectionMode.Single;
        public SelectionMode SelectionMode
        {
            get => _selectionMode;
            set => SetProperty(ref _selectionMode, value);
        }

        private bool __isPolylineAdd = true;
        public bool IsPolylineAdd
        {
            get => __isPolylineAdd;
            set => SetProperty(ref __isPolylineAdd, value);
        }

        private bool _isCircle = false;
        public bool IsCircle
        {
            get => _isCircle;
            set => SetProperty(ref _isCircle, value);
        }

        private ImageScaling _bitmapScaling = ImageScaling.Fit;
        public ImageScaling BitmapScaling
        {
            get => _bitmapScaling;
            set => SetProperty(ref _bitmapScaling, value);
        }

        #endregion

        #region Mouse

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

        #endregion

        #region Custom

        // Star
        private int _starPoints = 5;
        public int StarPoints
        {
            get => _starPoints;
            set => SetProperty(ref _starPoints, value);
        }

        private double _starInnerRadiusRatio = 0.5;
        public double StarInnerRadiusRatio
        {
            get => _starInnerRadiusRatio;
            set => SetProperty(ref _starInnerRadiusRatio, value);
        }

        // Regular polygon
        private int _polygonCorners = 5;
        public int PolygonCorners
        {
            get => _polygonCorners;
            set => SetProperty(ref _polygonCorners, value);
        }

        // Pencil
        private double _spacing = 13.0;
        public double Spacing
        {
            get => _spacing;
            set => SetProperty(ref _spacing, value);
        }

        // Arrow
        private double _headLength = 30;
        public double HeadLength
        {
            get => _headLength;
            set => SetProperty(ref _headLength, value);
        }

        private double _headWidth = 20;
        public double HeadWidth
        {
            get => _headWidth;
            set => SetProperty(ref _headWidth, value);
        }

        private double _tailWidth = 5;
        public double TailWidth
        {
            get => _tailWidth;
            set => SetProperty(ref _tailWidth, value);
        }

        // Spiral
        private int _spiralSamples = 100;
        public int SpiralSamples
        {
            get => _spiralSamples;
            set => SetProperty(ref _spiralSamples, value);
        }

        private int _turns = 3;
        public int Turns
        {
            get => _turns;
            set => SetProperty(ref _turns, value);
        }

        // Heart
        private int _heartSamples = 64;
        public int HeartSamples
        {
            get => _heartSamples;
            set => SetProperty(ref _heartSamples, value);
        }

        // Rounded Rectangle
        private double _radius = 1;
        public double Radius 
        { 
            get => _radius;
            set => SetProperty(ref _radius, value);
        }

        // Bitmap
        private ImageSource? _activeBitmap = null;
        public ImageSource? ActiveBitmap
        {
            get => _activeBitmap;
            set => SetProperty(ref _activeBitmap, value);
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

        private double _fontSize = 20;
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

        //private TextAlignment _textAlignment = TextAlignment.Left;
        //public TextAlignment TextAlignment
        //{
        //    get => _textAlignment;
        //    set => SetProperty(ref _textAlignment, value);
        //}

        #endregion

    }
}
