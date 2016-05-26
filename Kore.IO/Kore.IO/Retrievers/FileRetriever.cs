using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Kore.IO.Retrievers
{
    public class FileRetriever : IFileRetriever
    {
        public List<string> GetFiles(string folder)
        {
            return Directory.EnumerateFiles(folder, "*", SearchOption.AllDirectories).ToList();
        }
    }
}