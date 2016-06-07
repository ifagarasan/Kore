using System;
using System.Collections.Generic;
using System.IO;
using Castle.Components.DictionaryAdapter;
using Kore.IO.Scanners;
using Kore.IO.Sync;
using Kore.IO.TestUtil;
using Kore.IO.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kore.IO.UnitTests.Sync
{
    [TestClass]
    public class FolderDifferShould
    {
        private FolderDiffer _folderDiffer;
        private FolderDiff _diff;
        private const string SourceFolder = @"C:\source";
        private const string DestinationFolder = @"C:\source";
        private Mock<IFileScanResult> _mockSourceScanResult;
        private Mock<IFileScanResult> _mockDestinationScanResult;

        private Mock<IKoreFileInfo> _mockSourceFileInfo;
        private Mock<IKoreFileInfo> _mockDestinationFileInfo;

        [TestInitialize]
        public void Setup()
        {
            _mockSourceFileInfo = new Mock<IKoreFileInfo>();
            _mockDestinationFileInfo = new Mock<IKoreFileInfo>();

            _mockSourceScanResult = new Mock<IFileScanResult>();
            InitialiseScanResultMock(_mockSourceScanResult, SourceFolder);

            _mockDestinationScanResult = new Mock<IFileScanResult>();
            InitialiseScanResultMock(_mockDestinationScanResult, DestinationFolder);
        }

        private static void InitialiseScanResultMock(Mock<IFileScanResult> mockScanResult, string folder)
        {
            mockScanResult.Setup(m => m.Folder).Returns(folder);
            mockScanResult.Setup(m => m.Files).Returns(new List<IKoreFileInfo>());
        }

        [TestMethod]
        public void ReturnEmptyListWhenSourceAndDestinationAreEmpty()
        {
            _folderDiffer = new FolderDiffer(_mockSourceScanResult.Object, _mockDestinationScanResult.Object);

            _diff = _folderDiffer.BuildDiff();

            Assert.AreEqual(0, _diff.Diffs.Count);
        }

        [TestMethod]
        public void ReturnSourceNewWhenDestinationDoesNotHaveSourceFile()
        {
            _mockSourceFileInfo.Setup(m => m.FullName).Returns(Path.Combine(SourceFolder, @"file1.txt"));
            _mockSourceScanResult.Setup(m => m.Files).Returns(new List<IKoreFileInfo> { _mockSourceFileInfo.Object });

            _folderDiffer = new FolderDiffer(_mockSourceScanResult.Object, _mockDestinationScanResult.Object);

            _diff = _folderDiffer.BuildDiff();

            Assert.AreEqual(1, _diff.Diffs.Count);
            Assert.AreEqual(DiffType.SourceNew, _diff.Diffs[0].Type);
        }

        [TestMethod]
        public void ReturnIdenticalWhenDestinationHasSameSuffixAndDateTime()
        {
            DateTime lastWriteTime = DateTime.Now;

            TestIdenticalFileFullNameDiffs(lastWriteTime, lastWriteTime, DiffType.Identical);
        }

        [TestMethod]
        public void ReturnSourceNewerWhenDestinationHasSameSuffixAndSourceLastWrittenTimeIsNewer()
        {
            DateTime sourceLastWriteTime = DateTime.Now;
            DateTime destinationLastWriteTime = sourceLastWriteTime.Subtract(new TimeSpan(1));

            TestIdenticalFileFullNameDiffs(sourceLastWriteTime, destinationLastWriteTime, DiffType.SourceNewer);
        }

        [TestMethod]
        public void ReturnSourceOlderWhenDestinationHasSameSuffixAndSourceLastWrittenTimeIsOlder()
        {
            DateTime sourceLastWriteTime = DateTime.Now;
            DateTime destinationLastWriteTime = sourceLastWriteTime.Add(new TimeSpan(1));

            TestIdenticalFileFullNameDiffs(sourceLastWriteTime, destinationLastWriteTime, DiffType.SourceOlder);
        }

        [TestMethod]
        public void ReturnDestinationOrphanWhenSourceDoesNotHaveDestinationFile()
        {
            _mockDestinationFileInfo.Setup(m => m.FullName).Returns(Path.Combine(DestinationFolder, @"file1.txt"));
            _mockDestinationScanResult.Setup(m => m.Files).Returns(new List<IKoreFileInfo> { _mockDestinationFileInfo.Object });

            _folderDiffer = new FolderDiffer(_mockSourceScanResult.Object, _mockDestinationScanResult.Object);

            _diff = _folderDiffer.BuildDiff();

            Assert.AreEqual(1, _diff.Diffs.Count);
            Assert.AreEqual(DiffType.DestinationOrphan, _diff.Diffs[0].Type);
        }

        [TestMethod]
        public void NotCareAboutCasing()
        {
            DateTime now = DateTime.Now;

            _mockSourceFileInfo.Setup(m => m.FullName).Returns(Path.Combine(SourceFolder, "data", "file1.txt"));
            _mockSourceFileInfo.Setup(m => m.LastWriteTime).Returns(now);

            _mockDestinationFileInfo.Setup(m => m.FullName).Returns(Path.Combine(DestinationFolder, "data", "File1.txt"));
            _mockDestinationFileInfo.Setup(m => m.LastWriteTime).Returns(now);

            _mockSourceScanResult.Setup(m => m.Files).Returns(new List<IKoreFileInfo> { _mockSourceFileInfo.Object });
            _mockDestinationScanResult.Setup(m => m.Files).Returns(new List<IKoreFileInfo> { _mockDestinationFileInfo.Object });

            _folderDiffer = new FolderDiffer(_mockSourceScanResult.Object, _mockDestinationScanResult.Object);

            _diff = _folderDiffer.BuildDiff();

            Assert.AreEqual(1, _diff.Diffs.Count);
        }

        private void TestIdenticalFileFullNameDiffs(DateTime sourceLastWriteTime, DateTime destinationLastWriteTime, DiffType expectedDiffType)
        {
            _mockSourceFileInfo.Setup(m => m.FullName).Returns(Path.Combine(SourceFolder, "data", "file1.txt"));
            _mockSourceFileInfo.Setup(m => m.LastWriteTime).Returns(sourceLastWriteTime);

            _mockDestinationFileInfo.Setup(m => m.FullName).Returns(Path.Combine(DestinationFolder, "data", "file1.txt"));
            _mockDestinationFileInfo.Setup(m => m.LastWriteTime).Returns(destinationLastWriteTime);

            _mockSourceScanResult.Setup(m => m.Files).Returns(new List<IKoreFileInfo> { _mockSourceFileInfo.Object });
            _mockDestinationScanResult.Setup(m => m.Files).Returns(new List<IKoreFileInfo> { _mockDestinationFileInfo.Object });

            _folderDiffer = new FolderDiffer(_mockSourceScanResult.Object, _mockDestinationScanResult.Object);

            _diff = _folderDiffer.BuildDiff();

            Assert.AreEqual(1, _diff.Diffs.Count);
            Assert.AreEqual(expectedDiffType, _diff.Diffs[0].Type);
        }
    }
}
