using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Structure
{
    public class XImage
    {
        public string Title = "Untitled";
        public double Width = 1900;
        public double Height = 1080;
        public Color Background = Colors.White;
        public List<XLayer> Layers = new List<XLayer>();
    }
}
