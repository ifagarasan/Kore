using System;
using System.IO;
using Kore.IO.Exceptions;
using Kore.Validation;

namespace Kore.IO.Util
{
    public abstract class KoreIoNodeInfo : IKoreIoNodeInfo
    {
        public abstract bool Exists { get; }

        public abstract string FullName { get; }

        public void Copy(IKoreIoNodeInfo nodeInfo)
        {
            ObjectValidation.IsNotNull(nodeInfo, nameof(nodeInfo));

            if (!Exists)
                throw new NodeNotFoundException();

            if (FullName.Equals(nodeInfo.FullName))
                throw new InvalidDestinationNodeException();

            CopyNode(nodeInfo);
        }

        public void Delete()
        {
            if (!Exists)
                throw new NodeNotFoundException();

            DeleteNode();
        }

        public void EnsureExists()
        {
            if (Exists)
                return;

            EnsureNodeExists();
        }

        protected abstract void CopyNode(IKoreIoNodeInfo nodeInfo);
        protected abstract void EnsureNodeExists();
        protected abstract void DeleteNode();
    }
}