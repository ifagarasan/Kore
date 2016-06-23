using System;
using System.IO;
using Kore.Dev.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.IO.UnitTests
{
    [TestClass]
    public class KoreFolderInfoShould: KoreIoNodeInfoShould
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();

            if (!CurrentWorkingFolder.EndsWith("folder"))
                CurrentWorkingFolder = $"{CurrentWorkingFolder}_folder";
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public override void ReturnContentLengthInBytesForSize()
        {
            throw new NotImplementedException();
        }

        protected override IKoreIoNodeInfo CreateNodeInfo(string fullName)
        {
            return new KoreFolderInfo(fullName);
        }

        protected override void DeleteNode(IKoreIoNodeInfo nodeInfo)
        {
            Directory.Delete(nodeInfo.FullName, true);
        }

        protected override void EnsureNodeExists(IKoreIoNodeInfo nodeInfo)
        {
            IoUtil.EnsureFolderExits(nodeInfo.FullName);
        }
    }
}
