namespace NET.Paint.Drawing.Model.Structure
{
    public class XProject
    {
        public string Title = "Untitled Project";
        public string Description = "";
        public string Author = "";
        public DateTime Created = DateTime.Now;
        public DateTime Changed = DateTime.Now;
        public List<XImage> Images = new List<XImage>();
        public List<string> BitmapsBase64 = new List<string>();
    }
}
