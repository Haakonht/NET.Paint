using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using NET.Paint.Drawing.Mvvm;

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
