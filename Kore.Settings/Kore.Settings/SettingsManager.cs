using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kore.Settings.Serializers;
using System.IO;
using System.Runtime.Serialization;
using Kore.IO.Exceptions;
using Kore.IO.Util;
using static Kore.Validation.ObjectValidation;

namespace Kore.Settings
{
    public class SettingsManager<T> : ISettingsManager<T>
    {
        public T Data { get; set; }
        private readonly ISerializer<T> _serializer;


        public SettingsManager(ISerializer<T> serializer)
        {
            _serializer = serializer;
        }

        public void Write(IKoreFileInfo fileInfo)
        {
            IsNotNull(fileInfo, nameof(fileInfo));

            if (Data == null)
                throw new SerializationException();

            using (var stream = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate))
            {
                _serializer.Serialize(Data, stream);
            }
        }

        public void Read(IKoreFileInfo fileInfo)
        {
            IsNotNull(fileInfo, nameof(fileInfo));

            if (!fileInfo.Exists)
                throw new NodeNotFoundException();

            using (var stream = new FileStream(fileInfo.FullName, FileMode.Open))
            {
                Data = _serializer.Deserialize(stream);
            }
        }
    }
}
