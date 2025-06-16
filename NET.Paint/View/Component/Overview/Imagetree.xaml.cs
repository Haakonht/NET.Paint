using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Dialog;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Service;
using NET.Paint.View.Component.Dialog;
using NET.Paint.View.Component.Tools.Subcomponent;
using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

                if (e.NewValue is XLayer)
                    context.ActiveImage.ActiveLayer = e.NewValue as XLayer;
                
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

            var droppedLayer = _draggedTreeViewItem?.DataContext as XLayer;
            var droppedShape = _draggedTreeViewItem?.DataContext as XRenderable;
            var targetItem = GetNearestContainer(e.OriginalSource as UIElement);

            if (targetItem == null) return;
            var targetData = targetItem.DataContext;

            // Layer reordering
            if (droppedLayer != null && targetData is XLayer targetLayer && !ReferenceEquals(droppedLayer, targetLayer))
            {
                MoveLayer(context.ActiveImage, droppedLayer, targetLayer);
            }
            // Shape moving/reordering
            else if (droppedShape != null)
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
