using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Utility
{
    public abstract class XFill : PropertyNotifier
    {
        public abstract XFillType Type { get; }
    }

    public class XSolidFill : XFill
    {
        public override XFillType Type => XFillType.Solid;
        public Color Color { get; set; }
    }

    public abstract class XGradientFill : XFill
    {
        public override XFillType Type => XFillType.Gradient;
        public abstract XGradientStyle Style { get; }
        public ObservableCollection<XGradientStop> GradientStops { get; set; } = new ObservableCollection<XGradientStop>();
        public XGradientFill() => GradientStops.CollectionChanged += (s, e) => OnPropertyChanged(nameof(GradientStops));
    }

    public class XGradientStop : PropertyNotifier
    {
        private Color _color;
        public Color Color
        {
            get => _color;
            set => SetProperty(ref _color, value);
        }

        private double _offset;
        public double Offset
        {
            get => _offset;
            set => SetProperty(ref _offset, value);
        }
    }

    public class XLinearGradientFill : XGradientFill
    {
        public override XGradientStyle Style => XGradientStyle.Linear;
        public Point StartPoint { get; set; } = new Point(0, 0);
        public Point EndPoint { get; set; } = new Point(1, 0);
    }

    public class XRadialGradientFill : XGradientFill
    {
        public override XGradientStyle Style => XGradientStyle.Radial;
        public Point Center { get; set; }
        public double Radius { get; set; }
    }
}
