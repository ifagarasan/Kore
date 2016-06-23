using System;
using System.Collections.Generic;
using System.Linq;
using Kore.IO.Scanners;

namespace Kore.IO.Sync
{
    public class FolderDiffer : IFolderDiffer
    {
        public IFolderDiff BuildDiff(IFileScanResult sourceScanResult, IFileScanResult destinationScanResult)
        {
            var diffs = new List<IDiff>();

            ProcessScanResultFiles(sourceScanResult, destinationScanResult, SourceRelativeDiff, diffs);
            ProcessScanResultFiles(destinationScanResult, sourceScanResult, DestinationRelativeDiff, diffs);

            return new FolderDiff(new KoreFolderInfo(sourceScanResult.Folder), new KoreFolderInfo(destinationScanResult.Folder), diffs);
        }

        private void ProcessScanResultFiles(IFileScanResult sourceScanResult, IFileScanResult destinationScanResult,
            Func<IKoreFileInfo, IKoreFileInfo, DiffRelation?> diffFunc, List<IDiff> diffs)
        {
            foreach (var sourceFileInfo in sourceScanResult.Files)
            {
                var relativeFullFileName = ExtractRelativeFullFileName(sourceFileInfo, sourceScanResult.Folder.Length);

                var destinationFileInfo = destinationScanResult.Files.SingleOrDefault(
                    f => f.FullName.Substring(destinationScanResult.Folder.Length).ToLower().Equals(relativeFullFileName));

                var diffType = diffFunc(sourceFileInfo, destinationFileInfo);

                if (diffType.HasValue)
                {
                    var diff = new Diff(sourceFileInfo, destinationFileInfo, diffType.Value);

                    if (diffType.Value == DiffRelation.DestinationOrphan)
                        diff = new Diff(destinationFileInfo, sourceFileInfo, diffType.Value);

                    diffs.Add(diff);
                }
            }
        }

        private DiffRelation? SourceRelativeDiff(IKoreFileInfo sourceFileInfo, IKoreFileInfo destinationFileInfo)
        {
            DiffRelation? diffRelation;

            if (destinationFileInfo == null)
                diffRelation = DiffRelation.SourceNew;
            else if (sourceFileInfo.LastWriteTime > destinationFileInfo.LastWriteTime)
                diffRelation = DiffRelation.SourceNewer;
            else if (sourceFileInfo.LastWriteTime < destinationFileInfo.LastWriteTime)
                diffRelation = DiffRelation.SourceOlder;
            else
                diffRelation = DiffRelation.Identical;

            return diffRelation;
        }

        private DiffRelation? DestinationRelativeDiff(IKoreFileInfo sourceFileInfo, IKoreFileInfo destinationFileInfo)
        {
            DiffRelation? diffRelation = null;

            if (destinationFileInfo == null)
                diffRelation = DiffRelation.DestinationOrphan;

            return diffRelation;
        }

        private static string ExtractRelativeFullFileName(IKoreFileInfo sourceFileInfo, int startIndex)
        {
            return sourceFileInfo.FullName.Substring(startIndex).ToLower();
        }
    }
}