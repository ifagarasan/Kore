using System.Collections.Generic;
using System.Linq;
using Kore.IO.Retrievers;
using static Kore.Validation.ObjectValidation;

namespace Kore.IO.Scanners
{
    public class FileScanner : IFileScanner
    {
        private readonly IFileRetriever _fileRetriever;

        public event FileFoundDelegate FileFound;

        public FileScanner(IFileRetriever fileRetriever)
        {
            _fileRetriever = fileRetriever;
        }

        public IFileScanResult Scan(IKoreFolderInfo folder)
        {
            return Scan(folder, new FileScanOptions());
        }

        public IFileScanResult Scan(IKoreFolderInfo folder, FileScanOptions options)
        {
            IsNotNull(options);

            var files = new List<IKoreFileInfo>();

            var eventDelegate = new FileFoundDelegate(file =>
            {
                var filtered = options.Filters.Aggregate(new List<IKoreFileInfo> {file}, (current, filter) => filter.Filter(current));
                if (filtered.Count > 0)
                {
                    OnFileFound(file);
                    files.Add(file);
                }
            });

            _fileRetriever.FileFound += eventDelegate;

            _fileRetriever.GetFiles(folder);

            _fileRetriever.FileFound -= eventDelegate;

            return new FileScanResult(folder, files);
        }

        protected virtual void OnFileFound(IKoreFileInfo file)
        {
            FileFound?.Invoke(file);
        }
    }
}