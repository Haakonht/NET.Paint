using NET.Paint.Drawing.Model.Structure;
using System.Collections.ObjectModel;

namespace NET.Paint.Drawing.Interface
{
    public interface IShapeLayer
    {
        public List<XRenderable> Shapes { get; set; }
    }
}
