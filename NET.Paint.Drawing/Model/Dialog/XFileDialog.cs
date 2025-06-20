using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace NET.Paint.Drawing.Model.Dialog
{
    public class XFileDialog : PropertyNotifier
    {
        // Observable collections for directories, files, and file extensions
        public ObservableCollection<string> Directories { get; set; }
        public ObservableCollection<string> Files { get; set; }
        public ObservableCollection<string> FileExtensions { get; set; }

        private string _currentPath;
        public string CurrentPath
        {
            get => _currentPath;
            set
            {
                if (SetProperty(ref _currentPath, value))
                {
                    LoadFilesAndDirectories();
                    OnCurrentPathChanged();
                }
            }
        }

        private string _selectedDirectory;
        public string SelectedDirectory
        {
            get => _selectedDirectory;
            set
            {
                if (SetProperty(ref _selectedDirectory, value))
                {
                    // Handle logic when the selected directory changes
                    OnSelectedDirectoryChanged();
                }
            }
        }

        private string _fileName;
        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value);
        }

        private string _selectedExtension;
        public string SelectedExtension
        {
            get => _selectedExtension;
            set => SetProperty(ref _selectedExtension, value);
        }

        private string _title = "File Dialog";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        // Commands for navigation and actions
        public ICommand NavigateUpCommand { get; }
        public ICommand OpenItemCommand { get; }
        public ICommand OkCommand { get; }

        // Events for directory and file selection changes
        public event EventHandler<string> CurrentPathChanged;
        public event EventHandler<string> SelectedDirectoryChanged;

        public XFileDialog()
        {
            Directories = new ObservableCollection<string>();
            Files = new ObservableCollection<string>();
            FileExtensions = new ObservableCollection<string> { ".txt", ".png", ".jpg", ".pdf" };

            // Start at the "Drives" level
            CurrentPath = null;

            NavigateUpCommand = new RelayCommand(NavigateUp);
            OpenItemCommand = new RelayCommand(OpenItem);
            OkCommand = new RelayCommand(ExecuteOk);
        }

        private void LoadFilesAndDirectories()
        {
            Directories.Clear();
            Files.Clear();

            try
            {
                if (string.IsNullOrEmpty(CurrentPath))
                {
                    // List all drives when CurrentPath is null
                    foreach (var drive in DriveInfo.GetDrives())
                    {
                        Directories.Add(drive.Name);
                    }
                }
                else
                {
                    // List directories and files in the current path
                    foreach (var dir in Directory.GetDirectories(CurrentPath))
                    {
                        Directories.Add(dir);
                    }

                    foreach (var file in Directory.GetFiles(CurrentPath))
                    {
                        Files.Add(file);
                    }
                }
            }
            catch (IOException ex)
            {
                // Handle exceptions (e.g., access denied)
            }
        }

        private void NavigateUp()
        {
            if (string.IsNullOrEmpty(CurrentPath))
            {
                // Already at the "Drives" level, do nothing
                return;
            }

            var parentDir = Directory.GetParent(CurrentPath);
            if (parentDir != null)
            {
                CurrentPath = parentDir.FullName;
            }
            else
            {
                // Navigate to the "Drives" level
                CurrentPath = null;
            }
        }

        private void OpenItem()
        {
            if (Directory.Exists(SelectedDirectory))
            {
                CurrentPath = SelectedDirectory;
            }
            else if (File.Exists(SelectedDirectory))
            {
                // Handle file opening logic
            }
        }

        private void ExecuteOk()
        {
            // Logic for OK button (e.g., confirm file selection or save file)
            if (!string.IsNullOrEmpty(FileName) && !string.IsNullOrEmpty(SelectedExtension))
            {
                string fullPath = Path.Combine(CurrentPath, FileName + SelectedExtension);
                // Handle file creation or selection logic
            }
        }

        protected virtual void OnCurrentPathChanged()
        {
            CurrentPathChanged?.Invoke(this, CurrentPath);
        }

        protected virtual void OnSelectedDirectoryChanged()
        {
            SelectedDirectoryChanged?.Invoke(this, SelectedDirectory);
        }
    }
}
