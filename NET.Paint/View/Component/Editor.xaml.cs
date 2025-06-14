using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Factory;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NET.Paint.View.Component
{
    /// <summary>
    /// Interaction logic for Editor.xaml
    /// </summary>
    public partial class Editor : UserControl
    {
        private DateTime _lastAddTime = DateTime.MinValue;
        private readonly TimeSpan _interval = TimeSpan.FromMilliseconds(50);

        public XPreview Preview { get; } = new XPreview();

        public Editor()
        {
            InitializeComponent();
        }

        private void MouseDown(object sender, MouseButtonEventArgs e)
        {
            var image = DataContext as XImage;
            
            if (image != null)
                image.Tools.ClickLocation = e.GetPosition(sender as UIElement);
        }

        private void MouseMove(object sender, MouseEventArgs e)
        {
            var image = DataContext as XImage;

            if (image != null)
            {
                image.Tools.MouseLocation = e.GetPosition(sender as UIElement);

                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    if (image.Tools.ActiveTool == ToolType.Pencil && Preview.Shape is XPencil)
                    {
                        if ((DateTime.Now - _lastAddTime) >= _interval)
                        {
                            Preview.Shape.Points.Add(image.Tools.MouseLocation.Value);
                            _lastAddTime = DateTime.Now;
                        }
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
