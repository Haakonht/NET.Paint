
using System.Windows;

namespace NET.Paint.Drawing.Interface
{
    public interface IRotateable
    {
        public double Rotation { get; set; }
        public Point Center { get; }
    }
}
