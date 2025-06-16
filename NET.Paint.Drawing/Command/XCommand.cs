using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Service;

namespace NET.Paint.Drawing.Command
{
    public class XCommand
    {
        public XService _service;
        public XCommand(XService service) => _service = service;

        #region Edit Commands

        public void Copy(object elementToCopy)
        {
            if (elementToCopy is XRenderable || elementToCopy is XLayer)
            {
                XClipboard.Instance.Data = elementToCopy;
                XClipboard.Instance.IsCut = false;
            }
        }

        public void Cut(object elementToCut)
        {
            XClipboard.Instance.Data = elementToCut;
            XClipboard.Instance.IsCut = true;

            if (elementToCut is XLayer layer)
                _service.Project.Images.First(x => x.Layers.Contains(layer)).Layers.Remove(layer);

            else if (elementToCut is XRenderable renderable)
                _service.Project.Images.First(x => x.Layers.Any(l => l.Shapes.Contains(renderable))).Layers.First(l => l.Shapes.Contains(renderable)).Shapes.Remove(renderable);
        }

        public void Paste(object? target = null)
        {
            if (XClipboard.Instance.Data != null)
            {
                if (_service.ActiveImage != null)
                {
                    if (XClipboard.Instance.Data is XRenderable renderable && _service.ActiveImage.ActiveLayer != null)
                    {
                        if (target != null && target is XLayer targetLayer)
                            targetLayer.Shapes.Add(XClipboard.Instance.IsCut ? renderable : renderable.Clone() as XRenderable);
                        else
                            _service.ActiveImage.ActiveLayer.Shapes.Add(XClipboard.Instance.IsCut ? renderable : renderable.Clone() as XRenderable);
                    }

                    else if (XClipboard.Instance.Data is XLayer layer && _service.ActiveImage != null)
                        _service.ActiveImage.Layers.Add(XClipboard.Instance.IsCut ? layer : layer.Clone() as XLayer);

                    if (XClipboard.Instance.IsCut)
                        XClipboard.Instance.Data = null;
                }
            }
        }

        public void Undo()
        {
            if (_service.ActiveImage?.ActiveLayer?.Shapes.Any() == true)
            {
                var shape = _service.ActiveImage.ActiveLayer.Shapes.Last();
                _service.ActiveImage.ActiveLayer.Shapes.Remove(shape);
                _service.ActiveImage.Undo.Push(shape);
            }
        }

        public void Redo()
        {
            if (_service.ActiveImage?.Undo?.History.Any() == true && _service.ActiveImage != null && _service.ActiveImage.ActiveLayer != null)
            {
                var shape = _service.ActiveImage.Undo.History.Last();
                _service.ActiveImage.Undo.History.Remove(shape);
                _service.ActiveImage.ActiveLayer.Shapes.Add(shape);
            }
        }

        #endregion

        public void CreateImage(string title, double width = 1920, double height = 1080)
        {
            var image = new XImage
            {
                Title = title,
                Width = width,
                Height = height
            };

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

        public void CreateLayer(string title = null)
        {
            if (_service.ActiveImage != null)
            {
                var layer = new XLayer { Title = title != null ? title : "Layer " + _service.ActiveImage.Layers.Count };
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

        public void RemoveRenderable(XRenderable renderable)
        {
            foreach (var image in _service.Project.Images)
            {
                foreach (var layer in image.Layers)
                {
                    if (layer.Shapes.Contains(renderable))
                    {
                        layer.Shapes.Remove(renderable);
                        return;
                    }
                }
            }
        }
    }
}
