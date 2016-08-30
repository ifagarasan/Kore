using System.Collections.Generic;

namespace Kore.IO.Sync
{
    public interface IFolderDiff
    {
        IList<IDiff> Diffs { get; }
        IKoreFolderInfo Source { get; }
        IKoreFolderInfo Destination { get; }
    }
}