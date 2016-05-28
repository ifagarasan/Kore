using System.Collections.Generic;
using Kore.IO.Util;

namespace Kore.IO.Retrievers
{
    public interface IFileRetriever
    {
        List<IKoreFileInfo> GetFiles(string folder, string searchPattern);
    }
}