using MessagePack;
using NET.Paint.Drawing.Constant;
using NET.Paint.Drawing.Model.Structure;
using System.ComponentModel;

namespace NET.Paint.Drawing.Model.Diagram
{
    [MessagePackObject]
    public class XConnection : XStrokedRenderable
    {
        [Key(1)]
        public override XToolType Type => XToolType.Connection;

        [Key(6)]
        public KeyValuePair<XRenderable, XRenderable> Connected
        {
            get => _connected;
            set => SetProperty(ref _connected, value);
        }
        private KeyValuePair<XRenderable, XRenderable> _connected;

        [Key(7)]
        public XConnectionType Start { get; set; } = XConnectionType.None;

        [Key(8)]
        public XConnectionType End { get; set; } = XConnectionType.None;

        [Key(9)]
        public string Label { get; set; } = string.Empty;

        #region Volatile - Not Serialized

        [IgnoreMember]
        [Browsable(false)]
        public bool IsDirect => Points.Any();
        public override object Clone() => throw new NotImplementedException();
        
        #endregion
    }
}
