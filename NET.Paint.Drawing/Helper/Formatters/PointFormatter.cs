using MessagePack;
using MessagePack.Formatters;
using System.Windows;

namespace NET.Paint.Drawing.Helper
{
    public class PointFormatter : IMessagePackFormatter<Point>
    {
        public void Serialize(ref MessagePackWriter writer, Point value, MessagePackSerializerOptions options)
        {
            writer.WriteArrayHeader(2);
            writer.Write(value.X);
            writer.Write(value.Y);
        }

        public Point Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var count = reader.ReadArrayHeader();
            if (count != 2)
                throw new MessagePackSerializationException("Invalid Point data");

            var x = reader.ReadDouble();
            var y = reader.ReadDouble();
            return new Point(x, y);
        }
    }
}