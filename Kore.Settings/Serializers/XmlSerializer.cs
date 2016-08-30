using System.IO;
using System.Runtime.Serialization;
using System.Xml;

namespace Kore.Settings.Serializers
{
    public class XmlSerializer<T> : ISerializer<T>
    {
        private readonly DataContractResolver _contractResolver;
        private readonly DataContractSerializer _serializer;

        public XmlSerializer(DataContractResolver contractResolver=null)
        {
            _contractResolver = contractResolver ?? new IdentityContractResolver();
            _serializer = new DataContractSerializer(typeof(T));
        }

        public T Deserialize(Stream stream)
        {
            return (T)_serializer.ReadObject(XmlDictionaryReader.CreateDictionaryReader(XmlReader.Create(stream)), false, _contractResolver);
        }

        public void Serialize(T data, Stream stream)
        {
            using (var xmlWriter = XmlWriter.Create(stream))
            {
                _serializer.WriteObject(XmlDictionaryWriter.CreateDictionaryWriter(xmlWriter), data, _contractResolver);
                xmlWriter.Flush();
            }
        }
    }
}