using System;
using System.Collections.Generic;
using System.Linq;
using Kore.IO.Scanners;
using Kore.IO.Util;

namespace Kore.IO.Sync
{
    public class FolderDiffer
    {
        private readonly IFileScanResult _sourceScanResult;
        private readonly IFileScanResult _destinationScanResult;

        public FolderDiffer(IFileScanResult sourceScanResult, IFileScanResult destinationScanResult)
        {
            _sourceScanResult = sourceScanResult;
            _destinationScanResult = destinationScanResult;
        }

        public FolderDiff BuildDiff()
        {
            List<Diff> diffs = new List<Diff>();

            ProcessScanResultFiles(_sourceScanResult, _destinationScanResult, SourceRelativeDiff, diffs);
            ProcessScanResultFiles(_destinationScanResult, _sourceScanResult, DestinationRelativeDiff, diffs);

            return new FolderDiff(diffs);
        }

        private void ProcessScanResultFiles(IFileScanResult sourceScanResult, IFileScanResult destinationScanResult,
            Func<IKoreFileInfo, IKoreFileInfo, DiffType?> diffFunc, List<Diff> diffs)
        {
            foreach (IKoreFileInfo sourceFileInfo in sourceScanResult.Files)
            {
                string relativeFullFileName = ExtractRelativeFullFileName(sourceFileInfo, sourceScanResult.Folder.Length);

                IKoreFileInfo destinationFileInfo = destinationScanResult.Files.SingleOrDefault(
                    f => f.FullName.Substring(destinationScanResult.Folder.Length).ToLower().Equals(relativeFullFileName));

                DiffType? diffType = diffFunc(sourceFileInfo, destinationFileInfo);

                if (diffType.HasValue)
                    diffs.Add(new Diff(sourceFileInfo, destinationFileInfo, diffType.Value));
            }
        }

        private DiffType? SourceRelativeDiff(IKoreFileInfo sourceFileInfo, IKoreFileInfo destinationFileInfo)
        {
            DiffType? diffType;

            if (destinationFileInfo == null)
                diffType = DiffType.SourceNew;
            else if (sourceFileInfo.LastWriteTime > destinationFileInfo.LastWriteTime)
                diffType = DiffType.SourceNewer;
            else if (sourceFileInfo.LastWriteTime < destinationFileInfo.LastWriteTime)
                diffType = DiffType.SourceOlder;
            else
                diffType = DiffType.Identical;

            return diffType;
        }

        private DiffType? DestinationRelativeDiff(IKoreFileInfo sourceFileInfo, IKoreFileInfo destinationFileInfo)
        {
            DiffType? diffType = null;

            if (destinationFileInfo == null)
                diffType = DiffType.DestinationOrphan;

            return diffType;
        }

        private static string ExtractRelativeFullFileName(IKoreFileInfo sourceFileInfo, int startIndex)
        {
            return sourceFileInfo.FullName.Substring(startIndex).ToLower();
        }
    }
}