using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using Kore.IO.Scanners;
using Kore.IO.Retrievers;
using Kore.IO.TestUtil;
using Kore.IO.Filters;
using Kore.IO.Util;

namespace Kore.IO.AcceptanceTests.Scanners
{
    [TestClass]
    public class FileScannerShould
    {
        List<string> _expectedFileList;
        FileScanner _fileScanner;
        FileScanOptions _fileScanOptions;

        [TestInitialize]
        public void Setup()
        {
            _fileScanner = new FileScanner(new FileRetriever());
            _expectedFileList = new List<string>();
            _fileScanOptions = new FileScanOptions();

            _expectedFileList = ScannerUtil.BuildDeepTestFilesList(true, true);
        }

        [TestMethod]
        public void ReturnAllFiles()
        {
            List<string> fileList = _fileScanner.Scan(ScannerUtil.TestFolderDeep);

            AssertUtil.AssertFileListsAreEqual(_expectedFileList, fileList);
        }

        [TestMethod]
        public void ReturnsVisibleFilesWhenFilterIsSpecified()
        {
            _fileScanOptions.Filters.Add(new VisibleFileFilter(new FileInfoProvider()));

            List<string> fileList = _fileScanner.Scan(ScannerUtil.TestFolderDeep, _fileScanOptions);

            _expectedFileList = ScannerUtil.BuildDeepTestFilesList(true, false);

            AssertUtil.AssertFileListsAreEqual(_expectedFileList, fileList);
        }

        [TestMethod]
        public void AllowFilteringOfFilesBySearchPattern()
        {
            _fileScanOptions.SearchPattern = "*.txt";
            List<string> fileList = _fileScanner.Scan(ScannerUtil.TestFolderDeep, _fileScanOptions);

            _expectedFileList = ScannerUtil.BuildDeepTestFilesList(true, true, "txt");
            AssertUtil.AssertFileListsAreEqual(_expectedFileList, fileList);
        }
    }
}
