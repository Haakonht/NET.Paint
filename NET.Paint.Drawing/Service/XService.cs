using NET.Paint.Drawing.Command;
using NET.Paint.Drawing.Helper;
using NET.Paint.Drawing.Model;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Model.Utility;
using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Media;

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

        public XClipboard Clipboard { get; } = XClipboard.Instance;
        public XTools Tools { get; } = XTools.Instance;
        public XPreferences Preferences { get; } = new XPreferences();
        public XCommand Command { get; }

        //public XService()
        //{
        //    Command = new XCommand(this);
        //    ActiveImage = Project.Images.FirstOrDefault();
        //}

        public XService()
        {
            // Create sample layers and shapes
            var sampleLayer1 = new XHybridLayer { Title = "Layer 1" };

            // Create sample images with layers
            var sampleImage1 = new XImage
            {
                Title = "Sample Image 1",
                Width = 800,
                Height = 600,
                Background = Colors.White,
                ActiveLayer = sampleLayer1
            };
            sampleImage1.Layers.Add(sampleLayer1);

            var sampleImage2 = new XImage
            {
                Title = "Sample Image 2",
                Width = 1400,
                Height = 1000,
                Background = Colors.White
            };

            // Set up the project with images
            Project = new XProject
            {
                Title = "Design-Time Project",
                Description = "Sample project for design-time preview",
                Author = "Designer",
                Images = new ObservableCollection<XImage> { sampleImage1, sampleImage2 }
            };

            // Add some random bitmaps
            for (int i = 0; i < 5; i++)
                Project.Bitmaps.Add(XHelper.CreateRandomBitmap(200, 200));

            // Set the active image and layer
            ActiveImage = sampleImage1;

            // Set preferences if needed for visibility
            Preferences = new XPreferences
            {
                OverviewVisible = true,
                ToolboxVisible = true
            };

            // Initialize the command object
            Command = new XCommand(this);
        }
    }
}
