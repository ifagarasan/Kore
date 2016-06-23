using System;
using System.IO;
using Kore.Dev.Util;
using Kore.IO.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.IO.UnitTests
{
    [TestClass]
    public abstract class KoreIoNodeInfoShould
    {
        protected static string FullName { get; set; }
        IKoreIoNodeInfo _nodeInfo;
        protected static string CurrentWorkingFolder;

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
            EnsureNodeExists(_nodeInfo);
        }

        [TestMethod]
        public void ReturnFullPathForFullName()
        {
            Assert.AreEqual(Path.GetFullPath(FullName), _nodeInfo.FullName);
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

        [TestMethod]
        public void EnsureExistsCreatesNode()
        {
            DeleteNode(_nodeInfo);

            _nodeInfo.EnsureExists();

            Assert.IsTrue(_nodeInfo.Exists);
        }

        #region Delete

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void ValidateNodeExists()
        {
            DeleteNode(_nodeInfo);

            _nodeInfo.Delete();
        }

        [TestMethod]
        public void DeleteTheNode()
        {
            _nodeInfo.Delete();

            Assert.IsFalse(_nodeInfo.Exists);
        }

        #endregion

        [TestMethod]
        public void ReturnNameWithoutAnyPathElements()
        {
            Assert.AreEqual(Path.GetFileName(_nodeInfo.FullName), _nodeInfo.Name);
        }

        public abstract void ReturnContentLengthInBytesForSize();

        protected abstract IKoreIoNodeInfo CreateNodeInfo(string fullName);
        protected abstract void EnsureNodeExists(IKoreIoNodeInfo nodeInfo);
        protected abstract void DeleteNode(IKoreIoNodeInfo nodeInfo);
    }
}
