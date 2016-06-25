using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Kore.IO.Scanners
{
    public class FileScanResult: IFileScanResult
    {
        public FileScanResult(IKoreFolderInfo folder, List<IKoreFileInfo> files)
        {
            Folder = folder;
            Files = files.AsReadOnly();
        }

        public IKoreFolderInfo Folder { get; }
        public IReadOnlyList<IKoreFileInfo> Files { get; }
    }
}