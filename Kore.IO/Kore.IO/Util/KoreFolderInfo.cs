﻿using System.IO;

namespace Kore.IO.Util{
    public class KoreFolderInfo : KoreIoNodeInfo, IKoreFolderInfo
    {
        public KoreFolderInfo(string folder)
        {
            FullName = Path.GetFullPath(folder);
        }

        public override string FullName { get; }

        public override bool Exists => Directory.Exists(FullName);

        protected override void EnsureNodeExists()
        {
            Directory.CreateDirectory(FullName);
        }

        protected override void DeleteNode()
        {
            Directory.Delete(FullName, true);
        }

        protected override void CopyNode(IKoreIoNodeInfo nodeInfo)
        {
            throw new System.NotImplementedException();
        }
    }
}