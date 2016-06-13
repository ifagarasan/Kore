using System.Collections.Generic;

namespace Kore.IO.Sync
{
    public class FolderDiff : IFolderDiff
    {
        public FolderDiff(List<IDiff> diffs)
        {
            Diffs = diffs;
        }

        public IReadOnlyList<IDiff> Diffs { get; set; }
    }
}