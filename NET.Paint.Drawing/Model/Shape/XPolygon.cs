using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model.Structure;
using System.Collections.Specialized;
using System.Windows;

namespace NET.Paint.Drawing.Model.Shape
{
    public abstract class XPolygon : XFilled
    {
        public abstract override ToolType Type { get; }
    }

    public class XTriangle : XPolygon
    {
        public override ToolType Type => ToolType.Triangle;
    }
    public class XPentagon : XPolygon
    {
        public override ToolType Type => ToolType.Pentagon;
    }
    public class XHexagon : XPolygon
    {
        public override ToolType Type => ToolType.Hexagon;
    }
    public class XOctagon : XPolygon
    {
        public override ToolType Type => ToolType.Octagon;
    }
    public class XStar : XPolygon
    {
        public override ToolType Type => ToolType.Star;
    }
}
