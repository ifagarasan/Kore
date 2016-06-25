using System;
using System.Collections.Generic;
using System.Linq;
using Kore.IO.Scanners;
using Kore.IO.Util;

namespace Kore.IO.Sync
{
    public class FolderDiffer : IFolderDiffer
    {
        private readonly IIdentityProvider _identityProvider;

        public FolderDiffer(IIdentityProvider identityProvider)
        {
            _identityProvider = identityProvider;
        }

        public IFolderDiff BuildDiff(IFileScanResult sourceScanResult, IFileScanResult destinationScanResult)
        {
            var diffs = new List<IDiff>();

            ProcessScanResultFiles(sourceScanResult, destinationScanResult, SourceRelativeDiff, diffs);
            ProcessScanResultFiles(destinationScanResult, sourceScanResult, DestinationRelativeDiff, diffs);

            return new FolderDiff(sourceScanResult.Folder, destinationScanResult.Folder, diffs);
        }

        private void ProcessScanResultFiles(IFileScanResult sourceScanResult, IFileScanResult destinationScanResult,
            Func<IKoreFileInfo, IKoreFileInfo, DiffRelation?> diffFunc, List<IDiff> diffs)
        {
            foreach (var sourceFileInfo in sourceScanResult.Files)
            {
                var relativeFullFileName = IoNode.RelativePath(sourceFileInfo, sourceScanResult.Folder).ToLower();

                //TODO: build dictionay based on this stuff and reduce look-up to O(1)
                var destinationFileInfo = destinationScanResult.Files.SingleOrDefault(
                    f => IoNode.RelativePath(f, destinationScanResult.Folder).ToLower().Equals(relativeFullFileName));

                var diffType = diffFunc(sourceFileInfo, destinationFileInfo);

                if (diffType.HasValue)
                {
                    var diff = new Diff(sourceFileInfo, destinationFileInfo, diffType.Value, _identityProvider.GenerateId());

                    if (diffType.Value == DiffRelation.DestinationOrphan)
                        diff = new Diff(destinationFileInfo, sourceFileInfo, diffType.Value, _identityProvider.GenerateId());

                    diffs.Add(diff);
                }
            }
        }

        public DiffRelation? SourceRelativeDiff(IKoreFileInfo sourceFileInfo, IKoreFileInfo destinationFileInfo)
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

        public DiffRelation? DestinationRelativeDiff(IKoreFileInfo sourceFileInfo, IKoreFileInfo destinationFileInfo)
        {
            DiffRelation? diffRelation = null;

            if (destinationFileInfo == null)
                diffRelation = DiffRelation.DestinationOrphan;

            return diffRelation;
        }
    }
}