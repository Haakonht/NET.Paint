using System.ComponentModel;
using System.Windows.Controls;

namespace NET.Paint.View.Component.Editor
{
    /// <summary>
    /// Interaction logic for ToolContextSlim.xaml
    /// </summary>
    public partial class EditorContextMenu : UserControl, INotifyPropertyChanged
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

        public EditorContextMenu()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
