using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Structure;

namespace NET.Paint.Drawing.Model.Shape
{
    public class XEllipse : XFilledShape
    {
        public override XToolType Type => XToolType.Ellipse;
        public XEllipseStyle Style = XEllipseStyle.Ellipse;
        public double Rotation = 0;
    }
}
