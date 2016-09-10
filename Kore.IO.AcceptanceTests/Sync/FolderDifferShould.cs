using System;
using System.Collections.Generic;
using System.IO;
using Kore.IO.Scanners;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.IO.Sync;
using Kore.Dev.Util;

namespace Kore.IO.AcceptanceTests.Sync
{
    [TestClass]
    public class FolderDifferShould
    {
        private string _testFolder;
        private string _sourceFolder;
        private string _destinationFolder;

        private IKoreFolderInfo _sourceFolderInfo;
        private IKoreFolderInfo _destinationFolderInfo;

        private List<IKoreFileInfo> _sourceFiles;
        private List<IKoreFileInfo> _destinationFiles;

        private FileScanResult _sourceScanResult;
        private FileScanResult _destinationScanResult;

        [TestInitialize]
        public void Setup()
        {
            _testFolder = Path.Combine(Environment.CurrentDirectory, Path.Combine("..", Path.Combine("..", Path.Combine("..", Path.Combine("TestData", "Sync")))));
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

            _sourceFolderInfo = new KoreFolderInfo(_sourceFolder);
            _destinationFolderInfo = new KoreFolderInfo(_destinationFolder);

            _sourceScanResult = new FileScanResult(_sourceFolderInfo, _sourceFiles);
            _destinationScanResult = new FileScanResult(_destinationFolderInfo, _destinationFiles);
        }

        private void EnsureTestFilesExist()
        {
            IoUtil.EnsureFolderExists(_testFolder);
            IoUtil.EnsureFolderExists(_sourceFolder);
            IoUtil.EnsureFolderExists(_destinationFolder);

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
                fileInfo.EnsureExists();
        }

        [TestMethod]
        public void ReturnAListOfDiffItems()
        {
            IFolderDiffer folderDiffer = new FolderDiffer(new IdentityProvider());

            IFolderDiff folderDiff = folderDiffer.BuildDiff(_sourceScanResult, _destinationScanResult);

            Assert.AreEqual(DiffRelation.Identical, folderDiff.Diffs[0].Relation);
            Assert.AreEqual(DiffRelation.SourceNew, folderDiff.Diffs[1].Relation);
            Assert.AreEqual(DiffRelation.SourceNewer, folderDiff.Diffs[2].Relation);
            Assert.AreEqual(DiffRelation.SourceOlder, folderDiff.Diffs[3].Relation);
            Assert.AreEqual(DiffRelation.DestinationOrphan, folderDiff.Diffs[4].Relation);

            Assert.AreEqual(5, folderDiff.Diffs.Count);
        }
    }
}
