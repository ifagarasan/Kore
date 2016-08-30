using System.Collections.Generic;

namespace Kore.IO.Scanners
{
    public interface IFileScanResult
    {
        IKoreFolderInfo Folder { get; }
        IList<IKoreFileInfo> Files { get; }
    }
}