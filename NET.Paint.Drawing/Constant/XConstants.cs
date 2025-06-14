using NET.Paint.Drawing.Model.Utility;
using System.Windows.Media;

namespace NET.Paint.Drawing.Constant
{
    public static class XConstants
    {
        public static List<ToolType> ToolOptions => Enum.GetValues(typeof(ToolType)).Cast<ToolType>().ToList();
        public static List<FontFamily> FontFamilyOptions => Fonts.SystemFontFamilies.ToList();
        public static List<double> FontSizeOptions => new List<double> { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
        
        public static List<XStrokeStyle> StrokeStyleOptions = new List<XStrokeStyle>
        {
            new XStrokeStyle { Name = "Solid", DashArray = null },
            new XStrokeStyle { Name = "Dashed", DashArray = new DoubleCollection() { 4, 2 } },
            new XStrokeStyle { Name = "Dotted", DashArray = new DoubleCollection() { 1, 2 } },
            new XStrokeStyle { Name = "DashDot", DashArray = new DoubleCollection() { 4, 2, 1, 2 } },
        };
    }
}
