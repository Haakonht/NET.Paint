using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Factory;
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

                if (XTools.Instance.ActiveTool == ToolType.Selector)
                {              
                    if (sender is GridCanvas canvas)
                    {
                        var hitResult = VisualTreeHelper.HitTest(canvas, XTools.Instance.ClickLocation.Value);

                        if (hitResult?.VisualHit is Shape shape)
                            image.Selected = shape.DataContext as XRenderable;
                        else if (hitResult?.VisualHit is TextBlock textBox)
                            image.Selected = textBox.DataContext as XRenderable;
                        else
                            image.Selected = null;
                    }
                }

                if (XTools.Instance.ActiveTool == ToolType.Text)
                {
                    if (Preview.Shape != null && Preview.Shape is XText text && !string.IsNullOrEmpty(text.Text) && image.ActiveLayer != null)
                    {
                        image.ActiveLayer.Shapes.Add(Preview.Shape);
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

                if (e.LeftButton == MouseButtonState.Pressed)
                {

                    if (XTools.Instance.ActiveTool == ToolType.Pencil && Preview.Shape is XPencil pencil)
                    {
                        _lastAddedPoint = XFactory.CreatePencilPoints(pencil.Points, _lastAddedPoint, XTools.Instance.MouseLocation, pencil.Spacing);                        
                    }
                    else
                    {
                        if (XTools.Instance.ClickLocation != null && XTools.Instance.MouseLocation != null)
                            Preview.Shape = XFactory.CreateShape(XTools.Instance);
                    }
                }
                else if (e.XButton1 == MouseButtonState.Pressed)
                {
                    if (XTools.Instance.ActiveTool == ToolType.Bezier && image.ActiveLayer.Shapes.Last() is XBezier bezier)
                        bezier.Ctrl1 = XTools.Instance.MouseLocation;

                }
                else if (XTools.Instance.ActiveTool != ToolType.Text)
                {
                    if (Preview.Shape != null)
                    {
                        if (image.ActiveLayer != null)
                            image.ActiveLayer.Shapes.Add(Preview.Shape);
                        
                        XTools.Instance.ClickLocation = null;
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

        private void TextBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox text)
            {
                text.Focusable = true;
                text.Focus();
            }
        }
    }
}
