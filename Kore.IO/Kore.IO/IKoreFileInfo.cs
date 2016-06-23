using System;

namespace Kore.IO
{
    public interface IKoreFileInfo: IKoreIoNodeInfo
    {
        bool Hidden { get; set; }
        
        DateTime LastWriteTime { get; set; }

        IKoreIoNodeInfo FolderInfo { get; }
    }
}