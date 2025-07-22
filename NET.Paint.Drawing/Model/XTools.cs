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

        public bool Drag = false;
        public Point ClickLocation = new Point(0, 0);
        public Point MouseLocation = new Point(0, 0);

        #endregion

        #region Color

        public Color PrimaryColor = Colors.Black;
        public Color SecondaryColor = Colors.LightGray;

        #endregion

        #region Stroke

        public XGradientStyle ActiveStrokeGradientStyle = XGradientStyle.Linear;
        public XColorType ActiveStrokeType = XColorType.Solid;
        public DashStyle StrokeStyle = DashStyles.Solid;
        public double StrokeThickness = 1.0;


        #endregion

        #region Fill

        public XGradientStyle _activeFillGradientStyle = XGradientStyle.Linear;
        public XColorType _activeFillType = XColorType.Solid;

        #endregion

        #region Font

        public FontFamily _fontFamily = new FontFamily("Arial");


        public double _fontSize = 20;
        public bool _isBold = false;
        public bool _isItalic = false;
        public bool _isUnderline = false;
        public bool _isStrikethrough = false;

        #endregion

        #region Star

        public int _starPoints = 5;

        public double _starInnerRadiusRatio = 0.5;

        #endregion

        #region Cloud

        public int _cloudBumps = 8;
        public double _bumpVariance = 0.3;

        #endregion

        #region RegularPolygon

        public int _polygonCorners = 5;

        #endregion

        #region Polyline

        public double _pencilSpacing = 13.0;
        public double _eraserTolerance = 10.0;

        #endregion

        #region Arrow

        public double _headLength = 30;
        public double _headWidth = 20;
        public double _tailWidth = 5;

        #endregion

        #region Spiral

        public int _spiralSamples = 100;
        public int _turns = 3;

        #endregion

        #region Heart

        public int _heartSamples = 64;

        #endregion

        #region Rectangle

        public double _radius = 1;

        #endregion

        #region Bitmap

        public ImageSource? ActiveBitmap = null;

        #endregion

    }
}
