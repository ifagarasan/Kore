using System;

namespace Kore.IO.Util
{
    public interface IKoreFileInfo
    {
        bool Hidden { get; set; }
        string FullName { get; }
        bool Exists { get; }
        string DirectoryFullName { get; }
        DateTime LastWriteTime { get; set; }

        void EnsureDirectoryExists();
        void EnsureExits();
    }
}