using System.Collections.ObjectModel;
using NET.Paint.Drawing.Mvvm;

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

    }
}
