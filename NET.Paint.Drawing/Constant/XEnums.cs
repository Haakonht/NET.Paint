namespace NET.Paint.Drawing.Constant
{

    #region Types

    public enum  ObjectType
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

    public enum LayerType
    {
        Vector,
        Raster,
        Hybrid
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
        Arrow
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

    #endregion

    #region Modes

    public enum XSelectionMode
    {
        Manipulator,
        Pointer,
        Rectangle,
        Lasso
    }

    public enum XScalingMode
    {
        Original,
        Fit,
        Clip
    }

    #endregion

}
