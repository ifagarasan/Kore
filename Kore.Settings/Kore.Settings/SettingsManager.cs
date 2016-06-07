using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kore.Settings.Serializers;
using System.IO;

namespace Kore.Settings
{
    public class SettingsManager<T> where T : new()
    {
        public T Data { get; set; }
        private ISerializer<T> _serializer;

        public SettingsManager(T data, ISerializer<T> serializer)
        {
            this.Data = data;
            this._serializer = serializer;
        }

        public SettingsManager(ISerializer<T> serializer): this(new T(), serializer)
        {
        }

        public void Write(string file)
        {
            using (FileStream stream = new FileStream(file, FileMode.OpenOrCreate))
            {
                _serializer.Serialize(Data, stream);
            }
        }

        public void Read(string file)
        {
            using (FileStream stream = new FileStream(file, FileMode.Open))
            {
                Data = _serializer.Deserialize(stream);
            }
        }
    }
}
