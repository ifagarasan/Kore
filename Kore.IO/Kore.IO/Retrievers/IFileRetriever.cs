using System.Collections.Generic;

namespace Kore.IO.Retrievers
{
    public interface IFileRetriever
    {
        List<string> GetFiles(string folder, string searchPattern);
    }
}