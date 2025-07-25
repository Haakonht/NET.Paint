using MessagePack;
using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Utility
{
    [Union(0, typeof(XSolidColor))]
    [Union(1, typeof(XGradient))]
    [MessagePackObject]
    public abstract class XColor : PropertyNotifier
    {
        [IgnoreMember]
        public abstract XColorType Type { get; }
    }

    [MessagePackObject]
    public class XSolidColor : XColor
    {
        [Key(0)]
        public override XColorType Type => XColorType.Solid;

        [Key(1)]
        public Color Color 
        { 
            get => _color;
            set => SetProperty(ref _color, value);
        }
        private Color _color;
    }

    [Union(0, typeof(XLinearGradient))]
    [Union(1, typeof(XRadialGradient))]
    [MessagePackObject]
    public abstract class XGradient : XColor
    {
        [Key(0)]
        public override XColorType Type => XColorType.Gradient;

        [IgnoreMember]
        public abstract XGradientStyle Style { get; }

        [Key(1)]
        public ObservableCollection<XGradientStop> GradientStops { get; set; } = new ObservableCollection<XGradientStop>();
        public XGradient() => GradientStops.CollectionChanged += (s, e) => OnPropertyChanged(nameof(GradientStops));
    }

    [MessagePackObject]
    public class XGradientStop : PropertyNotifier
    {
        [Key(0)]
        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }
        private Color _color;

        [Key(1)]
        public double Offset
        {
            get => _offset;
            set => SetProperty(ref _offset, value);
        }
        private double _offset;
    }

    [MessagePackObject]
    public class XLinearGradient : XGradient
    {
        [Key(2)]
        public override XGradientStyle Style => XGradientStyle.Linear;

        [Key(3)]
        public Point StartPoint { get; set; } = new Point(0, 0);

        [Key(4)]
        public Point EndPoint { get; set; } = new Point(1, 0);
    }

    [MessagePackObject]
    public class XRadialGradient : XGradient
    {
        [Key(2)]
        public override XGradientStyle Style => XGradientStyle.Radial;

        [Key(3)]
        public Point Center { get; set; }

        [Key(4)]
        public double Radius { get; set; }
    }

    public static class XColorExtensions
    {
        /// <summary>
        /// Converts an XColor to a WPF Brush
        /// </summary>
        /// <param name="xColor">The XColor to convert</param>
        /// <returns>A Brush representing the XColor</returns>
        public static Brush ToBrush(this XColor xColor)
        {
            if (xColor == null)
                return null;

            return xColor switch
            {
                XSolidColor solid => new SolidColorBrush(solid.Color),
                XLinearGradient linearGradient => CreateLinearGradientBrush(linearGradient),
                XRadialGradient radialGradient => CreateRadialGradientBrush(radialGradient),
                _ => null
            };
        }

        public static Color ToColor(this XColor xColor, bool isFill = false)
        {
            return xColor switch
            {
                XSolidColor solid => solid.Color,
                XLinearGradient linearGradient => isFill ? linearGradient.GradientStops.Last().Color : linearGradient.GradientStops.First().Color,
                XRadialGradient radialGradient => isFill ? radialGradient.GradientStops.Last().Color : radialGradient.GradientStops.First().Color,
                _ => Colors.Transparent
            };
        }

        private static LinearGradientBrush CreateLinearGradientBrush(XLinearGradient xLinearGradient)
        {
            var brush = new LinearGradientBrush
            {
                StartPoint = xLinearGradient.StartPoint,
                EndPoint = xLinearGradient.EndPoint
            };

            foreach (var stop in xLinearGradient.GradientStops)
            {
                brush.GradientStops.Add(new GradientStop(stop.Color, stop.Offset));
            }

            return brush;
        }

        private static RadialGradientBrush CreateRadialGradientBrush(XRadialGradient xRadialGradient)
        {
            var brush = new RadialGradientBrush
            {
                Center = xRadialGradient.Center,
                RadiusX = xRadialGradient.Radius,
                RadiusY = xRadialGradient.Radius
            };

            foreach (var stop in xRadialGradient.GradientStops)
            {
                brush.GradientStops.Add(new GradientStop(stop.Color, stop.Offset));
            }

            return brush;
        }
    }
}
