using System.Collections.Generic;

namespace Kore.IO.Scanners
{
    public interface IFileScanResult
    {
        IKoreFolderInfo Folder { get; }
        IReadOnlyList<IKoreFileInfo> Files { get; }
    }
}