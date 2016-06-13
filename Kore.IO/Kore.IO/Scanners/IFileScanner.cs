namespace Kore.IO.Scanners
{
    public interface IFileScanner
    {
        FileScanResult Scan(string folder);
        FileScanResult Scan(string folder, FileScanOptions options);
    }
}