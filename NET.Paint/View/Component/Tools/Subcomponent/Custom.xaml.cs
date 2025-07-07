using NET.Paint.Drawing.Factory;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NET.Paint.View.Component.Tools.Subcomponent
{
    /// <summary>
    /// Interaction logic for Contextual.xaml
    /// </summary>
    public partial class Custom : UserControl
    {
        public XPreview ArrowPreview { get; set; } = new XPreview();

        public Custom()
        {
            InitializeComponent();
            CreateArrowPreview();
        }

        private void SelectBitmap(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is not null && DataContext is XService service)
                if (sender is Button && sender is Button button)
                    if (button.Content is Image image)
                        service.Tools.ActiveBitmap = image.Source;
        }

        private void ArrowPreviewChanged(object sender, RoutedPropertyChangedEventArgs<double> e) => CreateArrowPreview();
        private void CreateArrowPreview()
        {
            ArrowPreview.Shape = new XArrow
            {
                Points = XFactory.CreateArrow(
                    new Point(0, 0 + (XTools.Instance.HeadWidth / 2)),
                    new Point(120, 0 + (XTools.Instance.HeadWidth / 2)),
                    XTools.Instance.HeadLength,
                    XTools.Instance.HeadWidth,
                    XTools.Instance.TailWidth
                ),
                FillColor = Colors.LightGray,
                StrokeColor = Colors.Black,
                StrokeThickness = XTools.Instance.StrokeThickness,
                StrokeStyle = XTools.Instance.StrokeStyle
            };
        }
    }
}
