﻿using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using Kore.IO.Retrievers;
using Kore.IO.TestUtil;
using Kore.IO.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kore.IO.UnitTests.Retrievers
{
    [TestClass]
    public class FileRetrieverShould
    {
        private List<IKoreFileInfo> _expectedFiles;
        private FileRetriever _fileRetriever;
        private Mock<IFileInfoProvider> _mockFileInfoProvider;
        private Mock<IKoreFileInfo> _mockFileInfo;

        [TestInitialize]
        public void Setup()
        {
            _expectedFiles = new List<IKoreFileInfo>();
            _mockFileInfo = new Mock<IKoreFileInfo>();
            _mockFileInfoProvider = new Mock<IFileInfoProvider>();
            _mockFileInfoProvider.Setup(m => m.CreateFileInfo(It.IsAny<string>())).Returns(_mockFileInfo.Object);

            _fileRetriever = new FileRetriever(_mockFileInfoProvider.Object);
        }

        [TestMethod]
        public void ReturnsAllFilesInAFolderIncludingHidden()
        {
            ScannerUtil.AddFiles(ScannerUtil.TestFolderOneLevel, ScannerUtil.VisibleFileList, _expectedFiles);
            ScannerUtil.AddFiles(ScannerUtil.TestFolderOneLevel, ScannerUtil.HiddenFileList, _expectedFiles);

            List<IKoreFileInfo> actualFiles = _fileRetriever.GetFiles(ScannerUtil.TestFolderOneLevel, "*");
            Assert.AreEqual(_expectedFiles.Count, actualFiles.Count);
        }

        [TestMethod]
        public void ReturnsAllFilesInAFolderIncludingHiddenRecursively()
        {
            _expectedFiles = ScannerUtil.BuildDeepTestFilesList(true, true);

            List<IKoreFileInfo> actualFiles = _fileRetriever.GetFiles(ScannerUtil.TestFolderDeep, "*");

            Assert.AreEqual(_expectedFiles.Count, actualFiles.Count);
        }
    }
}
