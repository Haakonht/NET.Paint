using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model.Structure;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace NET.Paint.Drawing.Model.Shape
{
    public class TextViewModel : RenderableViewModel, IRotateable
    {
        public override XToolType Type => XToolType.Text;

        public Point Location
        {
            get => Points[0];
            set => Points[0] = value;
        }

        private string _text = "";
        public string Text
        {
            get => _text;
            set
            {
                SetProperty(ref _text, value);
                OnPropertyChanged(nameof(Width));
                OnPropertyChanged(nameof(Height));
                OnPropertyChanged(nameof(Center));
            }
        }

        private FontFamily _fontFamily = new FontFamily("Arial");
        public FontFamily FontFamily
        {
            get => _fontFamily;
            set => SetProperty(ref _fontFamily, value);
        }

        private double _fontSize = 11;
        public double FontSize
        {
            get => _fontSize;
            set
            {
                SetProperty(ref _fontSize, value);
                OnPropertyChanged(nameof(Center));
            }
        }

        private bool _isBold = false;
        public bool IsBold
        {
            get => _isBold;
            set => SetProperty(ref _isBold, value);
        }

        private bool _isItalic = false;
        public bool IsItalic
        {
            get => _isItalic;
            set => SetProperty(ref _isItalic, value);
        }

        private bool _isUnderline = false;
        public bool IsUnderline
        {
            get => _isUnderline;
            set => SetProperty(ref _isUnderline, value);
        }

        private bool _isStrikethrough = false;
        public bool IsStrikethrough
        {
            get => _isStrikethrough;
            set => SetProperty(ref _isStrikethrough, value);
        }

        private Brush _textColor;
        public Brush TextColor
        {
            get => _textColor;
            set => SetProperty(ref _textColor, value);
        }

        [Browsable(false)]
        public Point Center => new Point(Location.X + (Width / 2), Location.Y + (Height / 2));

        public double Width => new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(FontFamily.ToString()), FontSize, TextColor).Width;

        public double Height => new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(FontFamily.ToString()), FontSize, TextColor).Height;

        private double _rotation = 0;
        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }

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
            Location = this.Location,
            TextColor = this.TextColor,
            FontFamily = this.FontFamily,
            FontSize = this.FontSize,
            IsBold = this.IsBold,
            IsItalic = this.IsItalic,
            IsUnderline = this.IsUnderline,
            IsStrikethrough = this.IsStrikethrough,
            Rotation = this.Rotation,
            Points = new ObservableCollection<Point>(this.Points)
        };
    }

    public class BitmapViewModel : RenderableViewModel, IRotateable
    {
        public override XToolType Type => XToolType.Bitmap;

        public Point Location
        {
            get => Points[0];
            set => Points[0] = value;
        }

        [Category("Dimensions")]
        public double Width
        {
            get
            {
                return Points.Max(p => p.X) - Points.Min(p => p.X);
            }
        }

        [Category("Dimensions")]
        public double Height
        {
            get
            {
                return Points.Max(p => p.Y) - Points.Min(p => p.Y);
            }
        }

        [Browsable(false)]
        public Point Center => new Point(Location.X + (Width / 2), Location.Y + (Height / 2));

        private double _rotation = 0;
        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
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
            Source = this.Source,
            Scaling = this.Scaling,
            ClipOffset = this.ClipOffset,
            Rotation = this.Rotation,
            Points = new ObservableCollection<Point>(this.Points)
        };
    }

    public class EffectViewModel : RenderableViewModel
    {
        public override XToolType Type => XToolType.Effect;

        [DisplayName("Position")]
        public virtual Point Location => new Point(Points.Min(p => p.X), Points.Min(p => p.Y));

        [Category("Dimensions")]
        public virtual double Width => Points.Max(p => p.X) - Points.Min(p => p.X);

        [Category("Dimensions")]
        public virtual double Height => Points.Max(p => p.Y) - Points.Min(p => p.Y);

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
        }

        private Effect _effect = new BlurEffect()
        {
            Radius = 5,
            KernelType = KernelType.Gaussian,
            RenderingBias = RenderingBias.Quality
        };

        public Effect Effect
        {
            get => _effect;
            set => SetProperty(ref _effect, value);
        }

        public override object Clone() => new EffectViewModel
        {
            Effect = this.Effect,
            Points = new ObservableCollection<Point>(this.Points)
        };
    }
}
