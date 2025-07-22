using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model
{
    public class XTools
    {

        #region Tool / Shape Style / Mode

        public XToolType ActiveTool = XToolType.Line;

        public XPolygonStyle ActivePolygon = XPolygonStyle.Regular;
        public XRectangleStyle ActiveRectangle = XRectangleStyle.Rectangle;
        public XEllipseStyle ActiveEllipse = XEllipseStyle.Ellipse;
        
        public XSelectionMode SelectionMode = XSelectionMode.Pointer;
        public XPencilMode PencilMode = XPencilMode.Add;
        public XScalingMode ScalingMode = XScalingMode.Fit;

        #endregion

        #region Mouse

        private bool _drag = false;
        private Point _clickLocation = new Point(0, 0);
        private Point _mouseLocation = new Point(0, 0);

        #endregion

        #region Color

        private Color _primaryColor = Colors.Black;
        private Color _secondaryColor = Colors.LightGray;
        
        #endregion

        #region Stroke

        private XGradientStyle _activeStrokeGradientStyle = XGradientStyle.Linear;
        private XColorType _activeStrokeType = XColorType.Solid;
        private XStrokeStyle _strokeStyle = XConstants.StrokeStyleOptions.First(x => x.Name == "Solid");
        private double _strokeThickness = 1.0;


        #endregion

        #region Fill

        private XGradientStyle _activeFillGradientStyle = XGradientStyle.Linear;
        private XColorType _activeFillType = XColorType.Solid;

        #endregion

        #region Font

        private FontFamily _fontFamily = new FontFamily("Arial");


        private double _fontSize = 20;
        private bool _isBold = false;
        private bool _isItalic = false;
        private bool _isUnderline = false;
        private bool _isStrikethrough = false;

        #endregion

        #region Star

        private int _starPoints = 5;

        private double _starInnerRadiusRatio = 0.5;

        #endregion

        #region Cloud

        private int _cloudBumps = 8;
        private double _bumpVariance = 0.3;

        #endregion

        #region RegularPolygon

        private int _polygonCorners = 5;

        #endregion

        #region Polyline

        private double _pencilSpacing = 13.0;
        private double _eraserTolerance = 10.0;

        #endregion

        #region Arrow

        private double _headLength = 30;

        private double _headWidth = 20;

        private double _tailWidth = 5;

        #endregion

        #region Spiral

        private int _spiralSamples = 100;

        private int _turns = 3;

        #endregion

        #region Heart

        private int _heartSamples = 64;

        #endregion

        #region Rectangle

        private double _radius = 1;

        #endregion

        #region Bitmap

        private ImageSource? _activeBitmap = null;

        #endregion

    }
}
