using System.IO;

namespace Kore.IO.Util{
    public class KoreFolderInfo : IKoreFolderInfo
    {
        public KoreFolderInfo(string folder)
        {
            FullName = folder;
        }

        public string FullName { get; }

        public bool Exists => Directory.Exists(FullName);

        public void EnsureExists()
        {
            if (Directory.Exists(FullName))
                return;

            Directory.CreateDirectory(FullName);
        }
    }
}