using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Utility;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NET.Paint.Drawing.Helper
{
    public class XHelper
    {
        public static string ImageSourceToBase64(ImageSource image)
        {
            if (image is BitmapSource bitmapSource)
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                using var ms = new MemoryStream();
                encoder.Save(ms);
                return Convert.ToBase64String(ms.ToArray());
            }
            return string.Empty;
        }

        public static ImageSource Base64ToImageSource(string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            var image = new BitmapImage();
            using var ms = new MemoryStream(bytes);
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = ms;
            image.EndInit();
            image.Freeze();
            return image;
        }

        public static ImageSource CreateRandomBitmap(int width, int height)
        {
            var renderTarget = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);
            var visual = new DrawingVisual();

            using (var context = visual.RenderOpen())
            {
                // Fill background with a random color
                var random = new Random();
                var backgroundColor = Color.FromRgb(
                    (byte)random.Next(0, 256),
                    (byte)random.Next(0, 256),
                    (byte)random.Next(0, 256)
                );
                context.DrawRectangle(new SolidColorBrush(backgroundColor), null, new Rect(0, 0, width, height));

                // Draw random shapes
                for (int i = 0; i < 5; i++)
                {
                    var shapeColor = Color.FromRgb(
                        (byte)random.Next(0, 256),
                        (byte)random.Next(0, 256),
                        (byte)random.Next(0, 256)
                    );
                    var brush = new SolidColorBrush(shapeColor);

                    var x = random.Next(0, width);
                    var y = random.Next(0, height);
                    var size = random.Next(20, 50);

                    context.DrawEllipse(brush, null, new Point(x, y), size, size);
                }
            }

            renderTarget.Render(visual);
            return renderTarget;
        }

        public static XGradientFill CreateGradient(XTools tools)
        {
            if (tools.ActiveGradientStyle == XGradientStyle.Linear)
            {
                return new XLinearGradientFill
                {
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(1, 0),
                    GradientStops = new ObservableCollection<XGradientStop>
                    {
                        new XGradientStop { Color = tools.FillColor, Offset = 0 },
                        new XGradientStop { Color = tools.StrokeColor, Offset = 1 }
                    }
                };
            }
            else
            {
                return new XRadialGradientFill
                {
                    Center = new Point(0.5, 0.5),
                    Radius = 0.5,
                    GradientStops = new ObservableCollection<XGradientStop>
                    {
                        new XGradientStop { Color = tools.FillColor, Offset = 0 },
                        new XGradientStop { Color = tools.StrokeColor, Offset = 1 }
                    }
                };
            }
        }
    }
}
