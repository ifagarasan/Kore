using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Kore.IO.Retrievers;
using Kore.IO.Scanners;
using Kore.IO.TestUtil;
using Kore.IO.Filters;
using Kore.IO.Management;

namespace Kore.IO.UnitTests.Scanners
{
    [TestClass]
    public class FileManagerShould
    {
        IFileManager fileManager;
        Mock<IFileCopier> mockFileCopier;

        [TestInitialize]
        public void Setup()
        {
            mockFileCopier = new Mock<IFileCopier>();
            mockFileCopier.Setup(m => m.Copy(It.IsAny<IKoreFileInfo>(), It.IsAny<IKoreFileInfo>()));

            fileManager = new FileManager(mockFileCopier.Object);
        }

        #region Init

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidatesFileCopierOnInit()
        {
            fileManager = new FileManager(null);
        }

        #endregion

        [TestMethod]
        public void CallFileCopierCopySourceToDestination()
        {
            Mock<IKoreFileInfo> mockSourceFileInfo = new Mock<IKoreFileInfo>();
            Mock<IKoreFileInfo> mockDestinationFileInfo = new Mock<IKoreFileInfo>();

            fileManager.Copy(mockSourceFileInfo.Object, mockDestinationFileInfo.Object);

            mockFileCopier.Verify(m => m.Copy(mockSourceFileInfo.Object, mockDestinationFileInfo.Object));
        }
    }
}
