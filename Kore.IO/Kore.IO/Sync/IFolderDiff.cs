using System.Collections.Generic;

namespace Kore.IO.Sync
{
    public interface IFolderDiff
    {
        IReadOnlyList<IDiff> Diffs { get; set; }
    }
}