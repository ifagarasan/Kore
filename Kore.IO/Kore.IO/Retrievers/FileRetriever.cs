using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kore.IO.Retrievers
{
    public class FileRetriever : IFileRetriever
    {
        public List<string> GetFiles(string folder, string searchPattern)
        {
            return Directory.EnumerateFiles(folder, searchPattern, SearchOption.AllDirectories).ToList();
        }
    }
}