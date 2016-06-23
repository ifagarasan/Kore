using System.IO;

namespace Kore.Settings.Serializers
{
    public interface ISerializer<T>
    {
        void Serialize(T data, Stream stream);
        T Deserialize(Stream stream);
    }
}