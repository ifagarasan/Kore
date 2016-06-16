using System;
using System.IO;

namespace Kore.IO.Util
{
    public abstract class KoreIoNodeInfo : IKoreIoNodeInfo
    {
        public abstract bool Exists { get; }

        public abstract string FullName { get; }

        public void Copy(IKoreIoNodeInfo nodeInfo)
        {
            CopyNode(nodeInfo);
        }

        public void EnsureExists()
        {
            if (Exists)
                return;

            EnsureNodeExists();
        }

        protected abstract void CopyNode(IKoreIoNodeInfo nodeInfo);
        protected abstract void EnsureNodeExists();
    }
}