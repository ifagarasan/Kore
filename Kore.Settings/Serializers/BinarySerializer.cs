using Kore.Validation;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Kore.Settings.Serializers
{
    public class BinarySerializer<T> : ISerializer<T>
    {
        private readonly BinaryFormatter _binaryFormatter;

        public BinarySerializer()
        {
            _binaryFormatter = new BinaryFormatter();
        }

        public T Deserialize(Stream stream)
        {
            ObjectValidation.IsNotNull(stream);

            return (T)_binaryFormatter.Deserialize(stream);
        }

        public void Serialize(T data, Stream stream)
        {
            ObjectValidation.IsNotNull(data);
            ObjectValidation.IsNotNull(stream);

            _binaryFormatter.Serialize(stream, data);
        }
    }
}