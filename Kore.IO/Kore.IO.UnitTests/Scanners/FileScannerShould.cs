using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Kore.IO.Retrievers;
using Kore.IO.Scanners;
using Kore.IO.TestUtil;
using Kore.IO.Filters;

namespace Kore.IO.UnitTests.Scanners
{
    [TestClass]
    public class FileScannerShould
    {
        Mock<IFileRetriever> _mockFileRetriever;
        FileScanner _fileScanner;
        FileScanOptions _fileScanOptions;
        private IKoreFolderInfo _testFolderDeep;

        [TestInitialize]
        public void Setup()
        {
            _mockFileRetriever = new Mock<IFileRetriever>();
            _mockFileRetriever.Setup(m => m.GetFiles(It.IsAny<IKoreFolderInfo>(), It.IsAny<string>())).Returns(new List<IKoreFileInfo>());

            _fileScanner = new FileScanner(_mockFileRetriever.Object);
            _fileScanOptions = new FileScanOptions();

            _testFolderDeep = new KoreFolderInfo("TestFolderDeep");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateFileScanOptionsOnScan()
        {
            _fileScanner.Scan(_testFolderDeep, null);
        }

        [TestMethod]
        public void CallFilterForEveryFilter()
        {
            Mock<IFileFilter> mockFileFilter = new Mock<IFileFilter>();

            List<IKoreFileInfo> filteredFiles = new List<IKoreFileInfo>();
            List<IKoreFileInfo> inputFiles = ScannerUtil.BuildDeepTestFilesList(true, false);
            List<IKoreFileInfo>[] expected = new List<IKoreFileInfo>[2];

            expected[0] = inputFiles;
            expected[1] = filteredFiles;
            int index = 0;

            mockFileFilter.Setup(m => m.Filter(It.IsAny<List<IKoreFileInfo>>())).Returns(filteredFiles).Callback(
                (List<IKoreFileInfo> files) =>
                {
                    Assert.AreSame(expected[index++], files);
                });

            _mockFileRetriever.Setup(m => m.GetFiles(It.IsAny<IKoreFolderInfo>(), It.IsAny<string>())).Returns(inputFiles);

            _fileScanOptions.Filters.Add(mockFileFilter.Object);
            _fileScanOptions.Filters.Add(mockFileFilter.Object);

            _fileScanner.Scan(_testFolderDeep, _fileScanOptions);

            Assert.AreEqual(2, index);
        }
    }
}
