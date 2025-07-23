using MessagePack;
using MessagePack.Resolvers;
using NET.Paint.Drawing.Model.Structure;
using System.IO;

namespace NET.Paint.Drawing.Helper
{
    public static class XStorageService
    {
        private static readonly MessagePackSerializerOptions Options =
            MessagePackSerializerOptions.Standard
                .WithResolver(CompositeResolver.Create(
                    StandardResolver.Instance
                ));

        public static async Task SaveProjectAsync(XProject project, string filePath)
        {
            var data = MessagePackSerializer.Serialize(project, Options);
            await File.WriteAllBytesAsync(filePath, data);
        }

        public static async Task<XProject> LoadProjectAsync(string filePath)
        {
            var data = await File.ReadAllBytesAsync(filePath);
            return MessagePackSerializer.Deserialize<XProject>(data, Options);
        }

        public static void SaveProject(XProject project, string filePath)
        {
            var data = MessagePackSerializer.Serialize(project, Options);
            File.WriteAllBytes(filePath, data);
        }

        public static XProject LoadProject(string filePath)
        {
            var data = File.ReadAllBytes(filePath);
            return MessagePackSerializer.Deserialize<XProject>(data, Options);
        }
    }
}
