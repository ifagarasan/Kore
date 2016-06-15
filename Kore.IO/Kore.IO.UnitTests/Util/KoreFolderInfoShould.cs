using System;
using System.Collections.Generic;
using System.IO;
using Kore.IO.TestUtil;
using Kore.IO.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.IO.UnitTests.Util
{
    [TestClass]
    public class KoreFolderInfoShould
    {
        private readonly string _testFolder;
        private string _currentWorkingFolder;

        public KoreFolderInfoShould()
        {
            _testFolder = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "TestData", "KoreFolderInfo");
            _currentWorkingFolder = $"{_testFolder}\\{DateTime.Now.Ticks}";

            EnsureFolderExits(_testFolder);
            EnsureFolderExits(_currentWorkingFolder);
        }

        [TestMethod]
        public void ReturnFullNameAsPassedInValue()
        {
            string folder = @"C:\test\abc";

            Assert.AreEqual(folder, new KoreFolderInfo(folder).FullName);
        }

        [TestMethod]
        public void ReturnFalseForExistsIfFolderDoesNotExist()
        {
            string folder = $"C:\\test\\{DateTime.Now.Ticks}\\abc";

            IKoreFolderInfo folderInfo = new KoreFolderInfo(folder);

            Assert.IsFalse(folderInfo.Exists);
        }

        [TestMethod]
        public void ReturnTrueForExistsIfFolderExists()
        {
            IKoreFolderInfo folderInfo = new KoreFolderInfo(_testFolder);

            Assert.IsTrue(folderInfo.Exists);
        }

        [TestMethod]
        public void EnsureExistsCreatesFolder()
        {
            IKoreFolderInfo folderInfo = new KoreFolderInfo(Path.Combine(_currentWorkingFolder, "EnsureExists"));

            Assert.IsFalse(folderInfo.Exists);

            folderInfo.EnsureExists();

            Assert.IsTrue(folderInfo.Exists);
        }

        private static void EnsureFolderExits(string folder)
        {
            if (Directory.Exists((folder)))
                return;

            Directory.CreateDirectory(folder);
        }
    }
}
