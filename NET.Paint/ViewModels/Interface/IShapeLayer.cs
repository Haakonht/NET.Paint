using NET.Paint.Drawing.Model.Structure;
using NET.Paint.ViewModels.Drawing.Structure;
using System.Collections.ObjectModel;

namespace NET.Paint.ViewModels.Interface
{
    public interface IShapeLayer
    {
        public ObservableCollection<RenderableViewModel> Shapes { get; set; }
    }
}
