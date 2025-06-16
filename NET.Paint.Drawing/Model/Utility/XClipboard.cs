using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Mvvm;

namespace NET.Paint.Drawing.Model.Utility
{
    public class XClipboard : PropertyNotifier
    {
        private static readonly Lazy<XClipboard> _instance = new(() => new XClipboard());
        public static XClipboard Instance => _instance.Value;
        private XClipboard() { }


        public bool IsCut = false;
        public bool CanPaste => Data != null;

        private object? _data = null;
        public object? Data
        {
            get => _data;
            set
            {
                SetProperty(ref _data, value);
                OnPropertyChanged(nameof(CanPaste));
            }
        }
    }
}
