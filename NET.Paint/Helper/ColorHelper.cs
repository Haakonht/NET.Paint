using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NET.Paint.Helper
{
    public static class ColorHelper
    {
        private static Color HSLToRGB(double h, double s, double l)
        {
            // Normalize hue to 0-1 range
            h = h / 360.0;

            double r, g, b;

            if (s == 0)
            {
                // Achromatic (gray)
                r = g = b = l;
            }
            else
            {
                // Helper function for HSL to RGB conversion
                double HueToRGB(double p, double q, double t)
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

                r = HueToRGB(p, q, h + 1.0 / 3.0);
                g = HueToRGB(p, q, h);
                b = HueToRGB(p, q, h - 1.0 / 3.0);
            }

            // Convert to byte values and create Color
            return Color.FromArgb(
                255, // Full opacity
                (byte)Math.Round(r * 255),
                (byte)Math.Round(g * 255),
                (byte)Math.Round(b * 255)
            );
        }

        /// <summary>
        /// Converts HSL values to ARGB Color
        /// </summary>
        /// <param name="h">Hue (0-360)</param>
        /// <param name="s">Saturation (0-1)</param>
        /// <param name="l">Lightness (0-1)</param>
        /// <returns>ARGB Color</returns>
        public static Color HSLtoARGB(double h, double s, double l)
        {
            return HSLToRGB(h, s, l);
        }

        /// <summary>
        /// Converts HSL values with alpha to ARGB Color
        /// </summary>
        /// <param name="h">Hue (0-360)</param>
        /// <param name="s">Saturation (0-1)</param>
        /// <param name="l">Lightness (0-1)</param>
        /// <param name="a">Alpha (0-1)</param>
        /// <returns>ARGB Color</returns>
        public static Color HSLtoARGB(double h, double s, double l, double a)
        {
            var rgb = HSLToRGB(h, s, l);
            return Color.FromArgb(
                (byte)Math.Round(a * 255),
                rgb.R,
                rgb.G,
                rgb.B
            );
        }

        /// <summary>
        /// Converts HSL values to ARGB Color
        /// </summary>
        /// <param name="hsl">Tuple containing H (0-360), S (0-1), L (0-1)</param>
        /// <returns>ARGB Color</returns>
        public static Color HSLtoARGB(this (double H, double S, double L) hsl)
        {
            return HSLToRGB(hsl.H, hsl.S, hsl.L);
        }

        /// <summary>
        /// Converts HSL values with alpha to ARGB Color
        /// </summary>
        /// <param name="hsla">Tuple containing H (0-360), S (0-1), L (0-1), A (0-1)</param>
        /// <returns>ARGB Color</returns>
        public static Color HSLtoARGB(this (double H, double S, double L, double A) hsla)
        {
            var rgb = HSLToRGB(hsla.H, hsla.S, hsla.L);
            return Color.FromArgb(
                (byte)Math.Round(hsla.A * 255),
                rgb.R,
                rgb.G,
                rgb.B
            );
        }

        /// <summary>
        /// Converts ARGB Color to HSL values
        /// </summary>
        /// <param name="color">ARGB Color</param>
        /// <returns>Tuple containing H (0-360), S (0-1), L (0-1)</returns>
        public static (double H, double S, double L) ARGBtoHSL(this Color color)
        {
            double r = color.R / 255.0;
            double g = color.G / 255.0;
            double b = color.B / 255.0;

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));
            double delta = max - min;

            double h, s, l;

            // Calculate lightness
            l = (max + min) / 2.0;

            if (delta == 0)
            {
                // Achromatic
                h = s = 0;
            }
            else
            {
                // Calculate saturation
                s = l > 0.5 ? delta / (2.0 - max - min) : delta / (max + min);

                // Calculate hue
                if (max == r)
                {
                    h = (g - b) / delta + (g < b ? 6 : 0);
                }
                else if (max == g)
                {
                    h = (b - r) / delta + 2;
                }
                else
                {
                    h = (r - g) / delta + 4;
                }
                h /= 6;
            }

            return (h * 360, s, l); // Convert hue to degrees
        }

        /// <summary>
        /// Converts ARGB Color to HSL values including alpha
        /// </summary>
        /// <param name="color">ARGB Color</param>
        /// <returns>Tuple containing H (0-360), S (0-1), L (0-1), A (0-1)</returns>
        public static (double H, double S, double L, double A) ARGBtoHSLA(this Color color)
        {
            var hsl = color.ARGBtoHSL();
            return (hsl.H, hsl.S, hsl.L, color.A / 255.0);
        }
    }
}
