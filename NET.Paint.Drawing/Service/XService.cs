using NET.Paint.Drawing.Command;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;

namespace NET.Paint.Drawing.Service
{
    public class XService : PropertyNotifier
    {
        private XProject _project = new XProject
        {
            Title = "Test Project",
            Description = "This is a test project",
            Author = "Håkon Torgersen",
            Images = new ObservableCollection<XImage>{ new XImage { Title = "Test 1" }, new XImage { Title = "Test 2" } },
            Created = DateTime.Now
        };
        public XProject Project
        {
            get => _project;
            set => SetProperty(ref _project, value);
        }

        private XImage? _activeImage = null;
        public XImage? ActiveImage
        {
            get => _activeImage;
            set => SetProperty(ref _activeImage, value);
        }

        private XClipboard _clipboard = new XClipboard();
        public XClipboard Clipboard
        {
            get => _clipboard;
            set => SetProperty(ref _clipboard, value);
        }

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
    }
}
