using System.Text.Json;

namespace KursaVasiljev.Serialization
{
    internal class MyJsonSerializer : MySerializer
    {
        private readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            Converters = { new Array2DConverter() },
            WriteIndented = true,
        };

        public override async Task<T> Read<T>(string path)
        {
            using var myFileStream = new FileStream(path, FileMode.Open);
            return await JsonSerializer.DeserializeAsync<T>(myFileStream, _options);
        }

        public override async Task Write<T>(T data, string path)
        {
            await using FileStream createStream = File.Create(path);
            await JsonSerializer.SerializeAsync(createStream, data, _options);
        }
    }
}
