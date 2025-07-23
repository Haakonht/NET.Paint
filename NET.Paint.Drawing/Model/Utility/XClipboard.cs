using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;

namespace NET.Paint.Drawing.Model.Utility
{
    public class XClipboard : PropertyNotifier
    {
        private static readonly Lazy<XClipboard> _instance = new(() => new XClipboard());
        public static XClipboard Instance => _instance.Value;
        private XClipboard() => Data.CollectionChanged += (s, e) => OnPropertyChanged(nameof(CanPaste));
        
        public ObservableCollection<XObject> Data { get; } = new ObservableCollection<XObject>();

        public bool CanPaste => Data.Count > 0 && !Data.Any(item => item is XImage);

        private bool _isCut = false;
        public bool IsCut
        {
            get => _isCut;
            set => SetProperty(ref _isCut, value);
        }
    }
}
