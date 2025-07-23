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
        public string FontFamily
        {
            get => _fontFamily;
            set => SetProperty(ref _fontFamily, value);
        }
        private string _fontFamily = "Arial";

        [Key(5)]
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
        public bool IsBold
        {
            get => _isBold;
            set => SetProperty(ref _isBold, value);
        }
        private bool _isBold = false;

        [Key(7)]
        public bool IsItalic
        {
            get => _isItalic;
            set => SetProperty(ref _isItalic, value);
        }
        private bool _isItalic = false;

        [Key(8)]
        public bool IsUnderline
        {
            get => _isUnderline;
            set => SetProperty(ref _isUnderline, value);
        }
        private bool _isUnderline = false;

        [Key(9)]
        public bool IsStrikethrough
        {
            get => _isStrikethrough;
            set => SetProperty(ref _isStrikethrough, value);
        }
        private bool _isStrikethrough = false;

        [Key(10)]
        public XColor TextColor
        {
            get => _textColor;
            set => SetProperty(ref _textColor, value);
        }
        private XColor _textColor;

        [Key(11)]
        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }
        private double _rotation = 0;

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Browsable(false)]
        public Point Location
        {
            get => Points[0];
            set => Points[0] = value;
        }

        [IgnoreMember]
        [Browsable(false)]
        public Point Center => new Point(Location.X + (Width / 2), Location.Y + (Height / 2));

        [IgnoreMember]
        [Browsable(false)]
        public double Width => new FormattedText(Text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface(FontFamily.ToString()), FontSize, TextColor.ToBrush()).Width;

        [IgnoreMember]
        [Browsable(false)]
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
        public Point ClipOffset
        {
            get => _clipOffset;
            set => SetProperty(ref _clipOffset, value);
        }
        private Point _clipOffset = new Point(0, 0);

        [Key(4)]
        public string Bitmap
        {
            get => _bitmap;
            set => SetProperty(ref _bitmap, value);
        }
        private string _bitmap = null;

        [Key(5)]
        public XScalingMode Scaling
        {
            get => _scaling;
            set => SetProperty(ref _scaling, value);
        }
        private XScalingMode _scaling = XScalingMode.Original;


        [Key(6)]
        public double Rotation
        {
            get => _rotation;
            set => SetProperty(ref _rotation, value);
        }
        private double _rotation = 0;

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Browsable(false)]
        public Point Location
        {
            get => Points[0];
            set => Points[0] = value;
        }

        [IgnoreMember]
        [Browsable(false)]
        public double Width => Points.Max(p => p.X) - Points.Min(p => p.X);
        
        [IgnoreMember]
        [Browsable(false)]
        public double Height => Points.Max(p => p.Y) - Points.Min(p => p.Y);

        [IgnoreMember]
        [Browsable(false)]
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
            Bitmap = this.Bitmap,
            Scaling = this.Scaling,
            ClipOffset = this.ClipOffset,
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
        [Browsable(false)]
        public virtual Point Location => new Point(Points.Min(p => p.X), Points.Min(p => p.Y));

        [IgnoreMember]
        [Browsable(false)]
        public virtual double Width => Points.Max(p => p.X) - Points.Min(p => p.X);

        [IgnoreMember]
        [Browsable(false)]
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
