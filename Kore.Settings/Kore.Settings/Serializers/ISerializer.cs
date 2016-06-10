using System;
using System.IO;
using System.Xml.Serialization;

namespace Kore.Settings.Serializers
{
    public interface ISerializer<T> where T: new()
    {
        void Serialize(T data, Stream stream);
        T Deserialize(Stream stream);
    }
}