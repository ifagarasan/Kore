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

        #region Size

        [TestMethod]
        public override void ReturnContentLengthInBytesForSize()
        {
            using (StreamWriter wr = new StreamWriter(_fileInfo.FullName))
            {
                wr.Write('c');
                wr.Write('d');
                wr.Write('e');
            }

            Assert.AreEqual(3, _fileInfo.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void ValidateFileExistsForSize()
        {
            DeleteNode(_fileInfo);

            var size = _fileInfo.Size;
        }

        #endregion

        #region Copy

        [TestMethod]
        public void CopySourceFileToProvidedFile()
        {
            IKoreFileInfo destinationFileInfo = new KoreFileInfo(FullName.Replace(".txt", ".cpy"));

            _fileInfo.Copy(destinationFileInfo);

            Assert.IsTrue(File.Exists(destinationFileInfo.FullName));
        }

        [TestMethod]
        public void OverridesDestinationOnCopy()
        {
            IKoreFileInfo destinationFileInfo = new KoreFileInfo(FullName.Replace(".txt", ".cpy"));

            EnsureNodeExists(destinationFileInfo);

            var date = new DateTime(1989, 2, 27);

            _fileInfo.LastWriteTime = date;
            _fileInfo.Copy(destinationFileInfo);

            Assert.AreEqual(date, destinationFileInfo.LastWriteTime);
        }

        [TestMethod]
        public void CreatesDestinationFoldersOnCopy()
        {
            string destination = Path.Combine(FullName.Remove(FullName.Length - 4), "dest", "file1.txt");

            IKoreFileInfo destinationFileInfo = new KoreFileInfo(destination);

            _fileInfo.Copy(destinationFileInfo);

            Assert.IsTrue(File.Exists(destinationFileInfo.FullName));
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
