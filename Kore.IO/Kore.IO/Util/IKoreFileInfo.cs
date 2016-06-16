using System;

namespace Kore.IO.Util
{
    public interface IKoreFileInfo: IKoreIoNodeInfo
    {
        bool Hidden { get; set; }
        
        DateTime LastWriteTime { get; set; }

        IKoreIoNodeInfo FolderInfo { get; }
    }
}