using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Paint.View.Component.Drawing.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class SimpleRuler : Control
    {
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(nameof(Orientation), typeof(Orientation), typeof(SimpleRuler),
                new FrameworkPropertyMetadata(Orientation.Horizontal, FrameworkPropertyMetadataOptions.AffectsRender));

        public Orientation Orientation
        {
            get => (Orientation)GetValue(OrientationProperty);
            set => SetValue(OrientationProperty, value);
        }

        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register(nameof(Scale), typeof(double), typeof(SimpleRuler),
                new FrameworkPropertyMetadata(10.0, FrameworkPropertyMetadataOptions.AffectsRender));

        public double Scale
        {
            get => (double)GetValue(ScaleProperty);
            set => SetValue(ScaleProperty, value);
        }

        // New property: position marker on ruler
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(nameof(Value), typeof(double), typeof(SimpleRuler),
                new FrameworkPropertyMetadata(-1.0, FrameworkPropertyMetadataOptions.AffectsRender));

        /// <summary>
        /// Position on the ruler to mark (e.g., mouse position).
        /// -1 means no marker shown.
        /// </summary>
        public double Value
        {
            get => (double)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        static SimpleRuler()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SimpleRuler), new FrameworkPropertyMetadata(typeof(SimpleRuler)));
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            double length = Orientation == Orientation.Horizontal ? ActualWidth : ActualHeight;
            double thickness = Orientation == Orientation.Horizontal ? ActualHeight : ActualWidth;

            // Background
            dc.DrawRectangle(Brushes.Transparent, null, new Rect(0, 0, ActualWidth, ActualHeight));

            Pen pen = new Pen(Brushes.Black, 1);
            Pen mediumPen = new Pen(Brushes.Red, 1);

            for (double pos = 0; pos < length; pos += Scale)
            {
                bool isLongTick = ((int)(pos / Scale) % 100 == 0);
                bool isShortTick = ((int)(pos / Scale) % 5 == 0);
                bool isMediumTick = ((int)(pos / Scale) % 50 == 0);
                double tickLength = isLongTick ? thickness : isMediumTick ? thickness * 0.75 : thickness * 0.5;

                if (Orientation == Orientation.Horizontal)
                {
                    if (isShortTick)
                        if (!isMediumTick || isLongTick)
                            dc.DrawLine(pen, new Point(pos, thickness), new Point(pos, thickness - tickLength));
                        else
                            dc.DrawLine(mediumPen, new Point(pos, thickness), new Point(pos, thickness - tickLength - 2));

                    if (isLongTick)
                    {
                        var label = new FormattedText(
                            ((int)(pos / Scale)).ToString(),
                            System.Globalization.CultureInfo.InvariantCulture,
                            FlowDirection.LeftToRight,
                            new Typeface("Segoe UI"),
                            10,
                            Brushes.Black,
                            VisualTreeHelper.GetDpi(this).PixelsPerDip);

                        dc.DrawText(label, new Point(pos + 2, thickness - tickLength - 2));
                    }
                    else if (isMediumTick)
                    {
                        var label = new FormattedText(
                            ((int)(pos / Scale)).ToString(),
                            System.Globalization.CultureInfo.InvariantCulture,
                            FlowDirection.LeftToRight,
                            new Typeface("Segoe UI"),
                            10,
                            Brushes.Red,
                            VisualTreeHelper.GetDpi(this).PixelsPerDip);

                        dc.DrawText(label, new Point(pos + 2, thickness - tickLength - 7));
                    }
                }
                else // Vertical
                {
                    if (isShortTick)
                        if (!isMediumTick || isLongTick)
                            dc.DrawLine(pen, new Point(thickness, pos), new Point(thickness - tickLength, pos));
                        else
                            dc.DrawLine(mediumPen, new Point(thickness, pos), new Point(thickness - tickLength, pos));

                    if (isLongTick)
                    {
                        var label = new FormattedText(
                            ((int)(pos / Scale)).ToString(),
                            System.Globalization.CultureInfo.InvariantCulture,
                            FlowDirection.RightToLeft,
                            new Typeface("Segoe UI"),
                            10,
                            Brushes.Black,
                            VisualTreeHelper.GetDpi(this).PixelsPerDip);

                        dc.PushTransform(new RotateTransform(-90, thickness - tickLength - label.Height - 5, pos));
                        dc.DrawText(label, new Point(thickness - 41, pos + 15));
                        dc.Pop();
                    }
                    else if (isMediumTick)
                    {
                        var label = new FormattedText(
                            ((int)(pos / Scale)).ToString(),
                            System.Globalization.CultureInfo.InvariantCulture,
                            FlowDirection.RightToLeft,
                            new Typeface("Segoe UI"),
                            10,
                            Brushes.Red,
                            VisualTreeHelper.GetDpi(this).PixelsPerDip);

                        dc.PushTransform(new RotateTransform(-90, thickness - tickLength - label.Height - 5, pos));
                        dc.DrawText(label, new Point(thickness - 36, pos + 10));
                        dc.Pop();
                    }
                }
            }

            // Draw marker line if Value is valid
            if (Value >= 0 && Value <= length)
            {
                Pen markerPen = new Pen(Brushes.Red, 1.5) { DashStyle = DashStyles.Dot };
                if (Orientation == Orientation.Horizontal)
                {
                    dc.DrawLine(markerPen, new Point(Value, 0), new Point(Value, thickness));
                }
                else // Vertical
                {
                    dc.DrawLine(markerPen, new Point(0, Value), new Point(thickness, Value));
                }
            }
        }
    }

}
