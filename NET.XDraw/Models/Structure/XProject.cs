using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NET.XDraw.Utility;

namespace NET.XDraw.Models.Structure
{
    public class XProject : Notifier
    {
        private string _title = "Untitled Project";
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        private string _description = "Untitled Project has no description";
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        private string _author = "Håkon Torgersen";
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

        private ObservableCollection<XImage> _images = new ObservableCollection<XImage>() { new XImage { Title = "Test 1" }, new XImage { Title = "Test 2" } };
        public ObservableCollection<XImage> Images => _images;
        public XImage Active { get; set; }
    }
}
