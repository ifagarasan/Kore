using System;
using System.Collections.Generic;
using System.Linq;
using Kore.IO.Scanners;
using Kore.IO.Util;

namespace Kore.IO.Sync
{
    public class FolderDiffer : IFolderDiffer
    {
        public IFolderDiff BuildDiff(IFileScanResult sourceScanResult, IFileScanResult destinationScanResult)
        {
            List<IDiff> diffs = new List<IDiff>();

            ProcessScanResultFiles(sourceScanResult, destinationScanResult, SourceRelativeDiff, diffs);
            ProcessScanResultFiles(destinationScanResult, sourceScanResult, DestinationRelativeDiff, diffs);

            return new FolderDiff(new KoreFolderInfo(sourceScanResult.Folder), new KoreFolderInfo(destinationScanResult.Folder), diffs);
        }

        private void ProcessScanResultFiles(IFileScanResult sourceScanResult, IFileScanResult destinationScanResult,
            Func<IKoreFileInfo, IKoreFileInfo, DiffType?> diffFunc, List<IDiff> diffs)
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