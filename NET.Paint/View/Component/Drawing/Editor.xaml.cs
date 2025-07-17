using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Factory;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.View.Component.Drawing.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using XSelectionMode = NET.Paint.Drawing.Constant.XSelectionMode;

namespace NET.Paint.View.Component.Drawing
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

                if (tools.ActiveTool == XToolType.Text)
                {
                    if (Preview.Shape != null && Preview.Shape is XText text && !string.IsNullOrEmpty(text.Text) && image.ActiveLayer != null)
                    {
                        if (image.ActiveLayer is IShapeLayer vectorLayer)
                            vectorLayer.Shapes.Add(Preview.Shape);
                        
                        Preview.Shape = null;
                    }
                    else
                        Preview.Shape = XFactory.CreateShape(tools);
                }

                if (tools.ActiveTool == XToolType.Selector)
                {
                    if (tools.SelectionMode == XSelectionMode.Pointer)
                        SingleSelect(sender, tools, image);
                    else
                        Preview.Shape = XFactory.CreateShape(tools);
                }

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
                        if (Preview.Shape is XPolyline pencil)
                        {
                            if (tools.PencilMode == XPencilMode.Add || tools.ActiveTool == XToolType.Selector)
                                XFactory.CreatePencilPoints(pencil.Points, pencil.Points.LastOrDefault(), tools.MouseLocation, pencil.PointSpacing);
                        }

                        else if (tools.ActiveTool == XToolType.Pencil && tools.PencilMode == XPencilMode.Remove)
                            XFactory.RemovePencilPoints(image.ActiveLayer, tools.MouseLocation, tools.EraserTolerance);

                        else if (tools.ActiveTool == XToolType.Selector && tools.SelectionMode == XSelectionMode.Pointer)
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
                        if ((tools.ActiveTool == XToolType.Bezier || tools.ActiveTool == XToolType.Curve) && image.ActiveLayer is IShapeLayer vectorLayer && vectorLayer.Shapes.Last() is IControlPoints cps)
                            cps.Ctrl1 = tools.MouseLocation;

                    }
                    else if (e.XButton2 == MouseButtonState.Pressed)
                    {
                        if (tools.ActiveTool == XToolType.Bezier && image.ActiveLayer is IShapeLayer vectorLayer && vectorLayer.Shapes.Last() is XBezier bezier)
                            bezier.Ctrl2 = tools.MouseLocation;

                    }
                    else if (tools.ActiveTool != XToolType.Text)
                    {
                        if (Preview.Shape != null)
                        {
                            if (tools.ActiveTool == XToolType.Selector)
                            {
                                if (tools.SelectionMode == XSelectionMode.Lasso)
                                    LassoSelect(image);
                                else if (tools.SelectionMode == XSelectionMode.Rectangle)
                                    RectangleSelect(image);

                                if (image.Selected.Count > 0)
                                    tools.SelectionMode = XSelectionMode.Pointer;
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

                            if (tools.ActiveTool != XToolType.Selector)
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
            if (image.ActiveLayer is IShapeLayer shapeLayer && shapeLayer.Shapes.Count == 0) return;
            if (image.ActiveLayer is IBitmapLayer bitmapLayer && bitmapLayer.Bitmap == null) return;

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
                if (tools.ActiveTool == XToolType.Selector && tools.SelectionMode == XSelectionMode.Pointer)
                {
                    var hitResult = VisualTreeHelper.HitTest(canvas, tools.ClickLocation.Value);
                    XRenderable hitObject = null;

                    if (hitResult?.VisualHit is Shape shape)
                        hitObject = shape.DataContext as XRenderable;
                    else if (hitResult?.VisualHit is TextBlock textBox)
                        hitObject = textBox.DataContext as XRenderable;
                    else if (hitResult?.VisualHit is Image imageControl)
                        hitObject = imageControl.DataContext as XRenderable;

                    bool hitSelectedObject = hitObject != null && image.Selected.Contains(hitObject);

                    if (!hitSelectedObject)
                    {
                        image.Selected.Clear();
                        
                        if (hitObject != null)
                        {
                            image.Selected.Add(hitObject);
                        }
                    }
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
