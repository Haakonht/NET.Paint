using NET.Paint.Drawing.Mvvm;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json.Serialization;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NET.Paint.Drawing.Model.Structure
{
    public class XProject : PropertyNotifier
    {
        private string _title = "Untitled Project";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _description = "";
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _author = "";
        public string Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }

        private DateTime _created = DateTime.Now;
        public DateTime Created
        {
            get => _created;
            set => SetProperty(ref _created, value);
        }

        public DateTime _changed = DateTime.Now;
        public DateTime Changed
        {
            get => _changed;
            set => SetProperty(ref _changed, value);
        }

        private ObservableCollection<XImage> _images = new ObservableCollection<XImage>();
        public ObservableCollection<XImage> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }

        [JsonIgnore]
        public ObservableCollection<ImageSource> Bitmaps { get; set; } = new();
        
        public List<string> BitmapBase64
        {
            get => Bitmaps.Select(img => ImageSourceToBase64(img)).ToList();
            set
            {
                Bitmaps.Clear();
                foreach (var b64 in value)
                    Bitmaps.Add(Base64ToImageSource(b64));
                OnPropertyChanged(nameof(BitmapBase64));

            }
        }

        private static string ImageSourceToBase64(ImageSource image)
        {
            if (image is BitmapSource bitmapSource)
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                using var ms = new MemoryStream();
                encoder.Save(ms);
                return Convert.ToBase64String(ms.ToArray());
            }
            return string.Empty;
        }

        private static ImageSource Base64ToImageSource(string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            var image = new BitmapImage();
            using var ms = new MemoryStream(bytes);
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.StreamSource = ms;
            image.EndInit();
            image.Freeze();
            return image;
        }
    }
}
