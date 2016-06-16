using System;
using System.Collections.Generic;
using System.IO;
using Kore.IO.TestUtil;
using Kore.IO.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Dev.Util;

namespace Kore.IO.UnitTests.Util
{
    [TestClass]
    public abstract class KoreIoNodeInfoShould
    {
        protected static string FullName { get; set; }
        IKoreIoNodeInfo _nodeInfo;
        static string CurrentWorkingFolder;

        static KoreIoNodeInfoShould()
        {
            IoUtil.EnsureFolderExits(IoUtil.TestRoot);

            CurrentWorkingFolder = $"{IoUtil.TestRoot}\\{DateTime.Now.Ticks}";

            FullName = $"{CurrentWorkingFolder}\\node";
        }

        [TestInitialize]
        public virtual void Setup()
        {
            _nodeInfo = CreateNodeInfo(FullName);
        }

        [TestMethod]
        public void ExistsReturnsFalseIfFileDoesNotExistOnDisk()
        {
            _nodeInfo = CreateNodeInfo(FullName + "not_found");

            Assert.IsFalse(_nodeInfo.Exists);
        }

        [TestMethod]
        public void ExistsReturnsTrueIfFileExistsOnDisk()
        {
            EnsureNodeExists(_nodeInfo);

            Assert.IsTrue(_nodeInfo.Exists);
        }

        protected abstract IKoreIoNodeInfo CreateNodeInfo(string fullName);
        protected abstract void EnsureNodeExists(IKoreIoNodeInfo nodeInfo);
    }
}
