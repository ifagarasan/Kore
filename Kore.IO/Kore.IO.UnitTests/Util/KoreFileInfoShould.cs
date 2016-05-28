using System.Collections.Generic;
using Kore.IO.TestUtil;
using Kore.IO.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.IO.UnitTests.Util
{
    [TestClass]
    public class KoreFileInfoShould
    {
        [TestInitialize]
        public void Setup()
        {
            ScannerUtil.SetupTestFiles();
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
            const string name = @"C:\123\abc.txt";

            KoreFileInfo fileInfo = new KoreFileInfo(name);

            Assert.AreEqual(name, fileInfo.FullName);
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
    }
}
