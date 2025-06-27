using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Dialog;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using NET.Paint.View.Component.Dialog;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NET.Paint.View.Component
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class ImageTree : UserControl
    {
        private TreeViewItem _draggedTreeViewItem;

        public ImageTree()
        {
            InitializeComponent();
        }

        private void SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var context = DataContext as XService;

            if (context != null)
            {
                context.ActiveImage.Selected = e.NewValue;

                if (e.NewValue is XVectorLayer)
                    context.ActiveImage.ActiveLayer = e.NewValue as XVectorLayer;
                
                if (e.NewValue is XRenderable)
                    XTools.Instance.ActiveTool = ToolType.Selector;
            }
        }

        private void Unselect(object sender, MouseButtonEventArgs e)
        {
            var context = DataContext as XService;

            if (context != null && context.ActiveImage != null)
                context.ActiveImage.Selected = null;             
        }

        private void SelectImage(object sender, MouseButtonEventArgs e)
        {
            var context = DataContext as XService;

            if (context != null && context.ActiveImage != null)
                context.ActiveImage.Selected = context.ActiveImage;
        }

        private void AddLayer(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;

            if (context != null && context.ActiveImage != null)
            {
                var dialogModel = new XLayerDialog();
                var layerDialog = new LayerDialog(dialogModel);
                var result = layerDialog.ShowDialog();
                if (result == true && layerDialog.Result != null)
                {
                    context.Command.Operations.CreateLayer(layerDialog.Result.Title);
                }
            }
        }

        private void Flatten(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
            {
                if (sender is MenuItem item && item.DataContext is XVectorLayer layer) { }
                    //service.Command.FlattenLayer(layer);
            }
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;

            if (context != null)
            {
                var dialogModel = new XImageDialog();
                var imageDialog = new ImageDialog(dialogModel);
                var result = imageDialog.ShowDialog();
                if (result == true && imageDialog.Result != null)
                {
                    context.Command.Operations.CreateImage(new XImage
                    {
                        Title = imageDialog.Result.Title,
                        Width = imageDialog.Result.Width,
                        Height = imageDialog.Result.Height,
                        Background = imageDialog.Result.Background
                    });
                }
            }
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            if (DataContext is XService service && sender is MenuItem item)
            {
                if (item.DataContext is XLayer layer)
                    service.Command.Operations.RemoveLayer(layer);

                if (item.DataContext is XRenderable renderable)
                    service.Command.Operations.RemoveRenderable(renderable);
            }
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            if (DataContext is XService service && sender is MenuItem item)
                service.Command.Operations.Cut(item.DataContext);
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;
            var item = sender as MenuItem;

            if (context != null)
                context.Command.Operations.Copy(item.DataContext);
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;
            var item = sender as MenuItem;

            if (context != null)
                context.Command.Operations.Paste(item.DataContext);
        }

        private void TreeView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _draggedTreeViewItem = FindAncestor<TreeViewItem>((DependencyObject)e.OriginalSource);
        }

        private void TreeView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _draggedTreeViewItem != null)
            {
                {
                    var data = _draggedTreeViewItem.DataContext;
                    DragDrop.DoDragDrop(_draggedTreeViewItem, data, DragDropEffects.Move);  
                    _draggedTreeViewItem = null; // Reset after drag
                }
            }
        }

        private void TreeView_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Move;
            e.Handled = true;
        }

        private void TreeView_Drop(object sender, DragEventArgs e)
        {
            var context = DataContext as XService;
            if (context == null) return;

            var droppedLayer = _draggedTreeViewItem?.DataContext as XVectorLayer;
            var droppedShape = _draggedTreeViewItem?.DataContext as XRenderable;
            var targetItem = GetNearestContainer(e.OriginalSource as UIElement);

            if (targetItem == null) return;
            var targetData = targetItem.DataContext;

            // Layer reordering
            if (droppedLayer != null && targetData is XVectorLayer targetLayer && !ReferenceEquals(droppedLayer, targetLayer))
            {
                context.Command.Operations.MoveLayer(context.ActiveImage, droppedLayer, targetLayer);
            }
            // Shape moving/reordering
            else if (droppedShape != null)
            {
                if (targetData is XVectorLayer targetLayerForShape)
                {
                    context.Command.Operations.MoveShapeToLayer(context.ActiveImage, droppedShape, targetLayerForShape);
                }
                else if (targetData is XRenderable targetShape)
                {
                    context.Command.Operations.MoveShapeInFrontOfShape(context.ActiveImage, droppedShape, targetShape);
                }
            }
        }

        // Helper to find TreeViewItem
        private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            while (current != null)
            {
                if (current is T)
                    return (T)current;
                current = VisualTreeHelper.GetParent(current);
            }
            return null;
        }

        // Helper to get nearest TreeViewItem
        private TreeViewItem GetNearestContainer(UIElement element)
        {
            // Traverse up the visual tree to find the TreeViewItem
            return FindAncestor<TreeViewItem>(element);
        }
    }
}
