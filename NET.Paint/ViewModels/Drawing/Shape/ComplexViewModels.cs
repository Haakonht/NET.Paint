using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.ViewModels.Drawing.Structure;
using NET.Paint.ViewModels.Interface;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.ViewModels.Drawing.Shape
{
    public class TextViewModel : RenderableViewModel, IRotateable
    {
        public required XText Model { get; set; }

        public override XToolType Type => XToolType.Text;

        public Point Location
        {
            get => Points[0];
            set => Points[0] = value;
        }

        public string Text
        {
            get => Model.Text;
            set
            {
                SetProperty(ref Model.Text, value);
                OnPropertyChanged(nameof(Width));
                OnPropertyChanged(nameof(Height));
                OnPropertyChanged(nameof(Center));
            }
        }

        public FontFamily FontFamily
        {
            get => Model.FontFamily;
            set => SetProperty(ref Model.FontFamily, value);
        }

        public double FontSize
        {
            get => Model.FontSize;
            set
            {
                SetProperty(ref Model.FontSize, value);
                OnPropertyChanged(nameof(Center));
            }
        }

        public bool IsBold
        {
            get => Model.Bold;
            set => SetProperty(ref Model.Bold, value);
        }

        public bool IsItalic
        {
            get => Model.Italic;
            set => SetProperty(ref Model.Italic, value);
        }

        public bool IsUnderline
        {
            get => Model.Underline;
            set => SetProperty(ref Model.Underline, value);
        }

        public bool IsStrikethrough
        {
            get => Model.Strikethrough;
            set => SetProperty(ref Model.Strikethrough, value);
        }

        public Brush TextColor
        {
            get => Model.TextColor;
            set => SetProperty(ref Model.TextColor, value);
        }

        public double Rotation
        {
            get => Model.Rotation;
            set => SetProperty(ref Model.Rotation, value);
        }

        [Browsable(false)]
        public Point Center => new Point(Location.X + (Width / 2), Location.Y + (Height / 2));

        public double Width => new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(FontFamily.ToString()), FontSize, TextColor).Width;

        public double Height => new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(FontFamily.ToString()), FontSize, TextColor).Height;

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(Center));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
        }

        public override object Clone() => new TextViewModel
        {
            Model = new XText
            {
                Text = this.Text,
                FontFamily = this.FontFamily,
                FontSize = this.FontSize,
                Bold = this.IsBold,
                Italic = this.IsItalic,
                Underline = this.IsUnderline,
                Strikethrough = this.IsStrikethrough,
                TextColor = this.TextColor,
                Rotation = this.Rotation,
                Points = new List<Point>(this.Points)
            },
        };
    }

    public class BitmapViewModel : RenderableViewModel, IRotateable
    {
        public required XBitmap Model { get; set; }

        public override XToolType Type => XToolType.Bitmap;

        public Point Location
        {
            get => Points[0];
            set => Points[0] = value;
        }

        [Category("Dimensions")]
        public double Width => Points.Max(p => p.X) - Points.Min(p => p.X);

        [Category("Dimensions")]
        public double Height => Points.Max(p => p.Y) - Points.Min(p => p.Y);

        [Browsable(false)]
        public Point Center => new Point(Location.X + (Width / 2), Location.Y + (Height / 2));

        public double Rotation
        {
            get => Model.Rotation;
            set => SetProperty(ref Model.Rotation, value);
        }

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(Center));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
        }

        private Point _clipOffset = new Point(0, 0);
        public Point ClipOffset
        {
            get => _clipOffset;
            set => SetProperty(ref _clipOffset, value);
        }

        private ImageSource? _source = null;
        public ImageSource? Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }

        private XScalingMode _scaling = XScalingMode.Original;
        public XScalingMode Scaling
        {
            get => _scaling;
            set => SetProperty(ref _scaling, value);
        }

        public override object Clone() => new BitmapViewModel
        {
            Model = new XBitmap
            {
                Source = this.Source,
                Scaling = this.Scaling,
                ClipOffset = this.ClipOffset,
                Rotation = this.Rotation,
                Points = new List<Point>(this.Points)
            }
        };
    }
}
