using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Kore.Settings.Serializers
{
    public class XmlSerializer<T> : ISerializer<T>
    {
        private readonly XmlSerializer _serializer;

        public XmlSerializer()
        {
            _serializer = new XmlSerializer(typeof(T));
        }

        public T Deserialize(Stream stream)
        {
            using (var xmlReader = XmlReader.Create(stream))
                return (T)_serializer.Deserialize(xmlReader);
        }

        public void Serialize(T data, Stream stream)
        {
            using (var xmlWriter = XmlWriter.Create(stream))
                _serializer.Serialize(xmlWriter, data);
        }
    }
}