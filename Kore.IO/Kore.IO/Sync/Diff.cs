using Kore.IO.Util;

namespace Kore.IO.Sync
{
    public class Diff : IDiff
    {
        public Diff(IKoreFileInfo sourceFileInfo, IKoreFileInfo destinationFileInfo, DiffType type)
        {
            SourceFileInfo = sourceFileInfo;
            DestinationFileInfo = destinationFileInfo;
            Type = type;
        }

        public DiffType Type { get; }
        public IKoreFileInfo DestinationFileInfo { get; }
        public IKoreFileInfo SourceFileInfo { get; }
    }
}