using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Kore.IO.Scanners
{
    public class FileScanResult: IFileScanResult
    {
        public FileScanResult(string folder, List<IKoreFileInfo> files)
        {
            Folder = new DirectoryInfo(folder).FullName; //TODO: IKoreDirectoryInfo
            Files = files.AsReadOnly();
        }

        public string Folder { get; }
        public IReadOnlyList<IKoreFileInfo> Files { get; }
    }
}