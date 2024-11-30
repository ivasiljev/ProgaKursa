using KursaVasiljev.Models;

namespace KursaVasiljev.Serialization
{
    internal abstract class MySerializer
    {
        public abstract Task<T> Read<T>(string path);
        public abstract Task Write<T>(T data, string path);
    }
}
