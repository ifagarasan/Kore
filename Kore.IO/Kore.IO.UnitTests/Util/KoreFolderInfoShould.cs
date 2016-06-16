using System;
using System.Collections.Generic;
using System.IO;
using Kore.Dev.Util;
using Kore.IO.TestUtil;
using Kore.IO.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.IO.UnitTests.Util
{
    [TestClass]
    public class KoreFolderInfoShould
    {
        private static readonly string CurrentWorkingFolder;
        IKoreIoNodeInfo folderInfo;

        static KoreFolderInfoShould()
        {
            CurrentWorkingFolder = $"{IoUtil.TestRoot}\\KoreFolderInfo\\{DateTime.Now.Ticks}";
        }

        [TestInitialize]
        public void Setup()
        {
            IoUtil.EnsureFolderExits(IoUtil.TestRoot);
            IoUtil.EnsureFolderExits(CurrentWorkingFolder);
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

            folderInfo = new KoreFolderInfo(folder);

            Assert.IsFalse(folderInfo.Exists);
        }

        [TestMethod]
        public void ReturnTrueForExistsIfFolderExists()
        {
            folderInfo = new KoreFolderInfo(IoUtil.TestRoot);

            Assert.IsTrue(folderInfo.Exists);
        }

        [TestMethod]
        public void EnsureExistsCreatesFolder()
        {
            folderInfo = new KoreFolderInfo(Path.Combine(CurrentWorkingFolder, "EnsureExists"));

            Assert.IsFalse(folderInfo.Exists);

            folderInfo.EnsureExists();

            Assert.IsTrue(folderInfo.Exists);
        }
    }
}
