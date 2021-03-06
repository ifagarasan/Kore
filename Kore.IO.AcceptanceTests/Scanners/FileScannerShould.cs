﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Kore.IO.Scanners;
using Kore.IO.Retrievers;
using Kore.IO.TestUtil;
using Kore.IO.Filters;

namespace Kore.IO.AcceptanceTests.Scanners
{
    [TestClass]
    public class FileScannerShould
    {
        List<IKoreFileInfo> _expectedFileList;
        FileScanner _fileScanner;
        FileScanOptions _fileScanOptions;
        private IKoreFolderInfo _testDeepFolderInfo;

        [TestInitialize]
        public void Setup()
        {
            _fileScanner = new FileScanner(new FileRetriever());
            _expectedFileList = new List<IKoreFileInfo>();
            _fileScanOptions = new FileScanOptions();

            _expectedFileList = ScannerUtil.BuildDeepTestFilesList(true, true);

            _testDeepFolderInfo = new KoreFolderInfo(ScannerUtil.TestFolderDeep);
        }

        [TestMethod]
        public void ReturnAllFiles()
        {
            var scanResult = _fileScanner.Scan(_testDeepFolderInfo);

            AssertUtil.AssertIKoreFileInfoListsAreEqual(_expectedFileList, scanResult.Files);
        }

        [TestMethod]
        public void ReturnsVisibleFilesWhenFilterIsSpecified()
        {
            _fileScanOptions.Filters.Add(new VisibleFileFilter());

            var scanResult = _fileScanner.Scan(_testDeepFolderInfo, _fileScanOptions);

            _expectedFileList = ScannerUtil.BuildDeepTestFilesList(true, false);

            AssertUtil.AssertIKoreFileInfoListsAreEqual(_expectedFileList, scanResult.Files);
        }
    }
}
