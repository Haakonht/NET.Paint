using NET.Paint.Drawing.Mvvm;

namespace NET.Paint.ViewModels.Drawing
{
    public class PreferencesViewModel : PropertyNotifier
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

        private bool _preferencesVisible = false;
        public bool PreferencesVisible
        {
            get => _preferencesVisible;
            set => SetProperty(ref _preferencesVisible, value);
        }

        private bool _clipboardVisible = false;
        public bool ClipboardVisible
        {
            get => _clipboardVisible;
            set => SetProperty(ref _clipboardVisible, value);
        }

        private bool _undoVisible = false;
        public bool UndoVisible
        {
            get => _undoVisible;
            set => SetProperty(ref _undoVisible, value);
        }

    }
}
