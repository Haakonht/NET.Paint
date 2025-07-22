using NET.Paint.Drawing.Mvvm;
using NET.Paint.ViewModels.Drawing.Structure;
using System.Collections.ObjectModel;

namespace NET.Paint.ViewModels.Drawing.Utility
{
    public class ClipboardViewModel : PropertyNotifier
    {
        private static readonly Lazy<ClipboardViewModel> _instance = new(() => new ClipboardViewModel());
        public static ClipboardViewModel Instance => _instance.Value;
        private ClipboardViewModel() => Data.CollectionChanged += (s, e) => OnPropertyChanged(nameof(CanPaste));
        
        public ObservableCollection<object> Data { get; } = new ObservableCollection<object>();

        public bool CanPaste => Data.Count > 0 && !Data.Any(item => item is ImageViewModel);

        private bool _isCut = false;
        public bool IsCut
        {
            get => _isCut;
            set => SetProperty(ref _isCut, value);
        }
    }
}
