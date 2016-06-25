using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kore.IO.Retrievers
{
    public class FileRetriever : IFileRetriever
    {
        public List<IKoreFileInfo> GetFiles(IKoreFolderInfo folder)
        {
            return Directory.EnumerateFiles(folder.FullName, "*", SearchOption.AllDirectories).
                Select(BuildFileInfo).ToList();
        }

        private static IKoreFileInfo BuildFileInfo(string file)
        {
            return new KoreFileInfo(file);
        }
    }
}