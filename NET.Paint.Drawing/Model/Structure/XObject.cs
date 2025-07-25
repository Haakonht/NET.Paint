using MessagePack;
using NET.Paint.Drawing.Mvvm;
using System.ComponentModel;

namespace NET.Paint.Drawing.Model.Structure
{
    [MessagePackObject]
    public class XObject : PropertyNotifier
    {
        [Key(0)]
        [DisplayName("Identifier")]
        public Guid Id => _id;
        private Guid _id = Guid.NewGuid();
    }
}
