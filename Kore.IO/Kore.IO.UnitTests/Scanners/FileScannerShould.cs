using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Kore.Exceptions;
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
        private Mock<IFileRetriever> _mockFileRetriever;
        private FileScanner _fileScanner;
        private FileScanOptions _fileScanOptions;
        private IKoreFolderInfo _testFolderDeep;
        private Mock<IFileFilter> _mockFileFilter;
        List<IKoreFileInfo> _mockFiles;

        [TestInitialize]
        public void Setup()
        {
            _mockFiles = new List<IKoreFileInfo>
            {
                new Mock<IKoreFileInfo>().Object,
                new Mock<IKoreFileInfo>().Object,
                new Mock<IKoreFileInfo>().Object
            };

            _mockFileFilter = new Mock<IFileFilter>();

            _mockFileRetriever = new Mock<IFileRetriever>();
            _mockFileRetriever.Setup(m => m.GetFiles(It.IsAny<IKoreFolderInfo>())).Returns(new List<IKoreFileInfo>());

            _fileScanner = new FileScanner(_mockFileRetriever.Object);
            _fileScanOptions = new FileScanOptions();

            _testFolderDeep = new KoreFolderInfo("TestFolderDeep");
        }

        [TestMethod]
        [ExpectedException(typeof(NullException))]
        public void ValidateFileScanOptionsOnScan()
        {
            _fileScanner.Scan(_testFolderDeep, null);
        }

        [TestMethod]
        public void CallFilterForEveryFilter()
        {
            _mockFileFilter.Setup(m => m.Filter(It.IsAny<List<IKoreFileInfo>>())).Returns(new List<IKoreFileInfo> { _mockFiles[0]});

            _mockFileRetriever.Setup(m => m.GetFiles(It.IsAny<IKoreFolderInfo>()))
                .Returns(_mockFiles)
                .Raises(m => m.FileFound += null, _mockFiles[0]);

            _fileScanOptions.Filters.Add(_mockFileFilter.Object);
            _fileScanOptions.Filters.Add(_mockFileFilter.Object);

            _fileScanner.Scan(_testFolderDeep, _fileScanOptions);

            _mockFileFilter.Verify(m => m.Filter(It.IsAny<List<IKoreFileInfo>>()), Times.Exactly(2));
        }

        public void RaisesFileFoundOnFilteredFiles()
        {
            List<IKoreFileInfo> filteredFiles = _mockFiles.GetRange(0, 2);

            _mockFileFilter.Setup(m => m.Filter(It.IsAny<List<IKoreFileInfo>>())).Returns(filteredFiles);

            _mockFileRetriever.Setup(m => m.GetFiles(It.IsAny<IKoreFolderInfo>())).Returns(_mockFiles);

            _fileScanOptions.Filters.Add(_mockFileFilter.Object);

            var times = 0;

            _fileScanner.FileFound += (f) => { times++; };

            _fileScanner.Scan(_testFolderDeep, _fileScanOptions);

            Assert.AreEqual(filteredFiles.Count, times);
        }
    }
}
