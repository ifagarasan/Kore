namespace Kore.IO.Scanners
{
    public class FileScanOptions
    {
        public FileScanOptions()
        {
            SearchPattern = "*";
        }

        public string SearchPattern { get; set; }
    }
}