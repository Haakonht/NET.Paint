using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET.XDraw.Models.Shapes;
using NET.XDraw.Utility;

namespace NET.XDraw.Models.Structure
{
    public class XLayer : Notifier
    {
        private string _title = "Untitled";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private double _offsetX = 0;
        public double OffsetX
        {
            get => _offsetX;
            set => SetProperty(ref _offsetX, value);
        }

        private double _offsetY = 0;
        public double OffsetY
        {
            get => _offsetY;
            set => SetProperty(ref _offsetY, value);
        }

        private ObservableCollection<XShape> _shapes = new ObservableCollection<XShape>();
        public ObservableCollection<XShape> Shapes
        {
            get => _shapes;
        }
    }
}
