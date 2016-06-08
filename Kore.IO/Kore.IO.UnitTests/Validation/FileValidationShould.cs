using Kore.IO.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kore.IO.UnitTests.Validation
{
    [TestClass]
    public class FileValidationShould
    {
        Mock<IKoreFileInfo> _mockFileInfo;

        [TestInitialize]
        public void Setup()
        {
            _mockFileInfo = new Mock<IKoreFileInfo>();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNullExceptionIfFileInfoIsNull()
        {
            IO.Validation.FileValidation.Exists(null);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ThrowFileNotFoundExceptionIfFileInfoExistsIsFalse()
        {
            _mockFileInfo.Setup(m => m.Exists).Returns(false);

            IO.Validation.FileValidation.Exists(_mockFileInfo.Object);
        }

        [TestMethod]
        public void NotToThrowIfFileInfoExistsIsTrue()
        {
            _mockFileInfo.Setup(m => m.Exists).Returns(true);

            IO.Validation.FileValidation.Exists(_mockFileInfo.Object);
        }
    }
}
