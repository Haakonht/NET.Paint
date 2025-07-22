using NET.Paint.Drawing.Constant;

namespace NET.Paint.Drawing.Model.Structure
{
    public class XRectangle : XFilledShape
    {
        public override XToolType Type => XToolType.Rectangle;
        public XRectangleStyle Style = XRectangleStyle.Rectangle;
        public double CornerRadius = 0;
        public double Rotation = 0;
    }
}
