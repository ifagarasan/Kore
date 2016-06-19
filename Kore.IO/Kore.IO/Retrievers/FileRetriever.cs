using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kore.IO.Util;

namespace Kore.IO.Retrievers
{
    public class FileRetriever : IFileRetriever
    {
        public List<IKoreFileInfo> GetFiles(string folder, string searchPattern)
        {
            return Directory.EnumerateFiles(folder, searchPattern, SearchOption.AllDirectories).
                Select(BuildFileInfo).ToList();
        }

        private IKoreFileInfo BuildFileInfo(string file)
        {
            return new KoreFileInfo(file);
        }
    }
}