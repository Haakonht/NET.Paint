using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Mvvm;

namespace NET.Paint.Drawing.Model.Structure
{
    public class XLayer : PropertyNotifier
    {
        private string _title = "Layer";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private double _offsetX = 0;
        public double OffsetX
        {
            get => _offsetX;
            set => SetProperty(ref _offsetX, value);
        }

        private double _offsetY = 0;
        public double OffsetY
        {
            get => _offsetY;
            set => SetProperty(ref _offsetY, value);
        }

        private ObservableCollection<XRenderable> _shapes = new ObservableCollection<XRenderable>();
        public ObservableCollection<XRenderable> Shapes
        {
            get => _shapes;
        }

        #region Volatile

        public bool CanUndo => Shapes.Count > 0;
        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CanUndo));
        }
        public XLayer() => _shapes.CollectionChanged += CollectionChanged;

        #endregion
    }
}
