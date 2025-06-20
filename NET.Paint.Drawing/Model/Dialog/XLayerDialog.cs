using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Dialog
{
    public class XLayerDialog : PropertyNotifier
    {
        private string _title = "";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
    }
}

