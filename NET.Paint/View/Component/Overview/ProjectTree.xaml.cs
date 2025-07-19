using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Service;
using NET.Paint.Resources.Extensions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using XSelectionMode = NET.Paint.Drawing.Constant.XSelectionMode;

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
            _filterTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.75)
            };
            _filterTimer.Tick += FilterTimer_Tick;
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

                        XTools.Instance.ActiveTool = XToolType.Selector;
                        XTools.Instance.SelectionMode = XSelectionMode.Pointer;
                    }

                    service.ActiveImage.Selected.Clear();
                    service.ActiveImage.Selected.Add(element.DataContext);
                }
            }
        }

        private void Unselect(object sender, MouseButtonEventArgs e)
        {
            if (DataContext != null && DataContext is XService service)
            {
                if (service.ActiveImage != null && service.ActiveImage is XImage activeImage)
                    service.ActiveImage.Selected.Clear();
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
                {
                    activeImage.Selected.Clear();
                    activeImage.Selected.Add(service.ActiveImage);
                }
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
                    XLayer layer = new XRasterLayer() { Title = "", IsEditing = true };
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
                service.Command.Operations.Cut([item.DataContext]);
        }

        private void Copy(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is XService service && sender is MenuItem item)
                service.Command.Operations.Copy([item.DataContext]);
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

        #region Filter Management

        private DispatcherTimer _filterTimer;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _filterTimer.Stop();
            _filterTimer.Start();
        }

        private void FilterTimer_Tick(object sender, EventArgs e)
        {
            _filterTimer.Stop();
            ImageTree.Filter(FilterPredicate);
        }

        private bool FilterPredicate(object node)
        {
            if (FilterEnabled.IsChecked == false)
                return true;

            if (string.IsNullOrEmpty(FilterText.Text))
                return true;

            if (node is XImage image)
                return ImageFiltered(image);
            else if (node is XLayer layer)
                return LayerFiltered(layer);
            else if (node is XRenderable renderable)
                return RenderableFiltered(renderable);

            return true;
        }

        private void FilterEnabled_Checked(object sender, RoutedEventArgs e) => ImageTree.Filter(FilterPredicate);
        private bool ImageFiltered(XImage image) => image.Title.ToLower().Contains(FilterText.Text.ToLower()) || image.Layers.Any(LayerFiltered);
        private bool LayerFiltered(XLayer layer) => (layer is IShapeLayer shapeLayer) ? layer.Title.ToLower().Contains(FilterText.Text.ToLower()) || shapeLayer.Shapes.Any(RenderableFiltered) : layer.Title.ToLower().Contains(FilterText.Text.ToLower());
        private bool RenderableFiltered(XRenderable renderable)
        {
            if (renderable.Id.ToString().ToLower().Contains(FilterText.Text.ToLower()))
                return true;

            if (renderable is XText text)
                return text.Text.ToLower().Contains(FilterText.Text.ToLower()) || renderable.Type.ToString().ToLower().Contains(FilterText.Text.ToLower());

            if (renderable is XCircle circle)
                return circle.Style.ToString().ToLower().Contains(FilterText.Text.ToLower());

            if (renderable is XSquare square)
                return square.Style.ToString().ToLower().Contains(FilterText.Text.ToLower());

            if (renderable is XPolygon polygon)
                return polygon.Style.ToString().ToLower().Contains(FilterText.Text.ToLower());

            return renderable.Type.ToString().ToLower().Contains(FilterText.Text.ToLower());
        }

        private void FilterText_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(FilterText.Text))
                FilterPlaceholder.Visibility = Visibility.Visible;
            else
                FilterPlaceholder.Visibility = Visibility.Collapsed;
        }

        private void FilterText_GotFocus(object sender, RoutedEventArgs e) => FilterPlaceholder.Visibility = Visibility.Collapsed;

        #endregion

    }
}
