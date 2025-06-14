using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Mvvm;

namespace NET.Paint.Drawing.Model.Utility
{
    public class XClipboard : PropertyNotifier
    {
        private Stack<XRenderable> _undo = new Stack<XRenderable>();
        public Stack<XRenderable> Undo
        {
            get => _undo;
            set => SetProperty(ref _undo, value);
        }

        private IEnumerable<XRenderable> _copy = Enumerable.Empty<XRenderable>();
        public IEnumerable<XRenderable> Copy
        {
            get => _copy;
            set => SetProperty(ref _copy, value);
        }

        private object? _selected = null;
        public object? Selected
        {
            get => _selected;
            set => SetProperty(ref _selected, value);
        }
    }
}
