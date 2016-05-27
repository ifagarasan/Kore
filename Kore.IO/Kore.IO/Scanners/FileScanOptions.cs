using Kore.IO.Filters;
using System.Collections.Generic;

namespace Kore.IO.Scanners
{
    public class FileScanOptions
    {
        public FileScanOptions()
        {
            SearchPattern = "*";
            Filters = new List<IFileFilter>();
        }

        public List<IFileFilter> Filters { get; set; }
        public string SearchPattern { get; set; }
    }
}