using System.IO;

namespace Kore.IO.Util
{
    public class KoreFileInfo : IKoreFileInfo
    {
        FileInfo _fileInfo;

        public KoreFileInfo(string file)
        {
            _fileInfo = new FileInfo(file);
        }

        public bool Hidden
        {
            get
            {
                return _fileInfo.Attributes.HasFlag(FileAttributes.Hidden);
            }
        }
    }
}