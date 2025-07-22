using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NET.Paint.View.Component.Tools.Converters
{
    public class HueToSLBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double hue)
            {
                return CreateSLBackground(hue);
            }
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private ImageBrush CreateSLBackground(double hue)
        {
            int width = 256;
            int height = 256;
            var bitmap = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
            var pixels = new uint[width * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Saturation: 0 (left) to 1 (right)
                    double saturation = (double)x / (width - 1);

                    // Lightness: 1 (top) to 0 (bottom)
                    double lightness = 1.0 - (double)y / (height - 1);

                    // Convert HSL to RGB
                    Color color = HSLToRGB(hue, saturation, lightness);

                    pixels[y * width + x] = (uint)((color.A << 24) | (color.R << 16) | (color.G << 8) | color.B);
                }
            }

            bitmap.WritePixels(new System.Windows.Int32Rect(0, 0, width, height), pixels, width * 4, 0);
            return new ImageBrush(bitmap) { Stretch = Stretch.Fill };
        }

        private Color HSLToRGB(double h, double s, double l)
        {
            h = h / 360.0; // Normalize hue to 0-1

            double r, g, b;

            if (s == 0)
            {
                r = g = b = l; // achromatic
            }
            else
            {
                double hue2rgb(double p, double q, double t)
                {
                    if (t < 0) t += 1;
                    if (t > 1) t -= 1;
                    if (t < 1.0 / 6.0) return p + (q - p) * 6 * t;
                    if (t < 1.0 / 2.0) return q;
                    if (t < 2.0 / 3.0) return p + (q - p) * (2.0 / 3.0 - t) * 6;
                    return p;
                }

                double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
                double p = 2 * l - q;
                r = hue2rgb(p, q, h + 1.0 / 3.0);
                g = hue2rgb(p, q, h);
                b = hue2rgb(p, q, h - 1.0 / 3.0);
            }

            return Color.FromRgb(
                (byte)Math.Round(r * 255),
                (byte)Math.Round(g * 255),
                (byte)Math.Round(b * 255)
            );
        }
    }
}