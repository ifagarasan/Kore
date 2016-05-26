using System.Collections.Generic;
using Kore.IO.Retrievers;

namespace Kore.IO.Scanners
{
    public class FileScanner
    {
        private readonly IFileRetriever _fileRetriever;

        public FileScanner(IFileRetriever fileRetriever)
        {
            _fileRetriever = fileRetriever;
        }

        public List<string> Scan(string folder)
        {
            return _fileRetriever.GetFiles(folder);
        }
    }
}