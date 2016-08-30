using System;
using System.Collections.Generic;
using System.IO;
using Castle.Components.DictionaryAdapter;
using Kore.Exceptions;
using Kore.IO.Scanners;
using Kore.IO.Sync;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kore.IO.UnitTests.Sync
{
    [TestClass]
    public class FolderDiffShould
    {
        Mock<IKoreFolderInfo> _mockSourceFolderInfo;
        Mock<IKoreFolderInfo> _mockDestinationFolderInfo;
        IFolderDiff _folderDiff;

        [TestInitialize]
        public void Setup()
        {
            _mockSourceFolderInfo = new Mock<IKoreFolderInfo>();
            _mockDestinationFolderInfo = new Mock<IKoreFolderInfo>();

            _folderDiff = new FolderDiff(_mockSourceFolderInfo.Object, _mockDestinationFolderInfo.Object, new List<IDiff>());
        }

        #region Init

        [TestMethod]
        [ExpectedException(typeof(NullException))]
        public void ValidatesSourceParameterOnInit()
        {
            _folderDiff = new FolderDiff(null, _mockDestinationFolderInfo.Object, new List<IDiff>());
        }

        [TestMethod]
        [ExpectedException(typeof(NullException))]
        public void ValidatesDestinationParameterOnInit()
        {
            _folderDiff = new FolderDiff(_mockSourceFolderInfo.Object, null, new List<IDiff>());
        }

        [TestMethod]
        [ExpectedException(typeof(NullException))]
        public void ValidatesDiffsParameterOnInit()
        {
            _folderDiff = new FolderDiff(_mockSourceFolderInfo.Object, _mockDestinationFolderInfo.Object, null);
        }

        #endregion

        [TestMethod]
        public void ExposeSourceFolder()
        {
            Assert.AreSame(_mockSourceFolderInfo.Object, _folderDiff.Source);
        }

        [TestMethod]
        public void ExposeDestinationFolder()
        {
            Assert.AreSame(_mockDestinationFolderInfo.Object, _folderDiff.Destination);
        }
    }
}
