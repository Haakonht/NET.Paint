using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Structure;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Shape
{
    public class XText : XRenderable
    {
        public override XToolType Type => XToolType.Text;
        public string Text { get; set; } = "";
        public FontFamily FontFamily { get; set; } = new FontFamily("Arial");
        public double FontSize { get; set; }   = 11;
        public bool Bold { get; set; } = false;
        public bool Italic { get; set; } = false;
        public bool Underline { get; set; } = false;
        public bool Strikethrough { get; set; } = false;
        public Brush TextColor { get; set; } = Brushes.Black;
        public double Rotation { get; set; } = 0;
    }

    public class XBitmap : XRenderable
    {
        public override XToolType Type => XToolType.Bitmap;
        public double Rotation { get; set; } = 0;
        public Point ClipOffset { get; set; } = new Point(0, 0);
        public ImageSource? Source { get; set; } = null;
        public XScalingMode Scaling { get; set; } = XScalingMode.Original;
    }
}
