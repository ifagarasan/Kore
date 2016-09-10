using System;
using System.Collections.Generic;
using System.IO;
using Kore.IO.Scanners;
using Kore.IO.Sync;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kore.IO.UnitTests.Sync
{
    [TestClass]
    public class FolderDifferShould
    {
        private IFolderDiffer _folderDiffer;
        private IFolderDiff _diff;
        private const string SourceFolder = @"C:\source";
        private const string DestinationFolder = @"C:\source";
        private Mock<IFileScanResult> _mockSourceScanResult;
        private Mock<IFileScanResult> _mockDestinationScanResult;

        private Mock<IKoreFileInfo> _mockSourceFileInfo;
        private Mock<IKoreFileInfo> _mockDestinationFileInfo;

        private Mock<IKoreFolderInfo> _mockSourceFolderInfo;
        private Mock<IKoreFolderInfo> _mockDestinationFolderInfo;

        private Mock<IIdentityProvider> _mockIdentityProvider;

        long idToReturn = 2016;

        [TestInitialize]
        public void Setup()
        {
            _mockSourceFolderInfo = new Mock<IKoreFolderInfo>();
            _mockSourceFolderInfo.Setup(m => m.FullName).Returns(SourceFolder);

            _mockDestinationFolderInfo = new Mock<IKoreFolderInfo>();
            _mockDestinationFolderInfo.Setup(m => m.FullName).Returns(DestinationFolder);

            _mockIdentityProvider = new Mock<IIdentityProvider>();
            _mockIdentityProvider.Setup(m => m.GenerateId()).Returns(idToReturn);

            _mockSourceFileInfo = new Mock<IKoreFileInfo>();
            _mockDestinationFileInfo = new Mock<IKoreFileInfo>();

            _mockSourceScanResult = new Mock<IFileScanResult>();
            InitialiseScanResultMock(_mockSourceScanResult, _mockSourceFolderInfo.Object);

            _mockDestinationScanResult = new Mock<IFileScanResult>();
            InitialiseScanResultMock(_mockDestinationScanResult, _mockDestinationFolderInfo.Object);

            _folderDiffer = new FolderDiffer(_mockIdentityProvider.Object);
        }

        private static void InitialiseScanResultMock(Mock<IFileScanResult> mockScanResult, IKoreFolderInfo folder)
        {
            mockScanResult.Setup(m => m.Folder).Returns(folder);
            mockScanResult.Setup(m => m.Files).Returns(new List<IKoreFileInfo>());
        }

        [TestMethod]
        public void UseTheIdentityProviderToInitialiseDiffIds()
        {
            var now = DateTime.Now;

            SetupIOMocks(now, now);

            _diff = _folderDiffer.BuildDiff(_mockSourceScanResult.Object, _mockDestinationScanResult.Object);

            _mockIdentityProvider.Verify(m => m.GenerateId());

            Assert.AreEqual(idToReturn, _diff.Diffs[0].Id);
        }

        [TestMethod]
        public void ReturnEmptyListWhenSourceAndDestinationAreEmpty()
        {
            _diff = _folderDiffer.BuildDiff(_mockSourceScanResult.Object, _mockDestinationScanResult.Object);

            Assert.AreEqual(0, _diff.Diffs.Count);
        }

        [TestMethod]
        public void ReturnSourceNewWhenDestinationDoesNotHaveSourceFile()
        {
            _mockSourceFileInfo.Setup(m => m.FullName).Returns(Path.Combine(SourceFolder, @"file1.txt"));
            _mockSourceScanResult.Setup(m => m.Files).Returns(new List<IKoreFileInfo> { _mockSourceFileInfo.Object });

            _diff = _folderDiffer.BuildDiff(_mockSourceScanResult.Object, _mockDestinationScanResult.Object);

            Assert.AreEqual(1, _diff.Diffs.Count);
            Assert.AreEqual(DiffRelation.SourceNew, _diff.Diffs[0].Relation);
        }

        [TestMethod]
        public void ReturnIdenticalWhenDestinationHasSameSuffixAndDateTime()
        {
            DateTime lastWriteTime = DateTime.Now;

            TestIdenticalFileFullNameDiffs(lastWriteTime, lastWriteTime, DiffRelation.Identical);
        }

        [TestMethod]
        public void ReturnSourceNewerWhenDestinationHasSameSuffixAndSourceLastWrittenTimeIsNewer()
        {
            DateTime sourceLastWriteTime = DateTime.Now;
            DateTime destinationLastWriteTime = sourceLastWriteTime.Subtract(new TimeSpan(1));

            TestIdenticalFileFullNameDiffs(sourceLastWriteTime, destinationLastWriteTime, DiffRelation.SourceNewer);
        }

        [TestMethod]
        public void ReturnSourceOlderWhenDestinationHasSameSuffixAndSourceLastWrittenTimeIsOlder()
        {
            DateTime sourceLastWriteTime = DateTime.Now;
            DateTime destinationLastWriteTime = sourceLastWriteTime.Add(new TimeSpan(1));

            TestIdenticalFileFullNameDiffs(sourceLastWriteTime, destinationLastWriteTime, DiffRelation.SourceOlder);
        }

        [TestMethod]
        public void ReturnDestinationOrphanWhenSourceDoesNotHaveDestinationFile()
        {
            _mockDestinationFileInfo.Setup(m => m.FullName).Returns(Path.Combine(DestinationFolder, @"file1.txt"));
            _mockDestinationScanResult.Setup(m => m.Files).Returns(new List<IKoreFileInfo> { _mockDestinationFileInfo.Object });

            _diff = _folderDiffer.BuildDiff(_mockSourceScanResult.Object, _mockDestinationScanResult.Object);

            Assert.AreEqual(1, _diff.Diffs.Count);
            Assert.AreEqual(DiffRelation.DestinationOrphan, _diff.Diffs[0].Relation);
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

            _diff = _folderDiffer.BuildDiff(_mockSourceScanResult.Object, _mockDestinationScanResult.Object);

            Assert.AreEqual(1, _diff.Diffs.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ThrowInvalidOperationExceptionWhenDuplicateFileInfosWereFoundInSource()
        {
            TestDuplicateFileInfos(new List<IKoreFileInfo> { _mockSourceFileInfo.Object, _mockSourceFileInfo.Object },
                new List<IKoreFileInfo> { _mockDestinationFileInfo.Object });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ThrowInvalidOperationExceptionWhenDuplicateFileInfosWereFoundInDestination()
        {
            TestDuplicateFileInfos(new List<IKoreFileInfo> { _mockSourceFileInfo.Object },
                new List<IKoreFileInfo> { _mockDestinationFileInfo.Object, _mockDestinationFileInfo.Object });
        }

        private void TestDuplicateFileInfos(List<IKoreFileInfo> sourceFileInfoList, List<IKoreFileInfo> destinationFileInfoList)
        {
            _mockSourceFileInfo.Setup(m => m.FullName).Returns(Path.Combine(SourceFolder, "data", "file1.txt"));

            _mockDestinationFileInfo.Setup(m => m.FullName).Returns(Path.Combine(DestinationFolder, "data", "File1.txt"));

            _mockSourceScanResult.Setup(m => m.Files).Returns(sourceFileInfoList);
            _mockDestinationScanResult.Setup(m => m.Files).Returns(destinationFileInfoList);

            _diff = _folderDiffer.BuildDiff(_mockSourceScanResult.Object, _mockDestinationScanResult.Object);
        }

        private void TestIdenticalFileFullNameDiffs(DateTime sourceLastWriteTime, DateTime destinationLastWriteTime, DiffRelation expectedDiffRelation)
        {
            SetupIOMocks(sourceLastWriteTime, destinationLastWriteTime);

            _diff = _folderDiffer.BuildDiff(_mockSourceScanResult.Object, _mockDestinationScanResult.Object);

            Assert.AreEqual(1, _diff.Diffs.Count);
            Assert.AreEqual(expectedDiffRelation, _diff.Diffs[0].Relation);
        }

        private void SetupIOMocks(DateTime sourceLastWriteTime, DateTime destinationLastWriteTime)
        {
            _mockSourceFileInfo.Setup(m => m.FullName).Returns(Path.Combine(SourceFolder, "data", "file1.txt"));
            _mockSourceFileInfo.Setup(m => m.LastWriteTime).Returns(sourceLastWriteTime);

            _mockDestinationFileInfo.Setup(m => m.FullName).Returns(Path.Combine(DestinationFolder, "data", "file1.txt"));
            _mockDestinationFileInfo.Setup(m => m.LastWriteTime).Returns(destinationLastWriteTime);

            _mockSourceScanResult.Setup(m => m.Files).Returns(new List<IKoreFileInfo> { _mockSourceFileInfo.Object });
            _mockDestinationScanResult.Setup(m => m.Files).Returns(new List<IKoreFileInfo> { _mockDestinationFileInfo.Object });
        }
    }
}
