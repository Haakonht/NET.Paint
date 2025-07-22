using NET.Paint.Drawing.Helper;
using NET.Paint.Drawing.Model.Structure;
using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Media;

namespace NET.Paint.ViewModels.Drawing.Structure
{
    public class ProjectViewModel : PropertyNotifier
    {
        public required XProject Model { get; set; }

        public string Title
        {
            get => Model.Title;
            set => SetProperty(ref Model.Title, value);
        }

        public string Description
        {
            get => Model.Description;
            set => SetProperty(ref Model.Description, value);
        }

        public string Author
        {
            get => Model.Author;
            set => SetProperty(ref Model.Author, value);
        }

        public DateTime Created
        {
            get => Model.Created;
            set => SetProperty(ref Model.Created, value);
        }

        public DateTime Changed
        {
            get => Model.Changed;
            set => SetProperty(ref Model.Changed, value);
        }

        private ObservableCollection<ImageViewModel> _images;
        public ObservableCollection<ImageViewModel> Images
        {
            get
            {
                if (_images == null)
                    _images = new ObservableCollection<ImageViewModel>(Model.Images.Select(x => new ImageViewModel { Model = x }));
                return _images;
            }
            set => SetProperty(ref _images, value);
        }

        private ObservableCollection<ImageSource> _bitmaps;
        public ObservableCollection<ImageSource> Bitmaps     
        {
            get
            {
                if (_bitmaps == null)
                    _bitmaps = new ObservableCollection<ImageSource>(Model.BitmapsBase64.Select(x => XHelper.Base64ToImageSource(x)));
                return _bitmaps;
            }
            set
            {
                Model.BitmapsBase64.Clear();
                foreach (var b64 in value)
                    Model.BitmapsBase64.Add(XHelper.ImageSourceToBase64(b64));
            }
        }
    }
}
