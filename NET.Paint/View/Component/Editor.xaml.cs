using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Factory;
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
                image.Tools.ClickLocation = new XPoint(e.GetPosition(sender as UIElement));

                if (image.Tools.ActiveTool == ToolType.Selector)
                {
                    if (sender is GridCanvas canvas)
                    {
                        var hitResult = VisualTreeHelper.HitTest(canvas, image.Tools.ClickLocation.Value);

                        if (hitResult?.VisualHit is Shape shape)
                            image.Selected = shape.DataContext;
                        else
                            image.Selected = null;
                    }
                }
            }

        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            var image = DataContext as XImage;

            if (image != null)
            {
                image.Tools.MouseLocation = new XPoint(e.GetPosition(sender as UIElement));

                if (e.LeftButton == MouseButtonState.Pressed)
                {

                    if (image.Tools.ActiveTool == ToolType.Pencil && Preview.Shape is XPencil pencil)
                    {
                        _lastAddedPoint = XFactory.CreatePencilPoints(pencil.Points, _lastAddedPoint, image.Tools.MouseLocation.Value, pencil.Spacing);                        
                    }
                    else
                    {
                        if (image.Tools.ClickLocation != null && image.Tools.MouseLocation != null)
                            Preview.Shape = XFactory.CreateShape(image.Tools);
                    }
                }
                else if (e.XButton1 == MouseButtonState.Pressed)
                {
                    if (image.Tools.ActiveTool == ToolType.Bezier && image.ActiveLayer.Shapes.Last() is XBezier bezier)
                        bezier.Ctrl1 = image.Tools.MouseLocation.Value;

                }
                else
                {
                    if (Preview.Shape != null)
                    {
                        image.ActiveLayer.Shapes.Add(Preview.Shape);
                        image.Tools.ClickLocation = null;
                        Preview.Shape = null;
                        _lastAddedPoint = null;
                    }
                }
            }
        }

        private void OpenContext(object sender, MouseButtonEventArgs e)
        {
            Toolcontext.IsOpen = true;
            e.Handled = true;
        }
    }
}
