using System;

namespace Kore.IO.Util
{
    public interface IKoreFileInfo
    {
        bool Hidden { get; set; }
        string FullName { get; }
        bool Exists { get; }
        
        DateTime LastWriteTime { get; set; }

        IKoreFolderInfo FolderInfo { get; }

        void EnsureExits();
    }
}