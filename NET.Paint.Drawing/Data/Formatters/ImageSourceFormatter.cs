using MessagePack;
using MessagePack.Formatters;
using System.Buffers;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NET.Paint.Drawing.Persistence.Formatters
{
    public class ImageSourceFormatter : IMessagePackFormatter<ImageSource>
    {
        public void Serialize(ref MessagePackWriter writer, ImageSource value, MessagePackSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNil();
                return;
            }

            byte[] imageData = ImageSourceToByteArray(value);
            if (imageData == null || imageData.Length == 0)
            {
                writer.WriteNil();
                return;
            }

            // MessagePack will handle compression automatically
            writer.Write(imageData);
        }

        public ImageSource Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.TryReadNil())
                return null;

            var imageData = reader.ReadBytes();
            if (imageData == null || imageData.Value.Length == 0)
                return null;

            return ByteArrayToImageSource(imageData.Value.ToArray());
        }

        private byte[] ImageSourceToByteArray(ImageSource imageSource)
        {
            try
            {
                BitmapSource bitmapSource;

                if (imageSource is BitmapSource bitmap)
                {
                    bitmapSource = bitmap;
                }
                else
                {
                    // Convert other ImageSource types to BitmapSource
                    var drawingVisual = new DrawingVisual();
                    using (var drawingContext = drawingVisual.RenderOpen())
                    {
                        drawingContext.DrawImage(imageSource, new Rect(0, 0, imageSource.Width, imageSource.Height));
                    }

                    var renderTargetBitmap = new RenderTargetBitmap(
                        (int)imageSource.Width, (int)imageSource.Height,
                        96, 96, PixelFormats.Pbgra32);
                    renderTargetBitmap.Render(drawingVisual);
                    bitmapSource = renderTargetBitmap;
                }

                // Use PNG for lossless compression
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    return stream.ToArray();
                }
            }
            catch
            {
                return null;
            }
        }

        private ImageSource ByteArrayToImageSource(byte[] imageData)
        {
            try
            {
                using (var stream = new MemoryStream(imageData))
                {
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = stream;
                    bitmapImage.EndInit();
                    bitmapImage.Freeze(); // Important for performance
                    return bitmapImage;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}