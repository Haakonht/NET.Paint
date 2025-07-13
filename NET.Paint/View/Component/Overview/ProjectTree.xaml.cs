using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using SelectionMode = NET.Paint.Drawing.Constant.SelectionMode;

namespace NET.Paint.View.Component.Overview
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

        #region Item Selection

        private void SelectNode(object sender, MouseButtonEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
            {
                if (sender is FrameworkElement element)
                {
                    if (element.DataContext is XImage image)
                        service.ActiveImage = image;

                    if (element.DataContext is XLayer layer)
                    {
                        var containingImg = service.Project.Images.FirstOrDefault(img => img.Layers.Contains(layer));
                        if (containingImg != null && containingImg != service.ActiveImage)
                            service.ActiveImage = containingImg;

                        service.ActiveImage.ActiveLayer = layer;
                    }

                    if (element.DataContext is XRenderable renderable)
                    {
                        var containingImage = service.Project.Images.FirstOrDefault(img => img.Layers.Any(l => l is XVectorLayer vectorLayer && vectorLayer.Shapes.Contains(renderable)));
                        if (containingImage != null && containingImage != service.ActiveImage)
                            service.ActiveImage = containingImage;

                        var containingLayer = service.ActiveImage.Layers.FirstOrDefault(l => l is XVectorLayer vectorLayer && vectorLayer.Shapes.Contains(renderable)) as XVectorLayer;
                        if (containingLayer != null && containingLayer != service.ActiveImage.ActiveLayer)
                            service.ActiveImage.ActiveLayer = containingLayer;

                        XTools.Instance.ActiveTool = ToolType.Selector;
                        XTools.Instance.SelectionMode = SelectionMode.Single;
                    }

                    service.ActiveImage.Selected = element.DataContext;
                }
            }
        }

        private void Unselect(object sender, MouseButtonEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
            {
                if (service.ActiveImage != null && service.ActiveImage is XImage activeImage)
                    activeImage.Selected = null;
            }
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
                    _draggedTreeViewItem = null;
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
            if (DataContext != null && DataContext is XService service)
            {
                var draggedImage = _draggedTreeViewItem?.DataContext as XImage;
                var draggedLayer = _draggedTreeViewItem?.DataContext as XLayer;
                var targetItem = GetNearestContainer(e.OriginalSource as UIElement);

                if (targetItem == null) return;
                var targetData = targetItem.DataContext;

                if (draggedImage != null && targetData is XImage targetImage && !ReferenceEquals(draggedImage, targetImage))
                {
                    service.Command.Operations.MoveImage(service.Project, draggedImage, targetImage);
                }
                else if (draggedLayer != null && targetData is XImage targetImageForLayer)
                {
                    service.Command.Operations.MoveLayerToImage(service.Project, draggedLayer, targetImageForLayer);
                }
                else if (draggedLayer != null && targetData is XLayer targetLayer && !ReferenceEquals(draggedLayer, targetLayer))
                {
                    service.Command.Operations.MoveLayer(service.ActiveImage, draggedLayer, targetLayer);
                }
                else if (_draggedTreeViewItem?.DataContext is XRenderable droppedShape)
                {
                    if (targetData is IShapeLayer targetLayerForShape)
                    {
                        service.Command.Operations.MoveShapeToLayer(service.ActiveImage, droppedShape, targetLayerForShape as XLayer);
                    }
                    else if (targetData is XRenderable targetShape)
                    {
                        service.Command.Operations.MoveShapeInFrontOfShape(service.ActiveImage, droppedShape, targetShape);
                    }
                }
            }
        }

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

        private TreeViewItem GetNearestContainer(UIElement element) => FindAncestor<TreeViewItem>(element);

        #endregion

        #region Image Management

        private void AddImage(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
            {
                XImage image = new XImage() { Title = "", IsEditing = true };
                service.Project.Images.Add(image);
            }
        }

        private void SelectImage(object sender, MouseButtonEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
            {
                if (service.ActiveImage != null && service.ActiveImage is XImage activeImage)
                    activeImage.Selected = service.ActiveImage;
            }
        }

        #endregion

        #region Layer Management

        private void AddVectorLayer(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
            {
                if (service.ActiveImage != null && service.ActiveImage is XImage image)
                {
                    XLayer layer = new XVectorLayer() { Title = "", IsEditing = true };
                    service.ActiveImage.Layers.Add(layer);
                }
            }
        }

        private void AddHybridLayer(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
            {
                if (service.ActiveImage != null && service.ActiveImage is XImage image)
                {
                    XLayer layer = new XHybridLayer() { Title = "", IsEditing = true };
                    service.ActiveImage.Layers.Add(layer);
                }
            }
        }

        private void AddRasterLayer(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
            {
                if (service.ActiveImage != null && service.ActiveImage is XImage image)
                {
                    XLayer layer = new XVectorLayer() { Title = "", IsEditing = true };
                    service.ActiveImage.Layers.Add(layer);
                }
            }
        }

        #endregion

        #region Item Management

        private void Remove(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service && sender is MenuItem item)
            {
                if (item.DataContext is XImage image)
                    service.Command.Operations.RemoveImage(image);

                if (item.DataContext is XLayer layer)
                    service.Command.Operations.RemoveLayer(layer);

                if (item.DataContext is XRenderable renderable)
                    service.Command.Operations.RemoveRenderable(renderable);
            }
        }

        #endregion

        #region Edit Management

        private void Cut(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service && sender is MenuItem item)
                service.Command.Operations.Cut(item.DataContext);
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service && sender is MenuItem item)
                service.Command.Operations.Copy(item.DataContext);
        }

        private void Paste(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service && sender is MenuItem item)
                service.Command.Operations.Paste(item.DataContext);
        }

        private void Flatten(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
            {
                if (sender is MenuItem item && item.DataContext is XVectorLayer layer)
                {
                    var containingImage = service.Project.Images.First(x => x.Layers.Contains(layer));
                    if (containingImage != null)
                        service.Command.Operations.FlattenLayer(containingImage, layer);
                }
            }
        }

        #endregion

        private void OnAddComplete(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                if (tb.DataContext is XLayer layer)
                    layer.IsEditing = false;

                if (tb.DataContext is XImage image)
                    image.IsEditing = false;
            }
        }

        private void OnAddStarted(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox tb)
            {
                tb.Focusable = true;
                tb.Focus();
            }
        }

        private void OnAdd(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Enter || e.Key == Key.Escape) && sender is TextBox tb)
            {
                if (tb.DataContext is XLayer layer)
                    layer.IsEditing = false;

                if (tb.DataContext is XImage image)
                    image.IsEditing = false;

                e.Handled = true;
            }
        }
    }
}
