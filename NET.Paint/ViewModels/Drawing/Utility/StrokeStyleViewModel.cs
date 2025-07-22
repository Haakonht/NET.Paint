using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace NET.Paint.ViewModels.Drawing.Utility
{
    public class StrokeStyleViewModel
    {
        public string Name { get; set; }
        public DoubleCollection DashArray { get; set; }
    }
}
