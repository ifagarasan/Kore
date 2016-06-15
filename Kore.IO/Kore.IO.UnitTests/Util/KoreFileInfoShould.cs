using System;
using System.Collections.Generic;
using System.IO;
using Kore.IO.TestUtil;
using Kore.IO.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.IO.UnitTests.Util
{
    [TestClass]
    public class KoreFileInfoShould
    {
        const string FileName = @"C:\123\abc.txt";
        KoreFileInfo _fileInfo;

        [TestInitialize]
        public void Setup()
        {
            ScannerUtil.SetupTestFiles();

            _fileInfo = new KoreFileInfo(FileName);
        }

        [TestMethod]
        public void ReturnFalseForHiddenIfFileIsNotHidden()
        {
            List<IKoreFileInfo> inputFileList = ScannerUtil.BuildOneLevelTestFilesList(true, false);

            Assert.AreNotEqual(0, inputFileList.Count);

            foreach(IKoreFileInfo fileInfo in inputFileList)
                Assert.IsFalse(fileInfo.Hidden);
        }

        [TestMethod]
        public void ReturnTrueForHiddenIfFileIsHidden()
        {
            List<IKoreFileInfo> inputFileList = new List<IKoreFileInfo>();

            ScannerUtil.AddFiles(ScannerUtil.TestFolderOneLevel, ScannerUtil.HiddenFileList, inputFileList);

            Assert.AreNotEqual(0, inputFileList.Count);

            foreach (IKoreFileInfo fileInfo in inputFileList)
                Assert.IsTrue(fileInfo.Hidden);
        }

        [TestMethod]
        public void FullNameContainsFullPathPlusName()
        {
            Assert.AreEqual(FileName, _fileInfo.FullName);
        }

        [TestMethod]
        public void ExistsReturnsFalseIfFileDoesNotExistOnDisk()
        {
            Assert.IsFalse(new KoreFileInfo("abc").Exists);
        }

        [TestMethod]
        public void ExistsReturnsTrueIfFileExistsOnDisk()
        {
            List<IKoreFileInfo> inputFileList = new List<IKoreFileInfo>();

            ScannerUtil.AddFiles(ScannerUtil.TestFolderOneLevel, ScannerUtil.HiddenFileList, inputFileList);

            Assert.AreNotEqual(0, inputFileList.Count);

            foreach(IKoreFileInfo fileInfo in inputFileList)
                Assert.IsTrue(fileInfo.Exists);
        }

        [TestMethod]
        public void LastWriteTimeGetsDateTimeOfLastWrite()
        {
            DateTime dateTime = new DateTime(1989, 2, 27);

            string file = Path.Combine(ScannerUtil.TestFolderOneLevel, ScannerUtil.VisibleFileList[0]);
            _fileInfo = new KoreFileInfo(file);
            File.SetLastWriteTime(file, dateTime);

            Assert.AreEqual(dateTime, _fileInfo.LastWriteTime);
        }

        [TestMethod]
        public void LastWriteTimeSetsDateTimeOfLastWrite()
        {
            string file = Path.Combine(ScannerUtil.TestFolderOneLevel, ScannerUtil.VisibleFileList[0]);

            DateTime dateTime = File.GetLastWriteTime(file).AddDays(1);

            _fileInfo = new KoreFileInfo(file) {LastWriteTime = dateTime};

            Assert.AreEqual(dateTime, File.GetLastWriteTime(file));
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void HiddenGetThrowsFileNotFoundExceptionIfFileDoesNotExist()
        {
            var info = _fileInfo.Hidden;
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LastWriteTimeGetThrowsFileNotFoundExceptionIfFileDoesNotExist()
        {
            var lastWriteTime = _fileInfo.LastWriteTime;
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LastWriteTimeSetThrowsFileNotFoundExceptionIfFileDoesNotExist()
        {
            _fileInfo.LastWriteTime = DateTime.Now;
        }

        [TestMethod]
        public void ReturnsIKoreFolderInfoWithDirectoryName()
        {
            string folder = "C:\\123";
            string file = Path.Combine(folder, "abc.txt");

            _fileInfo = new KoreFileInfo(file);
            IKoreFolderInfo folderInfo = _fileInfo.FolderInfo;

            Assert.AreEqual(folder, folderInfo.FullName);
        }
    }
}
