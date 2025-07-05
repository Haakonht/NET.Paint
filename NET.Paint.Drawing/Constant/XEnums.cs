namespace NET.Paint.Drawing.Constant
{
    public enum ToolType
    {
        Pointer,
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

    public enum PolygonType
    {
        Triangle,
        Pentagon,
        Hexagon,
        Octagon,
        Heart,
        Spiral,
        Star,
        Arrow
    }

    public enum SelectionMode
    {
        Single,
        Rectangle,
        Lasso
    }

    public enum ImageScaling
    {
        Original,
        Fit,
        Clip
    }

    public enum LayerType
    {
        Vector,
        Raster,
        Hybrid
    }
}
