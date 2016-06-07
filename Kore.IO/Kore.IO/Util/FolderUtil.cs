using System.IO;

namespace Kore.IO.Util
{
    public static class FolderUtil
    {
        //TODO: to be removed and included in a future IKoreDirectoryInfo
        public static void EnsureExits(string folder)
        {
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);
        }
    }
}
