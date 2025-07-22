using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Structure;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Shape
{
    public class XText : XRenderable
    {
        public override XToolType Type => XToolType.Text;
        public string Text = "";
        public FontFamily FontFamily = new FontFamily("Arial");
        public double FontSize = 11;
        public bool Bold = false;
        public bool Italic = false;
        public bool Underline = false;
        public bool Strikethrough = false;
        public Brush TextColor = Brushes.Black;
        public double Rotation = 0;
    }

    public class XBitmap : XRenderable
    {
        public override XToolType Type => XToolType.Bitmap;
        public double Rotation = 0;
        public Point ClipOffset = new Point(0, 0);
        public ImageSource? Source = null;
        public XScalingMode Scaling = XScalingMode.Original;
    }
}
