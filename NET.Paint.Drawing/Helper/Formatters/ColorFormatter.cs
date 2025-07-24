using MessagePack;
using MessagePack.Formatters;
using System.Windows.Media;

namespace NET.Paint.Drawing.Helper
{
    public class ColorFormatter : IMessagePackFormatter<Color>
    {
        public void Serialize(ref MessagePackWriter writer, Color value, MessagePackSerializerOptions options)
        {
            writer.WriteArrayHeader(4);
            writer.Write(value.A);
            writer.Write(value.R);
            writer.Write(value.G);
            writer.Write(value.B);
        }

        public Color Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var count = reader.ReadArrayHeader();
            if (count != 4)
                throw new MessagePackSerializationException("Invalid Color data");

            var a = reader.ReadByte();
            var r = reader.ReadByte();
            var g = reader.ReadByte();
            var b = reader.ReadByte();

            return Color.FromArgb(a, r, g, b);
        }
    }
}