using System;
using Kore.Exceptions;
using Kore.IO.Exceptions;
using Kore.IO.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kore.IO.UnitTests.Management
{
    [TestClass]
    public class FileManagerShould
    {
        IFileManager _fileManager;
        Mock<IFileCopier> _mockFileCopier;

        [TestInitialize]
        public void Setup()
        {
            _mockFileCopier = new Mock<IFileCopier>();
            _mockFileCopier.Setup(m => m.Copy(It.IsAny<IKoreFileInfo>(), It.IsAny<IKoreFileInfo>()));

            _fileManager = new FileManager(_mockFileCopier.Object);
        }

        #region Init

        [TestMethod]
        [ExpectedException(typeof(NullException))]
        public void ValidatesFileCopierOnInit()
        {
            _fileManager = new FileManager(null);
        }

        #endregion

        [TestMethod]
        public void CallFileCopierCopySourceToDestination()
        {
            Mock<IKoreFileInfo> mockSourceFileInfo = new Mock<IKoreFileInfo>();
            Mock<IKoreFileInfo> mockDestinationFileInfo = new Mock<IKoreFileInfo>();

            _fileManager.Copy(mockSourceFileInfo.Object, mockDestinationFileInfo.Object);

            _mockFileCopier.Verify(m => m.Copy(mockSourceFileInfo.Object, mockDestinationFileInfo.Object));
        }
    }
}
