using System;
using System.IO;
using Kore.Dev.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Dev.UnitTests.Util
{
    [TestClass]
    public class IoUtilShould
    {
        private static readonly string CurrentTestFolder;
        private string _file;

        static IoUtilShould()
        {
            CurrentTestFolder = Path.Combine(IoUtil.TestRoot, DateTime.Now.Ticks.ToString());
        }

        public IoUtilShould()
        {
            _file = Path.Combine(CurrentTestFolder, "file1.txt");
        }

        [TestInitialize]
        public void Setup()
        {
            if (!Directory.Exists(IoUtil.TestRoot))
                Directory.CreateDirectory(IoUtil.TestRoot);

            if (!Directory.Exists(CurrentTestFolder))
                Directory.CreateDirectory(CurrentTestFolder);
        }

        [TestMethod]
        public void CreateFileAtLocation()
        {
            IoUtil.EnsureFileExists(_file);

            Assert.IsTrue(File.Exists(_file));
        }

        [TestMethod]
        public void CreateFileAtLocationIncludingMissingFolders()
        {
            _file = Path.Combine(CurrentTestFolder, "folder2", "file1.txt");

            IoUtil.EnsureFileExists(_file);

            Assert.IsTrue(File.Exists(_file));
        }

        [TestMethod]
        public void CreatesolderAtLocation()
        {
            string folder = Path.Combine(CurrentTestFolder, "folder1");

            IoUtil.EnsureFolderExists(folder);

            Assert.IsTrue(Directory.Exists(folder));
        }
    }
}
