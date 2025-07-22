using NET.Paint.Drawing.Model.Structure;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace NET.Paint.ViewModels.Interface
{
    public interface IBitmapLayer
    {
        public BitmapSource Bitmap { get; set; }
    }
}
