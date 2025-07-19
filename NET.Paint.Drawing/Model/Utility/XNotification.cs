using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Paint.Drawing.Model.Utility
{
    public class XNotification : PropertyNotifier
    {
        public XNotificationSource Source { get; init; }
        public string Message { get; init; }
    }
}
