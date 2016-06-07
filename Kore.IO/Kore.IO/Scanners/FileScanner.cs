﻿using System;
using System.Collections.Generic;
using System.Linq;
using Kore.IO.Retrievers;
using Kore.IO.Filters;
using Kore.IO.Util;

namespace Kore.IO.Scanners
{
    public class FileScanner
    {
        private readonly IFileRetriever _fileRetriever;

        public FileScanner(IFileRetriever fileRetriever)
        {
            _fileRetriever = fileRetriever;
        }

        public FileScanResult Scan(string folder)
        {
            return Scan(folder, new FileScanOptions());
        }

        public FileScanResult Scan(string folder, FileScanOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            List<IKoreFileInfo> files = _fileRetriever.GetFiles(folder, options.SearchPattern);

            return new FileScanResult(folder, options.Filters.Aggregate(files, (current, filter) => filter.Filter(current)));
        }
    }
}