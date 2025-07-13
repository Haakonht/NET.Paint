using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Factory;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Resources.Controls;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Xceed.Wpf.AvalonDock.Controls;
using SelectionMode = NET.Paint.Drawing.Constant.SelectionMode;

namespace NET.Paint.View.Component
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : UserControl
    {
        public XPreview Preview { get; } = new XPreview();

        public Editor()
        {
            InitializeComponent();
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            var image = DataContext as XImage;

            if (image != null && XTools.Instance is XTools tools)
            {
                tools.ClickLocation = e.GetPosition(sender as UIElement);
                tools.ClickLocation = new Point(tools.ClickLocation.Value.X - image.ActiveLayer!.OffsetX, tools.ClickLocation.Value.Y - image.ActiveLayer!.OffsetY);

                if (tools.ActiveTool == ToolType.Pointer || (tools.ActiveTool == ToolType.Selector && tools.SelectionMode == SelectionMode.Single))
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
                }

                if (tools.ActiveTool == ToolType.Text)
                {
                    if (Preview.Shape != null && Preview.Shape is XText text && !string.IsNullOrEmpty(text.Text) && image.ActiveLayer != null)
                    {
                        if (image.ActiveLayer is XVectorLayer vectorLayer)
                            vectorLayer.Shapes.Add(Preview.Shape);
                        
                        Preview.Shape = null;
                    }
                    else
                        Preview.Shape = XFactory.CreateShape(tools);
                }

                if (tools.ActiveTool == ToolType.Selector)
                    Preview.Shape = XFactory.CreateShape(tools);
            }
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            var image = DataContext as XImage;

            if (image != null && XTools.Instance is XTools tools)
            {
                tools.MouseLocation = e.GetPosition(sender as UIElement);
                tools.MouseLocation = new Point(tools.MouseLocation.X - image.ActiveLayer!.OffsetX, tools.MouseLocation.Y - image.ActiveLayer!.OffsetY);

                // Vector tools
                if (image.ActiveLayer != null && image.ActiveLayer is XLayer layer)
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        if (Preview.Shape is XPencil pencil)
                            XFactory.CreatePencilPoints(pencil.Points, pencil.Points.LastOrDefault(), tools.MouseLocation, pencil.Spacing);

                        else if (tools.ActiveTool == ToolType.Pointer && image.Selected == null)
                        {
                            if (tools.ClickLocation != null)
                            {
                                Vector? delta = delta = tools.MouseLocation - tools.ClickLocation;

                                if (delta != null)
                                {
                                    image.ActiveLayer.OffsetX += delta.Value.X;
                                    image.ActiveLayer.OffsetY += delta.Value.Y;
                                }
                            }
                            tools.ClickLocation = tools.MouseLocation;

                        }
                        else
                        {
                            if (tools.ClickLocation != null && tools.MouseLocation != null)
                                Preview.Shape = XFactory.CreateShape(tools);
                        }
                    }
                    else if (e.XButton1 == MouseButtonState.Pressed)
                    {
                        if ((tools.ActiveTool == ToolType.Bezier || tools.ActiveTool == ToolType.Curve) && image.ActiveLayer is IShapeLayer vectorLayer && vectorLayer.Shapes.Last() is IControlPoints cps)
                            cps.Ctrl1 = tools.MouseLocation;

                    }
                    else if (e.XButton2 == MouseButtonState.Pressed)
                    {
                        if (tools.ActiveTool == ToolType.Bezier && image.ActiveLayer is IShapeLayer vectorLayer && vectorLayer.Shapes.Last() is XBezier bezier)
                            bezier.Ctrl2 = tools.MouseLocation;

                    }
                    else if (tools.ActiveTool != ToolType.Text)
                    {
                        if (Preview.Shape != null)
                        {
                            if (tools.ActiveTool == ToolType.Selector && tools.SelectionMode == SelectionMode.Lasso)
                            {
                                LassoSelect(sender, image); 
                            }

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
                            
                            if (tools.ActiveTool != ToolType.Selector)
                            {
                                if (image.ActiveLayer is IShapeLayer vectorLayer)
                                    vectorLayer.Shapes.Add(Preview.Shape);
                                else if (image.ActiveLayer is XRasterLayer rasterLayer)
                                    rasterLayer.Bitmap = XFactory.AddShapeToBitmap(rasterLayer.Bitmap, Preview.Shape, image.Width, image.Height);
                            }

                            tools.ClickLocation = null;
                            Preview.Shape = null;
                        }
                    }
                }
            }
        }

        private void LassoSelect(object sender, XImage image)
        {
            if (Preview.Shape.Points.Count < 3)
                return;

            // Ensure the lasso polygon is closed
            if (Preview.Shape.Points[0] != Preview.Shape.Points[^1])
                Preview.Shape.Points.Add(Preview.Shape.Points[0]);

            var lassoGeometry = new PathGeometry()
            {
                Figures = new PathFigureCollection()
                {
                    new PathFigure {
                        StartPoint = Preview.Shape.Points[0],
                        Segments = new PathSegmentCollection() { new PolyLineSegment(Preview.Shape.Points.Skip(1).ToList(), true) },
                        IsClosed = true
                    }
                }
            };

            if (image.ActiveLayer is IShapeLayer shapeLayer && shapeLayer.Shapes.Count > 0)
            {
                List<object> selectedShapes = new List<object>();
                foreach (var renderable in shapeLayer.Shapes)
                {
                    // Check if any point of the shape is inside the lasso polygon
                    var anyPointInside = renderable.Points.Any(lassoGeometry.FillContains);
                    if (anyPointInside)
                        selectedShapes.Add(renderable);
                }

                if (selectedShapes.Count > 0)
                    image.Selected = selectedShapes;
            }
        }
    }
}
