using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Factory;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Resources.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock.Controls;

namespace NET.Paint.View.Component
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : UserControl
    {
        private Point? _lastAddedPoint = null;
        public XPreview Preview { get; } = new XPreview();

        public Editor()
        {
            InitializeComponent();
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            var image = DataContext as XImage;
            
            if (image != null)
            {
                XTools.Instance.ClickLocation = e.GetPosition(sender as UIElement);
                XTools.Instance.ClickLocation = new Point(XTools.Instance.ClickLocation.Value.X - image.ActiveLayer!.OffsetX, XTools.Instance.ClickLocation.Value.Y - image.ActiveLayer!.OffsetY);

                if (XTools.Instance.ActiveTool == ToolType.Selector)
                {              
                    if (sender is GridCanvas canvas)
                    {
                        var hitResult = VisualTreeHelper.HitTest(canvas, e.GetPosition(sender as UIElement));

                        if (hitResult?.VisualHit is Shape shape)
                            image.Selected = shape.DataContext as XRenderable;
                        else if (hitResult?.VisualHit is TextBlock textBox)
                            image.Selected = textBox.DataContext as XRenderable;
                        else if (hitResult?.VisualHit is Image imageControl)
                            image.Selected = imageControl.DataContext as XRenderable;
                        else
                            image.Selected = null;
                    }

                    if (image.Selected == null)
                        _lastAddedPoint = e.GetPosition(sender as UIElement);
                }

                if (XTools.Instance.ActiveTool == ToolType.Text)
                {

                    if (Preview.Shape != null && Preview.Shape is XText text && !string.IsNullOrEmpty(text.Text) && image.ActiveLayer != null)
                    {
                        if (image.ActiveLayer is XVectorLayer vectorLayer)
                            vectorLayer.Shapes.Add(Preview.Shape);
                        
                        Preview.Shape = null;
                    }
                    else
                        Preview.Shape = XFactory.CreateShape(XTools.Instance);
                }
            }
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            var image = DataContext as XImage;

            if (image != null)
            {
                XTools.Instance.MouseLocation = e.GetPosition(sender as UIElement);
                XTools.Instance.MouseLocation = new Point(XTools.Instance.MouseLocation.X - image.ActiveLayer!.OffsetX, XTools.Instance.MouseLocation.Y - image.ActiveLayer!.OffsetY);

                // Vector tools
                if (image.ActiveLayer != null && image.ActiveLayer is XLayer layer)
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {

                        if (XTools.Instance.ActiveTool == ToolType.Pencil && Preview.Shape is XPencil pencil)
                        {
                            _lastAddedPoint = XFactory.CreatePencilPoints(pencil.Points, _lastAddedPoint, XTools.Instance.MouseLocation, pencil.Spacing);
                        }
                        else if (XTools.Instance.ActiveTool == ToolType.Selector && image.Selected == null)
                        {
                            if (_lastAddedPoint != null)
                            {
                                Vector? delta = null;
                                if (_lastAddedPoint != null)
                                    delta = e.GetPosition(sender as UIElement) - _lastAddedPoint.Value;

                                if (delta != null)
                                {
                                    image.ActiveLayer.OffsetX += delta.Value.X;
                                    image.ActiveLayer.OffsetY += delta.Value.Y;
                                }
                            }
                            _lastAddedPoint = e.GetPosition(sender as UIElement);

                        }
                        else
                        {
                            if (XTools.Instance.ClickLocation != null && XTools.Instance.MouseLocation != null)
                                Preview.Shape = XFactory.CreateShape(XTools.Instance);
                        }
                    }
                    else if (e.XButton1 == MouseButtonState.Pressed)
                    {
                        if ((XTools.Instance.ActiveTool == ToolType.Bezier || XTools.Instance.ActiveTool == ToolType.Curve) && image.ActiveLayer is IShapeLayer vectorLayer && vectorLayer.Shapes.Last() is IControlPoints cps)
                            cps.Ctrl1 = XTools.Instance.MouseLocation;

                    }
                    else if (e.XButton2 == MouseButtonState.Pressed)
                    {
                        if (XTools.Instance.ActiveTool == ToolType.Bezier && image.ActiveLayer is IShapeLayer vectorLayer && vectorLayer.Shapes.Last() is XBezier bezier)
                            bezier.Ctrl2 = XTools.Instance.MouseLocation;

                    }
                    else if (XTools.Instance.ActiveTool != ToolType.Text)
                    {
                        if (Preview.Shape != null)
                        {
                            if (image.ActiveLayer is XHybridLayer hybridLayer)
                            {
                                if (hybridLayer.Shapes.Count > hybridLayer.History - 1)
                                {
                                    var shape = hybridLayer.Shapes.FirstOrDefault();
                                    if (shape != null)
                                    {
                                        hybridLayer.Bitmap = XFactory.AddShapeToBitmap(hybridLayer.Bitmap, shape, image.Width, image.Height);
                                        hybridLayer.Shapes.Remove(shape);
                                    }
                                }
                            }
                            
                            if (image.ActiveLayer is IShapeLayer vectorLayer)
                                vectorLayer.Shapes.Add(Preview.Shape);
                            else if (image.ActiveLayer is XRasterLayer rasterLayer)
                                rasterLayer.Bitmap = XFactory.AddShapeToBitmap(rasterLayer.Bitmap, Preview.Shape, image.Width, image.Height);

                            XTools.Instance.ClickLocation = null;
                            Preview.Shape = null;
                            _lastAddedPoint = null;
                        }
                    }
                }
            }
        }
    }
}
