using System.IO;

namespace Kore.Test.Util
{
    public static class IoUtil
    {
        public static void EnsureFolderExits(string folder)
        {
            if (Directory.Exists((folder)))
                return;

            Directory.CreateDirectory(folder);
        }

        public static void EnsureFileExits(string file)
        {
            if (File.Exists((file)))
                return;

            using (var fs = File.Create(file)) { }
        }
    }
}
