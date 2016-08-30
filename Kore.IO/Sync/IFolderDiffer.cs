using Kore.IO.Scanners;

namespace Kore.IO.Sync
{
    public interface IFolderDiffer
    {
        IFolderDiff BuildDiff(IFileScanResult sourceScanResult, IFileScanResult destinationScanResult);
    }
}