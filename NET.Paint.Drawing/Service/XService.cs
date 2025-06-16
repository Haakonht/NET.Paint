using NET.Paint.Drawing.Command;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;
using System.Security.RightsManagement;

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

        public XClipboard Clipboard { get; } = XClipboard.Instance;

        public XPreferences Preferences { get; } = new XPreferences();

        public XCommand Command { get; }

        private XImage? _activeImage = null;
        public XImage? ActiveImage
        {
            get => _activeImage;
            set => SetProperty(ref _activeImage, value);
        }

        public XService() => Command = new XCommand(this);
    }
}
