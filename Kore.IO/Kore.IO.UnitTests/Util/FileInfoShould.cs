using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Kore.IO.Retrievers;
using Kore.IO.Scanners;
using Kore.IO.TestUtil;
using Kore.IO.Filters;
using Kore.IO.Util;

namespace Kore.IO.UnitTests.Scanners
{
    [TestClass]
    public class FileInfoShould
    {
        [TestInitialize]
        public void Setup()
        {
            ScannerUtil.SetupTestFiles();
        }

        [TestMethod]
        public void ReturnFalseForHiddenIfFileIsNotHidden()
        {
            List<string> inputFileList = ScannerUtil.BuildOneLevelTestFilesList(true, false);

            Assert.AreNotEqual(0, inputFileList.Count);

            foreach(string file in inputFileList)
            {
                KoreFileInfo fileInfo = new KoreFileInfo(file);
                Assert.IsFalse(fileInfo.Hidden);
            }
        }

        [TestMethod]
        public void ReturnTrueForHiddenIfFileIsHidden()
        {
            List<string> inputFileList = new List<string>();

            ScannerUtil.AddFiles(ScannerUtil.TestFolderOneLevel, ScannerUtil.HiddenFileList, inputFileList);

            Assert.AreNotEqual(0, inputFileList.Count);

            foreach (string file in inputFileList)
            {
                KoreFileInfo fileInfo = new KoreFileInfo(file);
                Assert.IsTrue(fileInfo.Hidden);
            }
        }
    }
}
