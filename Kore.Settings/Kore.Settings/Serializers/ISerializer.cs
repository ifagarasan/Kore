using System;
using System.IO;
using System.Xml.Serialization;
using Kore.IO.Util;

namespace Kore.Settings.Serializers
{
    public interface ISerializer<T>
    {
        void Serialize(T data, Stream stream);
        T Deserialize(Stream stream);
    }
}