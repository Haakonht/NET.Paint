using NET.Paint.Drawing.Mvvm;

namespace NET.Paint.Drawing.Model
{
    public class XPreferences : PropertyNotifier
    {
        private bool _toolboxVisible = true;
        public bool ToolboxVisible
        {
            get => _toolboxVisible;
            set => SetProperty(ref _toolboxVisible, value);
        }

        private bool _overviewVisible = true;
        public bool OverviewVisible
        {
            get => _overviewVisible;
            set => SetProperty(ref _overviewVisible, value);
        }

        private bool _useProjectTree = false;
        public bool UseProjectTree
        {
            get => _useProjectTree;
            set => SetProperty(ref _useProjectTree, value);
        }
    }
}
