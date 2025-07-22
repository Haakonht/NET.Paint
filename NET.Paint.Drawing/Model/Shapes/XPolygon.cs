using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Structure;

namespace NET.Paint.Drawing.Model.Shape
{
    public abstract class XPolygon : XFilledShape
    {
        public override XToolType Type => XToolType.Polygon;
        public abstract XPolygonStyle Style { get; }
        public double Rotation = 0;
    }

    public class XTriangle : XPolygon
    {
        public override XToolType Type => XToolType.Triangle;
        public override XPolygonStyle Style => XPolygonStyle.Triangle;
    }

    public class XRegular : XPolygon
    {
        public override XPolygonStyle Style => XPolygonStyle.Regular;
        public int Corners = 5;
    }

    public class XStar : XPolygon
    {
        public override XPolygonStyle Style => XPolygonStyle.Star;
        public int Points = 5;
    }

    public class XCloud : XPolygon
    {
        public override XPolygonStyle Style => XPolygonStyle.Cloud;
        public int Bumps = 5;
        public double BumpVariance = 10;
    }

    public class XHeart : XPolygon
    {
        public override XPolygonStyle Style => XPolygonStyle.Heart;

        public int HeartSamples = 64;
    }

    public class XSpiral : XPolygon
    {
        public override XPolygonStyle Style => XPolygonStyle.Spiral;
        public int SpiralSamples = 100;
        public int Turns = 3;
    }

    public class XArrow : XPolygon
    {
        public override XPolygonStyle Style => XPolygonStyle.Arrow;
        public double HeadLength = 30;
        public double HeadWidth = 20;
        public double TailWidth = 5;
    }
}
