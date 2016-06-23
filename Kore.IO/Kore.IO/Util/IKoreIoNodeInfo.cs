﻿using System;

namespace Kore.IO.Util
{
    public interface IKoreIoNodeInfo
    {
        string FullName { get; }
        bool Exists { get; }
        string Name { get; }
        long Size { get; }

        void EnsureExists();

        void Copy(IKoreIoNodeInfo nodeInfo);
        void Delete();
    }
}