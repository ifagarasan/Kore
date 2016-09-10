using System.Collections.Generic;

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
        public IList<IKoreFileInfo> Files { get; }
    }
}