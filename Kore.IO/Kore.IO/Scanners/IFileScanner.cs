namespace Kore.IO.Scanners
{
    public interface IFileScanner
    {
        IFileScanResult Scan(string folder);
        IFileScanResult Scan(string folder, FileScanOptions options);
    }
}