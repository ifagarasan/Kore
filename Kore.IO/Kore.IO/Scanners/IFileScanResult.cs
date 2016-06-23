using System.Collections.Generic;

namespace Kore.IO.Scanners
{
    public interface IFileScanResult
    {
        string Folder { get; }
        IReadOnlyList<IKoreFileInfo> Files { get; }
    }
}