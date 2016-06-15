using System;

namespace Kore.IO.Util
{
    public interface IKoreIoNodeInfo
    {
        string FullName { get; }
        bool Exists { get; }

        void EnsureExists();
    }
}