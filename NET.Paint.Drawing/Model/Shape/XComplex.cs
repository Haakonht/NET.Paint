using MessagePack;
using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Interface;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace NET.Paint.Drawing.Model.Shape
{
    [MessagePackObject]
    public class XText : XRenderable, IRotateable
    {
        [Key(1)]
        public override XToolType Type => XToolType.Text;

        [Key(3)]
        [Category("Appearance")]
        [DisplayName("Text Content")]
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
        private string _text = "";

        [Key(4)]
        [Category("Appearance")]
        [DisplayName("Font")]
        public string FontFamily
        {
            get => _fontFamily;
            set => SetProperty(ref _fontFamily, value);
        }
        private string _fontFamily = "Arial";

        [Key(5)]
        [Category("Appearance")]
        [DisplayName("Font Size")]
        public double FontSize
        {
            get => _fontSize;
            set
            {
                SetProperty(ref _fontSize, value);
                OnPropertyChanged(nameof(Center));
            }
        }
        private double _fontSize = 11;


        [Key(6)]
        [Category("Style")]
        [DisplayName("Bold")]
        public bool IsBold
        {
            get => _isBold;
            set => SetProperty(ref _isBold, value);
        }
        private bool _isBold = false;

        [Key(7)]
        [Category("Style")]
        [DisplayName("Italic")]
        public bool IsItalic
        {
            get => _isItalic;
            set => SetProperty(ref _isItalic, value);
        }
        private bool _isItalic = false;

        [Key(8)]
        [Category("Style")]
        [DisplayName("Underline")]
        public bool IsUnderline
        {
            get => _isUnderline;
            set => SetProperty(ref _isUnderline, value);
        }
        private bool _isUnderline = false;

        [Key(9)]
        [Category("Style")]
        [DisplayName("Strikethrough")]
        public bool IsStrikethrough
        {
            get => _isStrikethrough;
            set => SetProperty(ref _isStrikethrough, value);
        }
        private bool _isStrikethrough = false;

        [Key(10)]
        [Category("Appearance")]
        [DisplayName("Text Color")]
        public XColor TextColor
        {
            get => _textColor;
            set => SetProperty(ref _textColor, value);
        }
        private XColor _textColor = new XSolidColor { Color = Colors.Red };

        [Key(11)]
        [Category("Layout")]
        [DisplayName("Rotation")]
        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }
        private double _rotation = 0;

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("Location")]
        public Point Location
        {
            get => Points[0];
            set => Points[0] = value;
        }

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("Center")]
        public Point Center => new Point(Location.X + (Width / 2), Location.Y + (Height / 2));

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("Width")]
        public double Width => new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(FontFamily.ToString()), FontSize, TextColor.ToBrush()).Width;

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("Height")]
        public double Height => new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(FontFamily.ToString()), FontSize, TextColor.ToBrush()).Height;

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(Center));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
        }

        public override object Clone() => new XText
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

        #endregion
    }

    [MessagePackObject]
    public class XBitmap : XRenderable, IRotateable
    {
        [Key(1)]
        public override XToolType Type => XToolType.Bitmap;
        
        [Key(3)]
        [Category("Layout")]
        [DisplayName("Offset")]
        public Point Offset
        {
            get => _offset;
            set => SetProperty(ref _offset, value);
        }
        private Point _offset = new Point(0, 0);

        [Key(4)]
        [Category("Bitmap")]
        [DisplayName("Source")]
        public ImageSource Source
        {
            get => _bitmap;
            set => SetProperty(ref _bitmap, value);
        }
        private ImageSource _bitmap = null;

        [Key(5)]
        [Category("Bitmap")]
        [DisplayName("Scaling")]
        public XScalingMode Scaling
        {
            get => _scaling;
            set => SetProperty(ref _scaling, value);
        }
        private XScalingMode _scaling = XScalingMode.Original;


        [Key(6)]
        [Category("Layout")]
        [DisplayName("Rotation")]
        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }
        private double _rotation = 0;

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("Location")]
        public Point Location
        {
            get => Points[0];
            set => Points[0] = value;
        }

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("Width")]
        public double Width => Points.Max(p => p.X) - Points.Min(p => p.X);
        
        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("Height")]
        public double Height => Points.Max(p => p.Y) - Points.Min(p => p.Y);

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("Center")]
        public Point Center => new Point(Location.X + (Width / 2), Location.Y + (Height / 2));

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(Center));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
        }

        public override object Clone() => new XBitmap
        {
            Source = this.Source.Clone(),
            Scaling = this.Scaling,
            Offset = this.Offset,
            Rotation = this.Rotation,
            Points = new ObservableCollection<Point>(this.Points)
        };

        #endregion
    }

    [MessagePackObject]
    public class XEffect : XRenderable
    {
        [Key(1)]
        public override XToolType Type => XToolType.Effect;

        [Key(3)]
        [Category("Effect")]
        [DisplayName("Effect")]
        public Effect Effect
        {
            get => _effect;
            set => SetProperty(ref _effect, value);
        }
        private Effect _effect = new BlurEffect()
        {
            Radius = 5,
            KernelType = KernelType.Gaussian,
            RenderingBias = RenderingBias.Quality
        };

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("Location")]
        public virtual Point Location => new Point(Points.Min(p => p.X), Points.Min(p => p.Y));

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("Width")]
        public virtual double Width => Points.Max(p => p.X) - Points.Min(p => p.X);

        [IgnoreMember]
        [Category("Layout")]
        [DisplayName("Height")]
        public virtual double Height => Points.Max(p => p.Y) - Points.Min(p => p.Y);

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
        }

        public override object Clone() => new XEffect
        {
            Effect = this.Effect,
            Points = new ObservableCollection<Point>(this.Points)
        };

        #endregion
    }
}
