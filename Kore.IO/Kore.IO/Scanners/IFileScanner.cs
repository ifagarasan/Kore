namespace Kore.IO.Scanners
{
    public interface IFileScanner
    {
        IFileScanResult Scan(IKoreFolderInfo folder);
        IFileScanResult Scan(IKoreFolderInfo folder, FileScanOptions options);
    }
}