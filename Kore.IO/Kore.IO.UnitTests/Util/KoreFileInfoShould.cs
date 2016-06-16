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
    public class KoreFileInfoShould: KoreIoNodeInfoShould
    {
        KoreFileInfo _fileInfo;

        [TestInitialize]
        public override void Setup()
        {
            if (!FullName.EndsWith(".txt"))
                FullName = $"{FullName}.txt";

            base.Setup();

            _fileInfo = new KoreFileInfo(FullName);
            EnsureNodeExists(_fileInfo);
        }

        #region Hidden

        [TestMethod]
        public void ReturnFalseForHiddenIfFileIsNotHidden()
        {
            Assert.IsFalse(_fileInfo.Hidden);
        }

        [TestMethod]
        public void ReturnTrueForHiddenIfFileIsHidden()
        {
            File.SetAttributes(_fileInfo.FullName, FileAttributes.Hidden);

            Assert.IsTrue(_fileInfo.Hidden);
        }

        [TestMethod]
        public void AllowSettingHidden()
        {
            _fileInfo.Hidden = true;

            Assert.IsTrue(_fileInfo.Hidden);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ThrowsFileNotFoundExceptionIfFileDoesNotExistOnHiddenGet()
        {
            File.Delete(FullName);

            var info = _fileInfo.Hidden;
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ThrowsFileNotFoundExceptionIfFileDoesNotExistOnHiddenSet()
        {
            File.Delete(FullName);

            _fileInfo.Hidden = true;
        }

        #endregion

        #region LastWriteTime

        [TestMethod]
        public void LastWriteTimeGetsDateTimeOfLastWrite()
        {
            var now = new DateTime(1989, 2, 27);

            File.SetLastWriteTime(FullName, now);

            Assert.AreEqual(now, _fileInfo.LastWriteTime);
        }

        [TestMethod]
        public void LastWriteTimeSetsDateTimeOfLastWrite()
        {
            var date = new DateTime(1989, 2, 27);

            File.SetLastWriteTime(FullName, date);

            date.AddMinutes(13);

            _fileInfo.LastWriteTime = date;

            Assert.AreEqual(date, _fileInfo.LastWriteTime);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LastWriteTimeGetThrowsFileNotFoundExceptionIfFileDoesNotExist()
        {
            File.Delete(FullName);

            var lastWriteTime = _fileInfo.LastWriteTime;
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LastWriteTimeSetThrowsFileNotFoundExceptionIfFileDoesNotExist()
        {
            File.Delete(FullName);

            _fileInfo.LastWriteTime = DateTime.Now;
        }

        #endregion

        [TestMethod]
        public void ReturnsIKoreFolderInfoWithDirectoryName()
        {
            IKoreIoNodeInfo folderInfo = _fileInfo.FolderInfo;

            Assert.AreEqual($"{folderInfo.FullName}\\{Path.GetFileName(FullName)}", Path.GetFullPath(FullName));
        }

        protected override IKoreIoNodeInfo CreateNodeInfo(string fullName)
        {
            return new KoreFileInfo(fullName);
        }

        protected override void EnsureNodeExists(IKoreIoNodeInfo nodeInfo)
        {
            IoUtil.EnsureFileExits(nodeInfo.FullName);
        }

        protected override void DeleteNode(IKoreIoNodeInfo nodeInfo)
        {
            File.Delete(nodeInfo.FullName);
        }
    }
}
