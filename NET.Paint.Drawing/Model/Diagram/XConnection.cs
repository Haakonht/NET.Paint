using MessagePack;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Diagram
{
    public enum ConnectionPoint
    {
        One,
        Many
    }

    public class XConnection : XObject
    {
        // Object 1
        public Guid Reference1 { get; set; }
        public ConnectionPoint Relation1 { get; set; }
        
        // Object 2
        public Guid Reference2 { get; set; }
        public ConnectionPoint Relation2 { get; set; }

        // Connection
        public ObservableCollection<Point> Path = new ObservableCollection<Point>();

        [Key(3)]
        [Category("Stroke")]
        [DisplayName("Color")]
        public XColor Stroke
        {
            get => _stroke;
            set => SetProperty(ref _stroke, value);
        }
        private XColor _stroke;

        [Key(4)]
        [Category("Stroke")]
        [DisplayName("Thickness")]
        public double StrokeThickness
        {
            get => _strokeThickness;
            set => SetProperty(ref _strokeThickness, value);
        }
        private double _strokeThickness;


        [Key(5)]
        [Category("Stroke")]
        [DisplayName("Style")]
        public DoubleCollection StrokeStyle
        {
            get => _strokeStyle;
            set => SetProperty(ref _strokeStyle, value);
        }
        private DoubleCollection _strokeStyle;

    }
}
