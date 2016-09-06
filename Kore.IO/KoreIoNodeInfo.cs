﻿using System;
using System.IO;
using Kore.IO.Exceptions;
using Kore.Validation;

namespace Kore.IO
{
    [Serializable]
    public abstract class KoreIoNodeInfo : IKoreIoNodeInfo
    {
        public abstract bool Exists { get; }

        public abstract string FullName { get; }

        public abstract long Size { get; }

        public string Name => Path.GetFileName(FullName);

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

        public override bool Equals(object obj)
        {
            return obj is KoreIoNodeInfo && Equals((KoreIoNodeInfo) obj);
        }

        public bool Equals(IKoreIoNodeInfo otherNodeInfo)
        {
            return FullName.Equals(otherNodeInfo.FullName);
        }

        protected abstract void CopyNode(IKoreIoNodeInfo nodeInfo);
        protected abstract void EnsureNodeExists();
        protected abstract void DeleteNode();
    }
}