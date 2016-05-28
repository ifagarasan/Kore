using System.IO;

namespace Kore.IO.Util
{
    public class KoreFileInfo : IKoreFileInfo
    {
        readonly FileInfo _fileInfo;

        public KoreFileInfo(string file)
        {
            _fileInfo = new FileInfo(file);
        }

        public bool Hidden => _fileInfo.Attributes.HasFlag(FileAttributes.Hidden);

        public string FullName => _fileInfo.FullName;

        public bool Exists => _fileInfo.Exists;

        public string DirectoryFullName => _fileInfo.DirectoryName;
    }
}