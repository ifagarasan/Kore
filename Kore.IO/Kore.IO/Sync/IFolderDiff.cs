using System.Collections.Generic;

namespace Kore.IO.Sync
{
    public interface IFolderDiff
    {
        IReadOnlyList<Diff> Diffs { get; set; }
    }
}