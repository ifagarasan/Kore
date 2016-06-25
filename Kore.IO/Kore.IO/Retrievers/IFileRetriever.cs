using System.Collections.Generic;

namespace Kore.IO.Retrievers
{
    public interface IFileRetriever
    {
        List<IKoreFileInfo> GetFiles(IKoreFolderInfo folder, string searchPattern);
    }
}