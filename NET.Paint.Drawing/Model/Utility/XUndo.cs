using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace NET.Paint.Drawing.Model.Utility
{
    public class XUndo : PropertyNotifier
    {
        private ObservableCollection<XRenderable> _history = new ObservableCollection<XRenderable>();
        public ObservableCollection<XRenderable> History => _history;
        public void Push(XRenderable item)
        {
            _history.Add(item);
            OnPropertyChanged(nameof(History));
        }
        public XRenderable Pop()
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
        public XUndo() => _history.CollectionChanged += CollectionChanged;
    }
}
