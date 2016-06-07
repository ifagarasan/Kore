using System.Collections.Generic;
using Kore.IO.Util;

namespace Kore.IO.Scanners
{
    public interface IFileScanResult
    {
        string Folder { get; }
        IReadOnlyList<IKoreFileInfo> Files { get; }
    }
}