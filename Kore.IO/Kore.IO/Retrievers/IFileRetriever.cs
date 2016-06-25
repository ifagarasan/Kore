using System.Collections.Generic;

namespace Kore.IO.Retrievers
{
    public interface IFileRetriever
    {
        event FileFoundDelegate FileFound;

        List<IKoreFileInfo> GetFiles(IKoreFolderInfo folder);
    }
}