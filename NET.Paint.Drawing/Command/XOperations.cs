using Microsoft.Win32;
using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Factory;
using NET.Paint.Drawing.Helper;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Service;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NET.Paint.Drawing.Command
{
    public class XOperations
    {
        public XService _service;
        public XOperations(XService service) => _service = service;

        #region Edit Operations

        public void Copy(object elementToCopy = null)
        {
            XClipboard.Instance.IsCut = false;
            XClipboard.Instance.Data.Clear();

            if (elementToCopy != null)
                if (elementToCopy is XRenderable || elementToCopy is XLayer || elementToCopy is XImage)
                    XClipboard.Instance.Data.Add(elementToCopy as XObject);
            else
                foreach (var element in _service.ActiveImage.Selected)
                    if (element is XRenderable || element is XLayer)
                        XClipboard.Instance.Data.Add(element as XObject);

            CreateNotification(XNotificationSource.Clipboard);
        }

        public void Cut(object elementToCut = null)
        {
            XClipboard.Instance.IsCut = true;
            XClipboard.Instance.Data.Clear();

            if (elementToCut != null)
            { 
                XClipboard.Instance.Data.Add(elementToCut as XObject);

                if (elementToCut is XLayer layer)
                    _service.Project.Images.First(x => x.Layers.Contains(layer)).Layers.Remove(layer);

                else if (elementToCut is XRenderable renderable)
                    (_service.Project.Images.First(x => x.Layers.Any(l => l is IShapeLayer shapeLayer && shapeLayer.Shapes.Contains(renderable))).Layers.First(l => l is IShapeLayer shapeLayer && shapeLayer.Shapes.Contains(renderable)) as IShapeLayer).Shapes.Remove(renderable);

            }
            else
            {
                var selectedElements = _service.ActiveImage.Selected;
                List<int> indicesToRemove = new List<int>();

                foreach (var element in selectedElements)
                {
                    if (element is XRenderable || element is XLayer)
                    {
                        XClipboard.Instance.Data.Add(element as XObject);

                        if (element is XLayer layer)
                            _service.Project.Images.First(x => x.Layers.Contains(layer)).Layers.Remove(layer);

                        else if (element is XRenderable renderable)
                            (_service.Project.Images.First(x => x.Layers.Any(l => l is IShapeLayer shapeLayer && shapeLayer.Shapes.Contains(renderable))).Layers.First(l => l is IShapeLayer shapeLayer && shapeLayer.Shapes.Contains(renderable)) as IShapeLayer).Shapes.Remove(renderable);

                        indicesToRemove.Add(selectedElements.IndexOf(element));
                    }
                }

                for (int i = indicesToRemove.Count - 1; i >= 0; i--)
                    selectedElements.RemoveAt(indicesToRemove[i]);
            }

            CreateNotification(XNotificationSource.Clipboard);
        }

        public void Paste(object? target = null)
        {
            if (XClipboard.Instance.Data.Count > 0)
            {
                if (_service.ActiveImage != null)
                {
                    _service.ActiveImage.Selected.Clear();
                    foreach (object item in XClipboard.Instance.Data)
                    {
                        if (item is XLayer layer)
                        {
                            XLayer pastedLayer = layer.Clone() as XLayer;
                            pastedLayer.Title = $"Copy of {pastedLayer.Title}";
                            if (item is XVectorLayer vectorLayer)
                                _service.ActiveImage.Layers.Add(pastedLayer as XVectorLayer);
                            else if (item is XHybridLayer hybridLayer)
                                _service.ActiveImage.Layers.Add(pastedLayer as XHybridLayer);
                            else if (item is XRasterLayer rasterLayer)
                                _service.ActiveImage.Layers.Add(pastedLayer as XRasterLayer);
                        } 
                        else if (item is XRenderable renderable)
                        {
                            XRenderable pastedRenderable = renderable.Clone() as XRenderable;
                            if (target != null && target is IShapeLayer targetLayer)
                                targetLayer.Shapes.Add(pastedRenderable);
                            else if (_service.ActiveImage.ActiveLayer is IShapeLayer activeLayer)
                                activeLayer.Shapes.Add(pastedRenderable);

                            _service.ActiveImage.Selected.Add(pastedRenderable);
                        }
                    }

                    if (XClipboard.Instance.IsCut)
                    {
                        XClipboard.Instance.Data.Clear();
                        XClipboard.Instance.IsCut = false;
                        _service.Preferences.ClipboardVisible = false;
                    }
                    CreateNotification(XNotificationSource.Clipboard);
                }
            }
        }

        public void ClearClipboard()
        {
            XClipboard.Instance.Data.Clear();
            XClipboard.Instance.IsCut = false;
        }

        public void Undo()
        {
            if (_service.ActiveImage is XImage activeImage)
            {
                if (activeImage.ActiveLayer is IShapeLayer shapeLayer)
                {
                    var shape = shapeLayer.Shapes.Last();
                    shapeLayer.Shapes.Remove(shape);
                    activeImage.Undo.Push(shape);

                    if (activeImage.Selected.Contains(shape))
                        activeImage.Selected.Remove(shape);

                    CreateNotification(XNotificationSource.History);
                }
            }
        }

        public void Redo()
        {
            if (_service.ActiveImage is XImage activeImage && activeImage.Undo.History.Any())
            {
                if (activeImage.ActiveLayer is IShapeLayer shapeLayer)
                {
                    var shape = activeImage.Undo.History.Last();
                    activeImage.Undo.History.Remove(shape);
                    shapeLayer.Shapes.Add(shape);

                    CreateNotification(XNotificationSource.History);
                }
            }
        }

        private void CreateNotification(XNotificationSource source)
        {
            if (source == XNotificationSource.Clipboard)
            {
                var existingNotification = _service.Notifications.FirstOrDefault(n => n.Source == XNotificationSource.Clipboard);
                if (existingNotification != null)
                    _service.Notifications.Remove(existingNotification);
                
                _service.Notifications.Add(new XNotification
                {
                    Source = XNotificationSource.Clipboard,
                    Message = _service.Clipboard.Data.Count > 0 ? "Items added to clipboard." : "Clipboard emptied"
                });
                              }

            if (source == XNotificationSource.History)
            {
                var existingNotification = _service.Notifications.FirstOrDefault(n => n.Source == XNotificationSource.History);
                if (existingNotification != null)
                    _service.Notifications.Remove(existingNotification);

                _service.Notifications.Add(new XNotification
                {
                    Source = XNotificationSource.History,
                    Message = _service.ActiveImage.Undo.History.Count > 0 ? "Items added to undo history." : "History emptied"
                });
            }
        }

        #endregion

        #region Image Operations

        public void CreateImage(XImage image)
        {
            _service.Project.Images.Add(image);
            _service.ActiveImage = image;
        }

        public void RemoveImage(XImage image)
        {
            foreach (var img in _service.Project.Images)
            {
                if (img == image)
                {
                    _service.Project.Images.Remove(img);
                    if (_service.Project.Images.Count > 0)
                        _service.ActiveImage = _service.Project.Images.First();
                    else
                        _service.ActiveImage = null;
                    return;
                }
            }
        }

        public void ExportImage(Canvas canvas, string filePath, string format)
        {
            // Step 1: Measure and arrange the canvas
            var size = new Size(canvas.ActualWidth, canvas.ActualHeight);
            canvas.Measure(size);
            canvas.Arrange(new Rect(size));

            // Step 2: Create a visual to include the background
            var drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
            {
                if (canvas.Background is SolidColorBrush solidColorBrush)
                {
                    drawingContext.DrawRectangle(solidColorBrush, null, new Rect(0, 0, canvas.ActualWidth, canvas.ActualHeight));
                }
                else if (canvas.Background is ImageBrush imageBrush)
                {
                    drawingContext.DrawRectangle(imageBrush, null, new Rect(0, 0, canvas.ActualWidth, canvas.ActualHeight));
                }

                drawingContext.DrawRectangle(new VisualBrush(canvas), null, new Rect(0, 0, canvas.ActualWidth, canvas.ActualHeight));
            }

            // Step 3: Render the visual to a bitmap
            var renderBitmap = new RenderTargetBitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            renderBitmap.Render(drawingVisual);

            // Step 4: Encode the bitmap to the desired format
            BitmapEncoder encoder;
            switch (format.ToLower())
            {
                case "png":
                    encoder = new PngBitmapEncoder();
                    break;
                case "jpeg":
                case "jpg":
                    encoder = new JpegBitmapEncoder();
                    break;
                default:
                    throw new ArgumentException("Unsupported format: " + format);
            }

            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            // Step 5: Save the image to a file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        public void ExportImage(XImage image)
        {
            // 1. Show SaveFileDialog
            var dialog = new SaveFileDialog
            {
                Filter = "PNG Image (*.png)|*.png|JPEG Image (*.jpg;*.jpeg)|*.jpg;*.jpeg",
                FileName = image.Title,
                DefaultExt = ".png"
            };
            if (dialog.ShowDialog() != true)
                return;

            string filePath = dialog.FileName;
            string format = Path.GetExtension(filePath).TrimStart('.').ToLower();

            // 2. Create a Canvas and set its size/background
            var canvas = new Canvas
            {
                Width = image.Width,
                Height = image.Height,
                Background = new SolidColorBrush(image.Background)
            };

            var resourceDictionary = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/Resources/Renderer.xaml", UriKind.Absolute)
            };
            canvas.Resources.MergedDictionaries.Add(resourceDictionary);

            // 3. Add each visible layer as a ContentPresenter
            foreach (var layer in image.Layers)
            {
                if (layer.IsVisible)
                {
                    var layerCanvas = new Canvas();
                    Canvas.SetLeft(layerCanvas, layer.OffsetX);
                    Canvas.SetTop(layerCanvas, layer.OffsetY);

                    if (layer is IShapeLayer shapeLayer)
                    {
                        foreach (var shape in shapeLayer.Shapes)
                        {
                            var shapePresenter = new ContentPresenter
                            {
                                Content = shape
                            };
                            layerCanvas.Children.Add(shapePresenter);
                        }
                    }

                    if (layer is IBitmapLayer bitmapLayer)
                    {
                       layerCanvas.Background = new ImageBrush(XHelper.Base64ToImageSource(bitmapLayer.Bitmap))
                       {
                           Stretch = Stretch.None
                       };
                    }
                    canvas.Children.Add(layerCanvas);
                }
            }

            // 4. Measure and arrange the canvas
            var size = new Size(image.Width, image.Height);
            canvas.Measure(size);
            canvas.Arrange(new Rect(size));
            canvas.UpdateLayout();

            // 5. Render the canvas to a bitmap
            var renderBitmap = new RenderTargetBitmap(
                (int)image.Width, (int)image.Height, 96, 96, PixelFormats.Pbgra32);
            renderBitmap.Render(canvas);

            // 6. Encode the bitmap to the desired format
            BitmapEncoder encoder;
            switch (format)
            {
                case "png":
                    encoder = new PngBitmapEncoder();
                    break;
                case "jpeg":
                case "jpg":
                    encoder = new JpegBitmapEncoder();
                    break;
                default:
                    encoder = new PngBitmapEncoder();
                    break;
            }

            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            // 7. Save the image to a file
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        #endregion

        #region Tree Operations

        public void MoveImage(XProject project, XImage imageToMove, XImage targetImage)
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

        public void MoveLayerToImage(XProject project, XLayer layerToMove, XImage targetImage)
        {
            if (layerToMove == null || targetImage == null)
                return;

            // Remove from old image
            var oldImage = project.Images.FirstOrDefault(img => img.Layers.Contains(layerToMove));
            oldImage?.Layers.Remove(layerToMove);

            // Add to new image (at end)
            targetImage.Layers.Add(layerToMove);
        }

        public void MoveLayer(XImage context, XLayer layerToMove, XLayer targetLayer)
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

        public void MoveShapeToLayer(XImage context, XRenderable shapeToMove, XLayer targetLayer)
        {
            if (shapeToMove == null || targetLayer == null)
                return;

            // Remove from old layer
            if (targetLayer is IShapeLayer targetShapeLayer)
            {
                var oldLayer = context.Layers.FirstOrDefault(l => l is IShapeLayer shapeLayer && shapeLayer.Shapes.Contains(shapeToMove)) as IShapeLayer;
                oldLayer?.Shapes.Remove(shapeToMove);

                // Add to new layer (at end)
                targetShapeLayer.Shapes.Add(shapeToMove);
            }
        }

        public void MoveShapeInFrontOfShape(XImage context, XRenderable shapeToMove, XRenderable targetShape)
        {
            if (shapeToMove == null || targetShape == null)
                return;

            // Find the layer containing the target shape
            var targetLayer = context.Layers.FirstOrDefault(l => l is IShapeLayer shapeLayer && shapeLayer.Shapes.Contains(targetShape)) as IShapeLayer;
            if (targetLayer == null)
                return;

            // Remove from old layer
            var oldLayer = context.Layers.FirstOrDefault(l => l is IShapeLayer shapeLayer && shapeLayer.Shapes.Contains(shapeToMove)) as IShapeLayer;
            oldLayer?.Shapes.Remove(shapeToMove);

            // Insert before the target shape
            int targetIndex = targetLayer.Shapes.IndexOf(targetShape);
            if (targetIndex >= 0)
                targetLayer.Shapes.Insert(targetIndex, shapeToMove);
            else
                targetLayer.Shapes.Add(shapeToMove);
        }

        #endregion

        #region Layer Operations

        public void CreateLayer(XLayer layer)
        {
            if (_service.ActiveImage != null)
            {
                _service.ActiveImage.Layers.Add(layer);
                _service.ActiveImage.ActiveLayer = layer;
            }
        }

        public void RemoveLayer(XLayer layer)
        {
            foreach (var image in _service.Project.Images)
            {
                if (image.Layers.Contains(layer))
                {
                    image.Layers.Remove(layer);
                    if (image.Layers.Count > 0)
                        image.ActiveLayer = image.Layers.First();
                    else
                        image.ActiveLayer = null;
                    return;
                }
            }
        }

        public void FlattenLayer(XImage image, XLayer layer)
        {
            if (layer is XVectorLayer vectorLayer)
            {
                XRasterLayer flattenedLayer = new XRasterLayer
                {
                    Title = vectorLayer.Title,
                    OffsetX = vectorLayer.OffsetX,
                    OffsetY = vectorLayer.OffsetY,
                    Bitmap = XHelper.ImageSourceToBase64(XFactory.FlattenLayerToBitmap(vectorLayer.Shapes, (int)image.Width, (int)image.Height))
                };

                int index = image.Layers.IndexOf(vectorLayer);
                image.Layers.Insert(index, flattenedLayer);
                image.Layers.Remove(vectorLayer);
            }

        }

        #endregion

        #region Renderable Operations

        public void RemoveRenderable(XRenderable renderable)
        {
            foreach (var image in _service.Project.Images)
            {
                foreach (XVectorLayer layer in image.Layers.Where(x => x.Type == Constant.XLayerType.Vector))
                {
                    if (layer.Shapes.Contains(renderable))
                    {
                        layer.Shapes.Remove(renderable);
                        return;
                    }
                }
            }
        }

        #endregion

        #region Project Operations

        public void CreateProject()
        {
            _service.Project = new XProject
            {
                Title = "New Project",
                Images = new ObservableCollection<XImage>()
            };
            _service.ActiveImage = null;
        }

        public void OpenProject()
        {

        }

        public void SaveProject()
        {

        }

        #endregion

    }
}
