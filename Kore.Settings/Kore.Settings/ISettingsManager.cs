using Kore.IO.Util;

namespace Kore.Settings
{
    public interface ISettingsManager<T> where T : new()
    {
        T Data { get; set; }

        void Read(IKoreFileInfo fileInfo);
        void Write(IKoreFileInfo fileInfo);
    }
}