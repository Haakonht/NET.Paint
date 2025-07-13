using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model.Structure;
using System.Windows;
using System.Windows.Controls;

namespace NET.Paint.View.Component.Overview
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : UserControl
    {
        public History()
        {
            InitializeComponent();
        }

        private void RestoreSpecific(object sender, RoutedEventArgs e)
        {
            if (DataContext is XImage image && sender is MenuItem item)
                if (item.DataContext is XRenderable renderable && image.ActiveLayer is IShapeLayer shapeLayer)
                    shapeLayer.Shapes.Add(renderable);

            RemoveFromHistory(sender, e);
        }

        private void RemoveFromHistory(object sender, RoutedEventArgs e)
        {
            if (DataContext is XImage image && sender is MenuItem item)
                if (item.DataContext is XRenderable renderable)
                    image.Undo.History.Remove(renderable);
        }
    }
}
