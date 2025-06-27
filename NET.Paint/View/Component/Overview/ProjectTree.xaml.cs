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

                if (e.NewValue is XVectorLayer layer)
                {
                    var containingImg = context.Project.Images.FirstOrDefault(img => img.Layers.Contains(layer));
                    if (containingImg != null && containingImg != context.ActiveImage)
                        context.ActiveImage = containingImg;

                    context.ActiveImage.ActiveLayer = layer;
                }

                if (e.NewValue is XRenderable renderable)
                {
                    var containingImage = context.Project.Images.FirstOrDefault(img => img.Layers.Any(l => l is XVectorLayer vectorLayer && vectorLayer.Shapes.Contains(renderable)));
                    if (containingImage != null && containingImage != context.ActiveImage)
                        context.ActiveImage = containingImage;
                    
                    var containingLayer = context.ActiveImage.Layers.FirstOrDefault(l => l is XVectorLayer vectorLayer && vectorLayer.Shapes.Contains(renderable)) as XVectorLayer;
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

            if (context != null && context.ActiveImage != null)
            {
                var dialogModel = new XLayerDialog();
                var layerDialog = new LayerDialog(dialogModel);
                var result = layerDialog.ShowDialog();
                if (result == true && layerDialog.Result != null)
                {
                    // Use layerDialog.Result (the XLayerDialog object)
                    context.Command.CreateLayer(layerDialog.Result.Title);
                }
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
                    // Use imageDialog.Result (the XImageDialog object)
                    context.Command.CreateImage(new XImage
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
            var context = DataContext as XService;
            var item = sender as MenuItem;

            if (context != null)
            {
                if (item.DataContext is XImage image)
                    context.Command.RemoveImage(image);

                if (item.DataContext is XVectorLayer layer)
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
            var draggedLayer = _draggedTreeViewItem?.DataContext as XVectorLayer;
            var targetItem = GetNearestContainer(e.OriginalSource as UIElement);

            if (targetItem == null) return;
            var targetData = targetItem.DataContext;

            // 1. Reorder images
            if (draggedImage != null && targetData is XImage targetImage && !ReferenceEquals(draggedImage, targetImage))
            {
                context.Command.MoveImage(context.Project, draggedImage, targetImage);
            }
            // 2. Move layer into another image
            else if (draggedLayer != null && targetData is XImage targetImageForLayer)
            {
                context.Command.MoveLayerToImage(context.Project, draggedLayer, targetImageForLayer);
            }
            // 3. Existing logic for layer reordering and shape moving
            else if (draggedLayer != null && targetData is XVectorLayer targetLayer && !ReferenceEquals(draggedLayer, targetLayer))
            {
                context.Command.MoveLayer(context.ActiveImage, draggedLayer, targetLayer);
            }
            else if (_draggedTreeViewItem?.DataContext is XRenderable droppedShape)
            {
                if (targetData is XVectorLayer targetLayerForShape)
                {
                    context.Command.MoveShapeToLayer(context.ActiveImage, droppedShape, targetLayerForShape);
                }
                else if (targetData is XRenderable targetShape)
                {
                    context.Command.MoveShapeInFrontOfShape(context.ActiveImage, droppedShape, targetShape);
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
