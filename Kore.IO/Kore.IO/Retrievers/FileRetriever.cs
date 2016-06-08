using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kore.IO.Util;

namespace Kore.IO.Retrievers
{
    public class FileRetriever : IFileRetriever
    {
        private readonly IFileInfoProvider _fileInfoProvider;

        public FileRetriever(IFileInfoProvider fileInfoProvider)
        {
            _fileInfoProvider = fileInfoProvider;
        }

        public List<IKoreFileInfo> GetFiles(string folder, string searchPattern)
        {
            return Directory.EnumerateFiles(folder, searchPattern, SearchOption.AllDirectories).
                Select(file => _fileInfoProvider.CreateFileInfo(file)).ToList();
        }
    }
}