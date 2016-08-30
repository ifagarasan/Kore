using System;
using System.Collections.Generic;
using Kore.Validation;

namespace Kore.IO.Sync
{
    public class FolderDiff : IFolderDiff
    {
        public FolderDiff(IKoreFolderInfo source, IKoreFolderInfo destination, List<IDiff> diffs)
        {
            ObjectValidation.IsNotNull(source);
            ObjectValidation.IsNotNull(destination);
            ObjectValidation.IsNotNull(diffs);

            Source = source;
            Destination = destination;
            Diffs = diffs.AsReadOnly();
        }

        public IKoreFolderInfo Source { get; }
        public IKoreFolderInfo Destination { get; }

        public IList<IDiff> Diffs { get; set; }
    }
}