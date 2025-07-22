using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.ViewModels.Drawing.Structure;
using NET.Paint.ViewModels.Interface;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace NET.Paint.ViewModels.Drawing.Shape
{
    public class RectangleViewModel : FilledShapeViewModel, IRotateable
    {
        public required XRectangle Model { get; set; }

        public override XToolType Type => XToolType.Rectangle;
        public virtual XRectangleStyle Style => XRectangleStyle.Rectangle;

        [Category("Corner")]
        public double CornerRadius
        {
            get => Model.CornerRadius; 
            set => SetProperty(ref Model.CornerRadius, value);
        }

        [Browsable(false)]
        public virtual Point Center => new Point((Points.Min(p => p.X) + Points.Max(p => p.X)) / 2, (Points.Min(p => p.Y) + Points.Max(p => p.Y)) / 2);

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
            OnPropertyChanged(nameof(CornerRadius));
        }

        public override object Clone() => new RectangleViewModel
        {
            Model = this.Model,
            Points = new ObservableCollection<Point>(this.Points)
        };
    }

    public class SquareViewModel : RectangleViewModel
    {
        public override XRectangleStyle Style => XRectangleStyle.Square;

        // Override Center to use Points[0] as the center point
        [Browsable(false)]
        public override Point Center => Points[0];

        [Category("Dimensions")]
        public override double Width => Math.Max(Math.Abs(Points[1].X - Points[0].X), Math.Abs(Points[1].Y - Points[0].Y)) * 2;

        [Category("Dimensions")]
        public override double Height => Math.Max(Math.Abs(Points[1].X - Points[0].X), Math.Abs(Points[1].Y - Points[0].Y)) * 2;

        // Override Location to position from center outward
        public override Point Location => new Point(Center.X - Width / 2, Center.Y - Width / 2);

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            // Call base but skip XRectangle's implementation to avoid conflicts
            OnPropertyChanged(nameof(Location));
            OnPropertyChanged(nameof(Center));
            OnPropertyChanged(nameof(Width));
            OnPropertyChanged(nameof(Height));
            OnPropertyChanged(nameof(CornerRadius));
        }

        public override object Clone() => new SquareViewModel
        {
            Model = this.Model,
            Points = new ObservableCollection<Point>(this.Points)
        };
    }

}
