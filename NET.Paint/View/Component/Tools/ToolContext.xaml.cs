using System.ComponentModel;
using System.Windows.Controls;

namespace NET.Paint.View.Component.Tools
{
    /// <summary>
    /// Interaction logic for ToolContextSlim.xaml
    /// </summary>
    public partial class ToolContext : UserControl, INotifyPropertyChanged
    {
        private int _contextIndex = 0;
        public int ContextIndex
        {
            get => _contextIndex;
            set
            {
                if (_contextIndex != value)
                {
                    _contextIndex = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContextIndex)));
                }
            }
        }

        public ToolContext()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
