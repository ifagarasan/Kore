using System;
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
            return Scan(folder, new FileScanOptions());
        }

        public List<string> Scan(string folder, FileScanOptions options)
        {
            if (options == null)
                throw new ArgumentNullException("options");

            return _fileRetriever.GetFiles(folder, options.SearchPattern);
        }
    }
}