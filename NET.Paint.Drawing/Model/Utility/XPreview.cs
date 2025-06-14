using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Mvvm;

namespace NET.Paint.Drawing.Model.Utility
{
    public class XPreview : PropertyNotifier
    {
        private XRenderable? _shape = null;
        public XRenderable? Shape
        {
            get => _shape;
            set => SetProperty(ref _shape, value);
        }
    }
}
