using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using Kore.IO.Scanners;
using Kore.IO.Retrievers;
using Kore.IO.TestUtil;

namespace Kore.IO.AcceptanceTests.Scanners
{
    [TestClass]
    public class FileScannerShould
    {
        List<string> _expectedFileList;
        FileScanner _fileScanner;

        [TestInitialize]
        public void Setup()
        {
            _fileScanner = new FileScanner(new FileRetriever());
            _expectedFileList = new List<string>();

            ScannerUtil.BuildTestFilesList(_expectedFileList);
        }

        [TestMethod]
        public void ReturnAllFiles()
        {
            List<string> fileList = _fileScanner.Scan(ScannerUtil.TestFolderDeep);

            AssertUtil.AssertFileListsAreEqual(_expectedFileList, fileList);
        }

        [TestMethod]
        public void AllowFilteringOfFilesBySearchPattern()
        {
            FileScanOptions options = new FileScanOptions();
            options.SearchPattern = "*.txt";
            List<string> fileList = _fileScanner.Scan(ScannerUtil.TestFolderDeep, options);

            _expectedFileList.Clear();
            ScannerUtil.BuildTestFilesList(_expectedFileList, "txt");
            AssertUtil.AssertFileListsAreEqual(_expectedFileList, fileList);
        }
    }
}
