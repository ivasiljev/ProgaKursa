using MessagePack;

namespace KursaVasiljev.Serialization
{
    /// <summary>
    /// MessagePack library used for binary serialization
    /// </summary>
    internal class MyBinarySerializer : MySerializer
    {
        public override async Task<T> Read<T>(string path)
        {
            using var myFileStream = new FileStream(path, FileMode.Open);
            var result = await MessagePackSerializer.Typeless.DeserializeAsync(myFileStream);
            return (T)result;
        }

        public override async Task Write<T>(T data, string path)
        {
            await using FileStream createStream = File.Create(path);
            await MessagePackSerializer.Typeless.SerializeAsync(createStream, data);
        }
    }
}
