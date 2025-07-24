using MessagePack;
using MessagePack.Formatters;
using System.Windows.Media;

namespace NET.Paint.Drawing.Helper
{
    public class DoubleCollectionFormatter : IMessagePackFormatter<DoubleCollection>
    {
        public void Serialize(ref MessagePackWriter writer, DoubleCollection value, MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }

            writer.WriteArrayHeader(value.Count);
            foreach (var item in value)
            {
                writer.Write(item);
            }
        }

        public DoubleCollection Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
                return null;

            var count = reader.ReadArrayHeader();
            var collection = new DoubleCollection();

            for (int i = 0; i < count; i++)
            {
                collection.Add(reader.ReadDouble());
            }

            return collection;
        }
    }
}