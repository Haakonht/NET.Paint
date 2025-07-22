using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace NET.Paint.ViewModels.Drawing.Utility
{
    public class UndoViewModel : PropertyNotifier
    {
        private ObservableCollection<RenderableViewModel> _history = new ObservableCollection<RenderableViewModel>();
        public ObservableCollection<RenderableViewModel> History => _history;
        public void Push(RenderableViewModel item)
        {
            _history.Add(item);
            OnPropertyChanged(nameof(History));
        }
        public RenderableViewModel Pop()
        {
            var renderable = _history.Last();
            _history.Remove(renderable);
            OnPropertyChanged(nameof(History));
            return renderable;
        }

        public bool CanRedo => History?.Count > 0;
        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CanRedo));
        }
        public UndoViewModel() => _history.CollectionChanged += CollectionChanged;
    }
}
