using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Kore.IO.Filters;
using Kore.IO.Retrievers;
using Kore.IO.Scanners;
using Kore.IO.TestUtil;
using Kore.IO.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.IO.Sync;

namespace Kore.IO.AcceptanceTests.Sync
{
    [TestClass]
    public class FolderDifferShould
    {
        private string _testFolder;
        private string _sourceFolder;
        private string _destinationFolder;

        private List<IKoreFileInfo> _sourceFiles;
        private List<IKoreFileInfo> _destinationFiles;

        private FileScanResult _sourceScanResult;
        private FileScanResult _destinationScanResult;

        [TestInitialize]
        public void Setup()
        {
            _testFolder = Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "TestData", "Sync");
            _sourceFolder = Path.Combine(_testFolder, "src");
            _destinationFolder = Path.Combine(_testFolder, "dest");

            _sourceFiles = new List<IKoreFileInfo>
            {
                new KoreFileInfo(Path.Combine(_sourceFolder, "same_as_destination.txt")),
                new KoreFileInfo(Path.Combine(_sourceFolder, "destination_does_not_have_it.png")),
                new KoreFileInfo(Path.Combine(_sourceFolder, "newer_than_destination.png")),
                new KoreFileInfo(Path.Combine(_sourceFolder, "older_than_destination.exe"))
            };

            _destinationFiles = new List<IKoreFileInfo>
            {
                new KoreFileInfo(Path.Combine(_destinationFolder, "same_as_destination.txt")),
                new KoreFileInfo(Path.Combine(_destinationFolder, "source_does_not_have_it.png")),
                new KoreFileInfo(Path.Combine(_destinationFolder, "newer_than_destination.png")),
                new KoreFileInfo(Path.Combine(_destinationFolder, "older_than_destination.exe"))
            };

            EnsureTestFilesExist();

            _sourceScanResult = new FileScanResult(_sourceFolder, _sourceFiles);
            _destinationScanResult = new FileScanResult(_destinationFolder, _destinationFiles);
        }

        private void EnsureTestFilesExist()
        {
            FolderUtil.EnsureExits(_testFolder);
            FolderUtil.EnsureExits(_sourceFolder);
            FolderUtil.EnsureExits(_destinationFolder);

            EnsureFilesExist(_sourceFiles);
            EnsureFilesExist(_destinationFiles);
            
            var now = DateTime.Now;
            _sourceFiles[0].LastWriteTime = now;
            _destinationFiles[0].LastWriteTime = now;

            _sourceFiles[2].LastWriteTime = now.AddDays(1);
            _destinationFiles[2].LastWriteTime = now;

            _destinationFiles[3].LastWriteTime = now.AddDays(1);
            _sourceFiles[3].LastWriteTime = now;
        }

        private void EnsureFilesExist(List<IKoreFileInfo> fileList)
        {
            foreach (IKoreFileInfo fileInfo in fileList)
                fileInfo.EnsureExits();
        }

        [TestMethod]
        public void ReturnAListOfDiffItems()
        {
            IFolderDiffer folderDiffer = new FolderDiffer(_sourceScanResult, _destinationScanResult);

            IFolderDiff folderDiff = folderDiffer.BuildDiff();

            Assert.AreEqual(DiffType.Identical, folderDiff.Diffs[0].Type);
            Assert.AreEqual(DiffType.SourceNew, folderDiff.Diffs[1].Type);
            Assert.AreEqual(DiffType.SourceNewer, folderDiff.Diffs[2].Type);
            Assert.AreEqual(DiffType.SourceOlder, folderDiff.Diffs[3].Type);
            Assert.AreEqual(DiffType.DestinationOrphan, folderDiff.Diffs[4].Type);

            Assert.AreEqual(5, folderDiff.Diffs.Count);
        }
    }
}
