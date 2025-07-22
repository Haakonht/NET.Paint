using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Helper;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Mvvm;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.ViewModels.Drawing
{
    public class ToolsViewModel : PropertyNotifier
    {
        private static readonly Lazy<ToolsViewModel> _instance = new(() => new ToolsViewModel());
        public static ToolsViewModel Instance => _instance.Value;
        public ToolsViewModel() { }

        #region Tool

        private XToolType _activeTool = XToolType.Line;
        public XToolType ActiveTool
        {
            get => _activeTool;
            set
            {
                SetProperty(ref _activeTool, value);
                if (value != XToolType.Bitmap)
                    SetProperty(ref _activeBitmap, null);
            }
        }

        private XPolygonStyle _activePolygon = XPolygonStyle.Regular;
        public XPolygonStyle ActivePolygon
        {
            get => _activePolygon;
            set => SetProperty(ref _activePolygon, value);
        }

        private XRectangleStyle _activeRectangle = XRectangleStyle.Rectangle;
        public XRectangleStyle ActiveRectangle
        {
            get => _activeRectangle;
            set => SetProperty(ref _activeRectangle, value);
        }

        private XEllipseStyle _activeEllipse = XEllipseStyle.Ellipse;
        public XEllipseStyle ActiveEllipse
        {
            get => _activeEllipse;
            set => SetProperty(ref _activeEllipse, value);
        }

        private XSelectionMode _selectionMode = XSelectionMode.Pointer;
        public XSelectionMode SelectionMode
        {
            get => _selectionMode;
            set => SetProperty(ref _selectionMode, value);
        }

        private XPencilMode _pencilMode = XPencilMode.Add;
        public XPencilMode PencilMode
        {
            get => _pencilMode;
            set => SetProperty(ref _pencilMode, value);
        }

        private XScalingMode _bitmapScaling = XScalingMode.Fit;
        public XScalingMode BitmapScaling
        {
            get => _bitmapScaling;
            set => SetProperty(ref _bitmapScaling, value);
        }

        #endregion

        #region Mouse

        private bool _drag = false;
        public bool Drag
        {
            get => _drag;
            set => SetProperty(ref _drag, value);
        }

        private Point _clickLocation = new Point(0, 0);
        public Point ClickLocation
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

        #region Color

        private Color _primaryColor = Colors.Black;
        public Color PrimaryColor
        {
            get => _primaryColor;
            set
            {
                SetProperty(ref _primaryColor, value);
                Stroke = XHelper.CreateColor(ActiveStrokeType, ActiveStrokeGradientStyle, value, SecondaryColor);
            }
        }

        private Color _secondaryColor = Colors.LightGray;
        public Color SecondaryColor
        {
            get => _secondaryColor;
            set
            {
                SetProperty(ref _secondaryColor, value);
                Fill = XHelper.CreateColor(ActiveFillType, ActiveFillGradientStyle, value, PrimaryColor);
            }
        }

        #endregion

        #region Stroke

        private XGradientStyle _activeStrokeGradientStyle = XGradientStyle.Linear;
        public XGradientStyle ActiveStrokeGradientStyle
        {
            get => _activeStrokeGradientStyle;
            set 
            {
                SetProperty(ref _activeStrokeGradientStyle, value);
                Stroke = XHelper.CreateColor(ActiveStrokeType, value, PrimaryColor, SecondaryColor);
            }
        }

        private XColorType _activeStrokeType = XColorType.Solid;
        public XColorType ActiveStrokeType
        {
            get => _activeStrokeType;
            set
            {
                SetProperty(ref _activeStrokeType, value);
                Stroke = XHelper.CreateColor(value, ActiveStrokeGradientStyle, PrimaryColor, SecondaryColor);
            }
        }

        private StrokeStyleViewModel _strokeStyle = XConstants.StrokeStyleOptions.First(x => x.Name == "Solid");
        public StrokeStyleViewModel StrokeStyle
        {
            get => _strokeStyle;
            set => SetProperty(ref _strokeStyle, value);
        }

        private double _strokeThickness = 1.0;
        public double StrokeThickness
        {
            get => _strokeThickness;
            set => SetProperty(ref _strokeThickness, value);
        }

        private ColorViewModel _stroke = new SolidColorViewModel { Color = Colors.Black };

        public ColorViewModel Stroke
        {
            get => _stroke;
            set => SetProperty(ref _stroke, value);
        }

        public Brush StrokeBrush
        {
            get             
            {
                if (Stroke is SolidColorViewModel solidFill)
                    return new SolidColorBrush(solidFill.Color);
                else if (Stroke is LinearGradientViewModel linearGradient)
                    return new LinearGradientBrush(new GradientStopCollection(linearGradient.GradientStops.Select(x => new GradientStop(x.Color, x.Offset))), linearGradient.StartPoint, linearGradient.EndPoint);
                else if (Stroke is RadialGradientViewModel radialGradient)
                    return new RadialGradientBrush(new GradientStopCollection(radialGradient.GradientStops.Select(x => new GradientStop(x.Color, x.Offset))));
                return Brushes.Transparent;
            }
        }

        #endregion

        #region Fill

        private XGradientStyle _activeFillGradientStyle = XGradientStyle.Linear;
        public XGradientStyle ActiveFillGradientStyle
        {
            get => _activeFillGradientStyle;
            set
            {
                SetProperty(ref _activeFillGradientStyle, value);
                Fill = XHelper.CreateColor(ActiveFillType, value, SecondaryColor, PrimaryColor);
            }
        }

        private XColorType _activeFillType = XColorType.Solid;
        public XColorType ActiveFillType
        {
            get => _activeFillType;
            set
            {
                SetProperty(ref _activeFillType, value);
                Fill = XHelper.CreateColor(value, ActiveFillGradientStyle, SecondaryColor, PrimaryColor);
            }
        }

        private ColorViewModel _fill = new SolidColorViewModel { Color = Colors.LightGray };

        public ColorViewModel Fill
        {
            get => _fill;
            set => SetProperty(ref _fill, value);
        }

        public Brush FillBrush
        {
            get
            {
                if (Fill is SolidColorViewModel solidFill)
                    return new SolidColorBrush(solidFill.Color);
                else if (Fill is LinearGradientViewModel linearGradient)
                    return new LinearGradientBrush(new GradientStopCollection(linearGradient.GradientStops.Select(x => new GradientStop(x.Color, x.Offset))), linearGradient.StartPoint, linearGradient.EndPoint);
                else if (Fill is RadialGradientViewModel radialGradient)
                    return new RadialGradientBrush(new GradientStopCollection(radialGradient.GradientStops.Select(x => new GradientStop(x.Color, x.Offset))));
                return Brushes.Transparent;
            }
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

        #region Star

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

        #endregion

        #region Cloud

        private int _cloudBumps = 8;
        public int CloudBumps
        {
            get => _cloudBumps;
            set => SetProperty(ref _cloudBumps, value);
        }

        private double _bumpVariance = 0.3;
        public double BumpVariance
        {
            get => _bumpVariance;
            set => SetProperty(ref _bumpVariance, value);
        }

        #endregion

        #region RegularPolygon

        private int _polygonCorners = 5;
        public int PolygonCorners
        {
            get => _polygonCorners;
            set => SetProperty(ref _polygonCorners, value);
        }

        #endregion

        #region Polyline

        private double _pencilSpacing = 13.0;
        public double PencilSpacing
        {
            get => _pencilSpacing;
            set => SetProperty(ref _pencilSpacing, value);
        }

        private double _eraserTolerance = 10.0;
        public double EraserTolerance
        {
            get => _eraserTolerance;
            set => SetProperty(ref _eraserTolerance, value);
        }

        #endregion

        #region Arrow

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

        #endregion

        #region Spiral

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

        #endregion

        #region Heart

        private int _heartSamples = 64;
        public int HeartSamples
        {
            get => _heartSamples;
            set => SetProperty(ref _heartSamples, value);
        }

        #endregion

        #region Rectangle

        private double _radius = 1;
        public double Radius
        {
            get => _radius;
            set => SetProperty(ref _radius, value);
        }

        #endregion

        #region Bitmap

        private ImageSource? _activeBitmap = null;
        public ImageSource? ActiveBitmap
        {
            get => _activeBitmap;
            set => SetProperty(ref _activeBitmap, value);
        }

        #endregion

    }
}
