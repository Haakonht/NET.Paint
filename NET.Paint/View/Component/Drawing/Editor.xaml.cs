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
                    SingleSelect(sender, tools, image);

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

        private void MouseUp(object sender, MouseButtonEventArgs e) => MouseMove(sender, e);
        private void MouseMove(object sender, MouseEventArgs e)
        {
            var image = DataContext as XImage;

            if (image != null && XTools.Instance is XTools tools)
            {
                Point mousePosition = e.GetPosition(sender as UIElement);
                tools.MouseLocation = new Point(mousePosition.X - image.ActiveLayer!.OffsetX, mousePosition.Y - image.ActiveLayer!.OffsetY);

                // Vector tools
                if (image.ActiveLayer != null && image.ActiveLayer is XLayer layer)
                {
                    if (e.LeftButton == MouseButtonState.Pressed)
                    {
                        if (Preview.Shape is XPencil pencil)
                        {
                            if (tools.IsPolylineAdd || tools.ActiveTool == ToolType.Selector)
                                XFactory.CreatePencilPoints(pencil.Points, pencil.Points.LastOrDefault(), tools.MouseLocation, pencil.PointSpacing);

                            else if (tools.ActiveTool == ToolType.Pencil && !tools.IsPolylineAdd)
                                XFactory.RemovePencilPoints(image.ActiveLayer, tools.MouseLocation, tools.EraserTolerance);
                        }

                        else if (tools.ActiveTool == ToolType.Pointer)
                        {
                            if (image.Selected.Count == 0)
                                MoveLayer(tools, image);
                            else
                                MoveSelected(tools, image);
                        }

                        else
                            if (tools.ClickLocation != null)
                                Preview.Shape = XFactory.CreateShape(tools);
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
                            if (tools.ActiveTool == ToolType.Selector)
                            {
                                if (tools.SelectionMode == SelectionMode.Lasso)
                                    LassoSelect(image);
                                else if (tools.SelectionMode == SelectionMode.Rectangle)
                                    RectangleSelect(image);

                                if (image.Selected.Count > 0)
                                    tools.ActiveTool = ToolType.Pointer;
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

                            if (tools.ActiveTool != ToolType.Selector && tools.ActiveTool != ToolType.Pointer)
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

        private void MoveLayer(XTools tools, XImage image)
        {
            if (tools.ClickLocation == null) return;
            Vector? delta = delta = tools.MouseLocation - tools.ClickLocation;

            if (delta != null)
            {
                image.ActiveLayer.OffsetX += delta.Value.X;
                image.ActiveLayer.OffsetY += delta.Value.Y;
            }
        }

        private void MoveSelected(XTools tools, XImage image)
        {
            if (tools.ClickLocation == null) return;
            Vector delta = tools.MouseLocation - tools.ClickLocation.Value;
            
            foreach (XRenderable selected in image.Selected.OfType<XRenderable>())
            {
                for (int i = 0; i < selected.Points.Count; i++)
                {
                    var currentPoint = selected.Points[i];
                    selected.Points[i] = new Point(
                        currentPoint.X + delta.X, 
                        currentPoint.Y + delta.Y
                    );
                }
            }
            tools.ClickLocation = tools.MouseLocation;
        }

        private void SingleSelect(object sender, XTools tools, XImage image)
        {
            if (sender is GridCanvas canvas)
            {
                if (tools.ActiveTool == ToolType.Selector)
                {
                    image.Selected.Clear();
                    var hitResult = VisualTreeHelper.HitTest(canvas, tools.ClickLocation.Value);

                    if (hitResult?.VisualHit is Shape shape)
                        image.Selected.Add(shape.DataContext as XRenderable);
                    else if (hitResult?.VisualHit is TextBlock textBox)
                        image.Selected.Add(textBox.DataContext as XRenderable);
                    else if (hitResult?.VisualHit is Image imageControl)
                        image.Selected.Add(imageControl.DataContext as XRenderable);
                }
                else if (tools.ActiveTool == ToolType.Pointer)
                {
                    var hitResult = VisualTreeHelper.HitTest(canvas, tools.ClickLocation.Value);
                    if (hitResult?.VisualHit is GridCanvas)
                        image.Selected.Clear();
                }
            }
        }

        private void RectangleSelect(XImage image)
        {
            if (Preview.Shape.Points.Count != 2)
                return;

            Rect rectangle = new Rect(Preview.Shape.Points[0], Preview.Shape.Points[1]);

            if (image.ActiveLayer is IShapeLayer shapeLayer && shapeLayer.Shapes.Count > 0)
            {
                image.Selected.Clear();
                foreach (var renderable in shapeLayer.Shapes)
                    if (renderable.Points.Any(rectangle.Contains))
                        image.Selected.Add(renderable);
            }
        }

        private void LassoSelect(XImage image)
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
                image.Selected.Clear();
                foreach (var renderable in shapeLayer.Shapes)
                    if (renderable.Points.Any(lassoGeometry.FillContains))
                        image.Selected.Add(renderable);
            }
        }
    }
}
