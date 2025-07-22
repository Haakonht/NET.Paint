using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Mvvm;

namespace NET.Paint.ViewModels.Drawing.Utility
{
    public class ShapePreviewViewModel : PropertyNotifier
    {
        private RenderableViewModel? _shape = null;
        public RenderableViewModel? Shape
        {
            get => _shape;
            set => SetProperty(ref _shape, value);
        }
    }
}
