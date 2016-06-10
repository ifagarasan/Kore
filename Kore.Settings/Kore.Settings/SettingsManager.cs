using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kore.Settings.Serializers;
using System.IO;
using Kore.IO.Util;

namespace Kore.Settings
{
    public class SettingsManager<T> : ISettingsManager<T> where T: new()
    {
        public T Data { get; set; }
        private readonly ISerializer<T> _serializer;

        public SettingsManager(T data, ISerializer<T> serializer)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            Data = data;
            _serializer = serializer;
        }

        public SettingsManager(ISerializer<T> serializer): this(Activator.CreateInstance<T>(), serializer)
        {
        }

        public void Write(IKoreFileInfo fileInfo)
        {
            if (fileInfo == null)
                throw new ArgumentNullException(nameof(fileInfo));

            using (FileStream stream = new FileStream(fileInfo.FullName, FileMode.OpenOrCreate))
            {
                _serializer.Serialize(Data, stream);
            }
        }

        public void Read(IKoreFileInfo fileInfo)
        {
            if (fileInfo == null)
                throw new ArgumentNullException(nameof(fileInfo));

            if (!fileInfo.Exists)
                return;

            using (FileStream stream = new FileStream(fileInfo.FullName, FileMode.Open))
            {
                Data = _serializer.Deserialize(stream);
            }
        }
    }
}
