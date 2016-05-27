using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using Kore.IO.Retrievers;
using Kore.IO.TestUtil;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.IO.UnitTests.Retrievers
{
    [TestClass]
    public class FileRetrieverShould
    {
        private List<string> _expectedFiles;
        private FileRetriever _fileRetriever;

        [TestInitialize]
        public void Setup()
        {
            _expectedFiles = new List<string>();
            _fileRetriever = new FileRetriever();
        }

        [TestMethod]
        public void ReturnsAllFilesInAFolderIncludingHidden()
        {
            ScannerUtil.AddFiles(ScannerUtil.TestFolderOneLevel, ScannerUtil.VisibleFileList, _expectedFiles);
            ScannerUtil.AddFiles(ScannerUtil.TestFolderOneLevel, ScannerUtil.HiddenFileList, _expectedFiles);

            List<string> actualFiles = _fileRetriever.GetFiles(ScannerUtil.TestFolderOneLevel, "*");

            AssertUtil.AssertFileListsAreEqual(_expectedFiles, actualFiles);
        }

        [TestMethod]
        public void ReturnsAllFilesInAFolderIncludingHiddenRecursively()
        {
            ScannerUtil.BuildTestFilesList(_expectedFiles);

            List<string> actualFiles = _fileRetriever.GetFiles(ScannerUtil.TestFolderDeep, "*");

            AssertUtil.AssertFileListsAreEqual(_expectedFiles, actualFiles);
        }
    }
}
