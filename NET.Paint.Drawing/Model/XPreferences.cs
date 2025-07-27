using NET.Paint.Drawing.Mvvm;

namespace NET.Paint.Drawing.Model
{
    public class XPreferences : PropertyNotifier
    {
        #region Dialogs

        private bool _imageDialogVisible = false;
        public bool ImageDialogVisible
        {
            get => _imageDialogVisible;
            set => SetProperty(ref _imageDialogVisible, value);
        }

        private bool _projectDialogVisible = false;
        public bool ProjectDialogVisible
        {
            get => _projectDialogVisible;
            set => SetProperty(ref _projectDialogVisible, value);
        }

        private bool _preferencesVisible = false;
        public bool PreferencesDialogVisible
        {
            get => _preferencesVisible;
            set => SetProperty(ref _preferencesVisible, value);
        }

        #endregion

        #region Anchorables

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

        #endregion
    }
}
