using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Factory;
using NET.Paint.Drawing.Model.Shape;
using NET.Paint.ViewModels.Drawing.Structure;
using NET.Paint.ViewModels.Interface;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;

namespace NET.Paint.ViewModels.Drawing.Shape
{
    public class LineViewModel : StrokedShapeViewModel
    {
        public required XLine Model { get; set; }

        public override XToolType Type => XToolType.Line;

        [Category("Position")]
        public Point Start
        {
            get => Model.Points[0];
            set => Model.Points[0] = value;
        }
        [Category("Position")]
        public Point End
        {
            get => Model.Points[1];
            set => Model.Points[1] = value;
        }
        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Start));
            OnPropertyChanged(nameof(End));
        }

        public override object Clone() => new LineViewModel
        {
            Model = new XLine
            {
                StrokeBrush = this.StrokeBrush,
                StrokeThickness = this.StrokeThickness,
                StrokeStyle = this.StrokeStyle,
                Points = new List<Point>(this.Points)
            }
        };
    }

    public class PolylineViewModel : StrokedShapeViewModel
    {
        public required XPolyline Model { get; set; }

        public override XToolType Type => XToolType.Pencil;

        [DisplayName("Resolution")]
        public double Spacing
        {
            get => Model.Spacing;
            set
            {
                if (value != Model.Spacing && value > 9)
                {
                    ShapeFactory.ResamplePoints(Points, value);
                    SetProperty(ref Model.Spacing, value);
                }
            }
        }

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) => OnPropertyChanged(nameof(Points));

        public override object Clone() => new PolylineViewModel
        {
            Model = new XPolyline
            {
                StrokeBrush = this.StrokeBrush,
                StrokeThickness = this.StrokeThickness,
                StrokeStyle = this.StrokeStyle,
                Points = new List<Point>(this.Points),
                Spacing = this.Spacing
            }
        };

    }

    public class CurveViewModel : StrokedShapeViewModel, IControlPoints
    {
        public required XCurve Model { get; set; }

        public override XToolType Type => XToolType.Curve;

        [Category("Position")]
        public Point Start
        {
            get => Model.Points[0];
            set => Model.Points[0] = value;
        }

        [Category("Position")]
        public Point End
        {
            get => Model.Points[1];
            set => Model.Points[1] = value;
        }

        [Category("Position")]
        [DisplayName("Control")]
        public Point Ctrl1
        {
            get => Model.Points[2];
            set => Model.Points[2] = value;
        }

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Start));
            OnPropertyChanged(nameof(End));
            OnPropertyChanged(nameof(Ctrl1));
        }

        public override object Clone() => new CurveViewModel
        {
            Model = new XCurve
            {
                StrokeBrush = this.StrokeBrush,
                StrokeThickness = this.StrokeThickness,
                StrokeStyle = this.StrokeStyle,
                Points = new List<Point>(this.Points)
            }
        };
    }

    public class BezierViewModel : CurveViewModel
    {
        [Browsable(false)]
        public Point Ctrl2
        {
            get => Model.Points[3];
            set => Model.Points[3] = value;
        }

        public override void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.CollectionChanged(sender, e);
            OnPropertyChanged(nameof(Start));
            OnPropertyChanged(nameof(End));
            OnPropertyChanged(nameof(Ctrl1));
            OnPropertyChanged(nameof(Ctrl2));
        }

        public override object Clone() => new BezierViewModel
        {
            Model = new XBezier
            {
                StrokeBrush = this.StrokeBrush,
                StrokeThickness = this.StrokeThickness,
                StrokeStyle = this.StrokeStyle,
                Points = new List<Point>(this.Points)
            }
        };
    }
}
