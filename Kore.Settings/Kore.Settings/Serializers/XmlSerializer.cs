using System;
using System.IO;
using System.Xml.Serialization;

namespace Kore.Settings.Serializers
{
    public class XmlSerializer<T> : ISerializer<T> where T: new()
    {
        readonly XmlSerializer _serializer;

        public XmlSerializer()
        {
            _serializer = new XmlSerializer(typeof(T));
        }

        public T Deserialize(Stream stream)
        {
            return (T)_serializer.Deserialize(stream);
        }

        public void Serialize(T data, Stream stream)
        {
            _serializer.Serialize(stream, data);
        }
    }
}