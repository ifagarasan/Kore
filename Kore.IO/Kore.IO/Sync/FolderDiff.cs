﻿using System.Collections.Generic;

namespace Kore.IO.Sync
{
    public class FolderDiff
    {
        public FolderDiff(List<Diff> diffs)
        {
            Diffs = diffs;
        }

        public IReadOnlyList<Diff> Diffs { get; set; }
    }
}