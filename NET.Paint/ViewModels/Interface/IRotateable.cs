using System.Windows;

namespace NET.Paint.ViewModels.Interface
{
    public interface IRotateable
    {
        public double Rotation { get; set; }
        public Point Center { get; }
    }
}
