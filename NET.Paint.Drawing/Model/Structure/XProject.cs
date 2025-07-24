using MessagePack;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace NET.Paint.Drawing.Model.Structure
{
    [MessagePackObject]
    public class XProject : XObject
    {
        [Key(1)]
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }
        private string _title = "Untitled Project";

        [Key(2)]
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }
        private string _description = "";

        [Key(3)]
        public string Author
        {
            get => _author;
            set => SetProperty(ref _author, value);
        }
        private string _author = "";

        [Key(4)]
        public DateTime Created
        {
            get => _created;
            set => SetProperty(ref _created, value);
        }
        private DateTime _created = DateTime.Now;

        [Key(5)]
        public DateTime Changed
        {
            get => _changed;
            set => SetProperty(ref _changed, value);
        }
        private DateTime _changed = DateTime.Now;

        [Key(6)]
        public ObservableCollection<XImage> Images
        {
            get => _images;
            set => SetProperty(ref _images, value);
        }
        private ObservableCollection<XImage> _images = new ObservableCollection<XImage>();

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Browsable(false)]
        public ObservableCollection<ImageSource> Bitmaps 
        {
            get => _bitmaps;
            set => SetProperty(ref _bitmaps, value);
        } 
        private ObservableCollection<ImageSource> _bitmaps = new ObservableCollection<ImageSource>();

        #endregion
    }
}
