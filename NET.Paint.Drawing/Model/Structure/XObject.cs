using MessagePack;
using NET.Paint.Drawing.Mvvm;

namespace NET.Paint.Drawing.Model.Structure
{
    [MessagePackObject]
    public class XObject : PropertyNotifier
    {
        [Key(0)]
        public Guid Id => _id;
        private Guid _id = Guid.NewGuid();
    }
}
