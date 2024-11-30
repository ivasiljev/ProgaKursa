using System.Xml.Serialization;

namespace KursaVasiljev.Serialization
{
    internal class MyXmlSerializer : MySerializer
    {


        public override Task<T> Read<T>(string path)
        {
            var mySerializer = new XmlSerializer(typeof(T));
            using var myFileStream = new FileStream(path, FileMode.Open);
            return Task.FromResult((T)mySerializer.Deserialize(myFileStream));
        }

        public override Task Write<T>(T data, string path)
        { 
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            StreamWriter myWriter = new StreamWriter(path);
            mySerializer.Serialize(myWriter, data);
            myWriter.Close();
            return Task.CompletedTask;
        }
    }
}
