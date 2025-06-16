using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NET.Paint.View.Component
{
    /// <summary>
    /// Interaction logic for Overview.xaml
    /// </summary>
    public partial class ProjectTree : UserControl
    {
        private TreeViewItem _draggedTreeViewItem;

        public ProjectTree()
        {
            InitializeComponent();
        }

        private void SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var context = DataContext as XService;

            if (context != null)
            {
                if (e.NewValue is XImage image)
                    context.ActiveImage = image;

                if (e.NewValue is XLayer layer)
                {
                    var containingImg = context.Project.Images.FirstOrDefault(img => img.Layers.Contains(layer));
                    if (containingImg != null && containingImg != context.ActiveImage)
                        context.ActiveImage = containingImg;

                    context.ActiveImage.ActiveLayer = layer;
                }

                if (e.NewValue is XRenderable renderable)
                {
                    var containingImage = context.Project.Images.FirstOrDefault(img => img.Layers.Any(l => l.Shapes.Contains(renderable)));
                    if (containingImage != null && containingImage != context.ActiveImage)
                        context.ActiveImage = containingImage;
                    
                    var containingLayer = context.ActiveImage.Layers.FirstOrDefault(l => l.Shapes.Contains(renderable));
                    if (containingLayer != null && containingLayer != context.ActiveImage.ActiveLayer)
                        context.ActiveImage.ActiveLayer = containingLayer;

                    XTools.Instance.ActiveTool = ToolType.Selector;
                }

                context.ActiveImage.Selected = e.NewValue;
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

            if (context != null)
                context.Command.CreateLayer();
        }

        private void AddImage(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;

            if (context != null)
                context.Command.CreateImage("Testimage");
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;
            var item = sender as MenuItem;

            if (context != null)
            {
                if (item.DataContext is XLayer layer)
                    context.Command.RemoveLayer(layer);

                if (item.DataContext is XRenderable renderable)
                    context.Command.RemoveRenderable(renderable);
            }
        }

        private void Cut(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;
            var item = sender as MenuItem;

            if (context != null)
                context.Command.Cut(item.DataContext);
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;
            var item = sender as MenuItem;

            if (context != null)
                context.Command.Copy(item.DataContext);
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            var context = DataContext as XService;
            var item = sender as MenuItem;

            if (context != null)
                context.Command.Paste(item.DataContext);
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

            var draggedImage = _draggedTreeViewItem?.DataContext as XImage;
            var draggedLayer = _draggedTreeViewItem?.DataContext as XLayer;
            var targetItem = GetNearestContainer(e.OriginalSource as UIElement);

            if (targetItem == null) return;
            var targetData = targetItem.DataContext;

            // 1. Reorder images
            if (draggedImage != null && targetData is XImage targetImage && !ReferenceEquals(draggedImage, targetImage))
            {
                MoveImage(context.Project, draggedImage, targetImage);
            }
            // 2. Move layer into another image
            else if (draggedLayer != null && targetData is XImage targetImageForLayer)
            {
                MoveLayerToImage(context.Project, draggedLayer, targetImageForLayer);
            }
            // 3. Existing logic for layer reordering and shape moving
            else if (draggedLayer != null && targetData is XLayer targetLayer && !ReferenceEquals(draggedLayer, targetLayer))
            {
                MoveLayer(context.ActiveImage, draggedLayer, targetLayer);
            }
            else if (_draggedTreeViewItem?.DataContext is XRenderable droppedShape)
            {
                if (targetData is XLayer targetLayerForShape)
                {
                    MoveShapeToLayer(context.ActiveImage, droppedShape, targetLayerForShape);
                }
                else if (targetData is XRenderable targetShape)
                {
                    MoveShapeInFrontOfShape(context.ActiveImage, droppedShape, targetShape);
                }
            }
        }

        private void MoveImage(XProject project, XImage imageToMove, XImage targetImage)
        {
            if (imageToMove == null || targetImage == null || ReferenceEquals(imageToMove, targetImage))
                return;

            var images = project.Images;
            int oldIndex = images.IndexOf(imageToMove);
            int targetIndex = images.IndexOf(targetImage);

            if (oldIndex < 0 || targetIndex < 0 || oldIndex == targetIndex)
                return;

            images.RemoveAt(oldIndex);
            if (oldIndex < targetIndex) targetIndex--;
            images.Insert(targetIndex, imageToMove);
        }

        private void MoveLayerToImage(XProject project, XLayer layerToMove, XImage targetImage)
        {
            if (layerToMove == null || targetImage == null)
                return;

            // Remove from old image
            var oldImage = project.Images.FirstOrDefault(img => img.Layers.Contains(layerToMove));
            oldImage?.Layers.Remove(layerToMove);

            // Add to new image (at end)
            targetImage.Layers.Add(layerToMove);
        }

        private void MoveLayer(XImage context, XLayer layerToMove, XLayer targetLayer)
        {
            if (layerToMove == null || targetLayer == null || ReferenceEquals(layerToMove, targetLayer))
                return;

            var layers = context.Layers;
            int oldIndex = layers.IndexOf(layerToMove);
            int targetIndex = layers.IndexOf(targetLayer);

            if (oldIndex < 0 || targetIndex < 0 || oldIndex == targetIndex)
                return;

            layers.RemoveAt(oldIndex);

            // Adjust target index if removing an earlier item shifts the target
            if (oldIndex < targetIndex) targetIndex--;

            layers.Insert(targetIndex, layerToMove);
        }

        private void MoveShapeToLayer(XImage context, XRenderable shapeToMove, XLayer targetLayer)
        {
            if (shapeToMove == null || targetLayer == null)
                return;

            // Remove from old layer
            var oldLayer = context.Layers.FirstOrDefault(l => l.Shapes.Contains(shapeToMove));
            oldLayer?.Shapes.Remove(shapeToMove);

            // Add to new layer (at end)
            targetLayer.Shapes.Add(shapeToMove);
        }

        private void MoveShapeInFrontOfShape(XImage context, XRenderable shapeToMove, XRenderable targetShape)
        {
            if (shapeToMove == null || targetShape == null)
                return;

            // Find the layer containing the target shape
            var targetLayer = context.Layers.FirstOrDefault(l => l.Shapes.Contains(targetShape));
            if (targetLayer == null)
                return;

            // Remove from old layer
            var oldLayer = context.Layers.FirstOrDefault(l => l.Shapes.Contains(shapeToMove));
            oldLayer?.Shapes.Remove(shapeToMove);

            // Insert before the target shape
            int targetIndex = targetLayer.Shapes.IndexOf(targetShape);
            if (targetIndex >= 0)
                targetLayer.Shapes.Insert(targetIndex, shapeToMove);
            else
                targetLayer.Shapes.Add(shapeToMove);
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
