using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Interface;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NET.Paint.Drawing.Model.Structure
{
    public abstract class XLayer
    {
        public abstract XLayerType Type { get; }
        public string Title = "Layer";
        public double OffsetX = 0;
        public double OffsetY = 0;
        public double Rotation = 0;
    }

    public class XVectorLayer : XLayer, IShapeLayer
    {
        public override XLayerType Type => XLayerType.Vector;
        public List<XRenderable> Shapes { get; set; } = new List<XRenderable>();
    }

    public class XRasterLayer : XLayer, IBitmapLayer
    {
        public override XLayerType Type => XLayerType.Raster;
        public BitmapSource Bitmap { get; set; } = new RenderTargetBitmap(100, 100, 96, 96, PixelFormats.Pbgra32);
    }

    public class XHybridLayer : XLayer, IShapeLayer, IBitmapLayer
    {
        public override XLayerType Type => XLayerType.Hybrid;
        public List<XRenderable> Shapes { get; set; } = new List<XRenderable>();
        public BitmapSource Bitmap { get; set; } = new RenderTargetBitmap(100, 100, 96, 96, PixelFormats.Pbgra32);
        public int History = 5;
    }
}
