using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Mvvm;

namespace NET.Paint.Drawing.Model.Structure
{
    [Serializable]
    public class XLayer : PropertyNotifier, ICloneable
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
            set => SetProperty(ref _shapes, value);
        }

        #region Volatile

        public bool CanUndo => Shapes.Count > 0;
        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(CanUndo));
        }
        public XLayer() => _shapes.CollectionChanged += CollectionChanged;

        public object Clone() => new XLayer
        {
            Title = Title,
            OffsetX = OffsetX,
            OffsetY = OffsetY,
            Shapes = new ObservableCollection<XRenderable>(Shapes.Select(shape => (XRenderable)shape.Clone()))
        };

        #endregion
    }
}
