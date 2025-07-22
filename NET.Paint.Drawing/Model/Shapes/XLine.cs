using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Structure;

namespace NET.Paint.Drawing.Model.Shape
{
    public class XLine : XStrokedShape
    {
        public override XToolType Type => XToolType.Line;
    }

    public class XPolyline : XStrokedShape
    {
        public override XToolType Type => XToolType.Pencil;
        public double Spacing = 13.0;
    }

    public class XCurve : XStrokedShape
    {
        public override XToolType Type => XToolType.Curve;
    }

    public class XBezier : XCurve
    {
        public override XToolType Type => XToolType.Bezier;
    }
}
