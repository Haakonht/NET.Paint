namespace NET.Paint.Drawing.Constant
{

    #region Types

    public enum  XObjectType
    {
        Image,
        Layer,
        Shape
    }

    public enum XToolType
    {
        Selector,
        Polyline,
        Line,
        Curve,
        Bezier,
        Ellipse,
        Triangle,
        Rectangle,
        Polygon,
        Text,
        Bitmap,
        Effect
    }

    public enum XLayerType
    {
        Vector,
        Raster,
        Hybrid,
        Diagram
    }

    public enum XColorType
    {
        Solid,
        Gradient
    }

    #endregion

    #region Styles

    public enum XPolygonStyle
    {
        Triangle,
        Regular,
        Heart,
        Spiral,
        Star,
        Arrow,
        Cloud
    }

    public enum  XRectangleStyle
    {
        Square,
        Rectangle
    }

    public enum XEllipseStyle
    {
        Circle,
        Ellipse
    }

    public enum XGradientStyle
    {
        Linear,
        Radial
    }
    public enum XEffectStyle
    {
        Fade,
        Vignette
    }

    #endregion

    #region Modes

    public enum XSelectionMode
    {
        Move,
        Rotate,
        Pointer,
        Rectangle,
        Lasso
    }

    public enum XPolylineMode
    {
        Add,
        Remove
    }

    public enum XScalingMode
    {
        Original,
        Fit,
        Clip
    }

    #endregion

    #region Sources

    public enum  XNotificationSource
    {
        Project,
        Clipboard,
        History,
        Selection,
        Message
    }

    #endregion
}
