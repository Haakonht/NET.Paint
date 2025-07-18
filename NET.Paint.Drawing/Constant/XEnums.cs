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
        Pencil,
        Line,
        Curve,
        Bezier,
        Ellipse,
        Triangle,
        Rectangle,
        Polygon,
        Text,
        Bitmap
    }

    public enum XLayerType
    {
        Vector,
        Raster,
        Hybrid
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

    public enum XPencilMode
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

    #region Effects

    public enum  XEffect 
    {
        Fade,
        Vignette
    }

    #endregion
}
