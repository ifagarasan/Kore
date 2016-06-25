using Kore.IO.Retrievers;

namespace Kore.IO.Scanners
{
    public interface IFileScanner
    {
        event FileFoundDelegate FileFound;

        IFileScanResult Scan(IKoreFolderInfo folder);
        IFileScanResult Scan(IKoreFolderInfo folder, FileScanOptions options);
    }
}