using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace NET.Paint.Drawing.Model.Shape
{
    public abstract class XRenderable : PropertyNotifier
    {
        [Browsable(false)]
        public abstract ToolType Type { get; }

        private ObservableCollection<Point> _points;
        [Browsable(false)]
        public ObservableCollection<Point> Points
        {
            get => _points;
            set
            {
                SetProperty(ref _points, value);
                _points.CollectionChanged += CollectionChanged;
            }
        }

        public abstract void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
    }
}
