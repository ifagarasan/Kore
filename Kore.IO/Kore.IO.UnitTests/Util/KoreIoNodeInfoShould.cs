using System;
using System.Collections.Generic;
using System.IO;
using Kore.IO.TestUtil;
using Kore.IO.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Dev.Util;
using Kore.IO.Exceptions;

namespace Kore.IO.UnitTests.Util
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

        #region Copy

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void ValidatesSourceExistsOnCopy()
        {
            DeleteNode(_nodeInfo);

            _nodeInfo.Copy(_nodeInfo);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidatesDestinationOnCopy()
        {
            _nodeInfo.Copy(null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDestinationNodeException))]
        public void ValidatesSourceAndDestinationDontMatchOnCopy()
        {
            var destinationNodeInfo = CreateNodeInfo(_nodeInfo.FullName);

            _nodeInfo.Copy(destinationNodeInfo);
        }

        #endregion

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

        protected abstract IKoreIoNodeInfo CreateNodeInfo(string fullName);
        protected abstract void EnsureNodeExists(IKoreIoNodeInfo nodeInfo);
        protected abstract void DeleteNode(IKoreIoNodeInfo nodeInfo);
    }
}
