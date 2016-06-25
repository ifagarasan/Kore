﻿using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using Kore.IO.Retrievers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using static Kore.IO.TestUtil.ScannerUtil;

namespace Kore.IO.UnitTests.Retrievers
{
    [TestClass]
    public class FileRetrieverShould
    {
        private List<IKoreFileInfo> _expectedFiles;
        private FileRetriever _fileRetriever;

        [TestInitialize]
        public void Setup()
        {
            _expectedFiles = new List<IKoreFileInfo>();
            _fileRetriever = new FileRetriever();
        }

        [TestMethod]
        public void ReturnsAllFilesInAFolderIncludingHidden()
        {
            AddFiles(TestFolderOneLevel, VisibleFileList, _expectedFiles);
            AddFiles(TestFolderOneLevel, HiddenFileList, _expectedFiles);

            List<IKoreFileInfo> actualFiles = _fileRetriever.GetFiles(new KoreFolderInfo(TestFolderOneLevel));
            Assert.AreEqual(_expectedFiles.Count, actualFiles.Count);
        }

        [TestMethod]
        public void ReturnsAllFilesInAFolderIncludingHiddenRecursively()
        {
            _expectedFiles = BuildDeepTestFilesList(true, true);

            List<IKoreFileInfo> actualFiles = _fileRetriever.GetFiles(new KoreFolderInfo(TestFolderDeep));

            Assert.AreEqual(_expectedFiles.Count, actualFiles.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidatesFolderOnGetFiles()
        {
            _fileRetriever.GetFiles(null);
        }

        [TestMethod]
        public void RaisesEventForEachFileFound()
        {
            var times = 0;
            var totalFileCount = 2 * (VisibleFileList.Count + HiddenFileList.Count);

            _fileRetriever.FileFound += (f) => { times++; };

            _fileRetriever.GetFiles(new KoreFolderInfo(TestFolderDeep));

            Assert.AreEqual(totalFileCount, times);
        }
    }
}
