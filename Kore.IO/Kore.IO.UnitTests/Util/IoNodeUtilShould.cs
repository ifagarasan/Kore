using System;
using System.IO;
using Kore.Dev.Util;
using Kore.IO.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using static Kore.IO.Util.IoNode;

namespace Kore.IO.UnitTests.Util
{
    [TestClass]
    public class IoNodeUtilShould
    {
        Mock<IKoreIoNodeInfo> mockSourceNodeInfo;
        Mock<IKoreIoNodeInfo> mockParentNodeInfo;

        [TestInitialize]
        public void Setup()
        {
            mockSourceNodeInfo = new Mock<IKoreIoNodeInfo>();
            mockParentNodeInfo = new Mock<IKoreIoNodeInfo>();
        }

        #region Relative Path

        [TestMethod]
        public void ReturnRelativePathToTheParentOnRelativePath()
        {
            var parent = "C:\\music";
            var relative = "favorite\\song.mp3";
            var source = Path.Combine(parent, relative);

            mockSourceNodeInfo.Setup(m => m.FullName).Returns(source);
            mockParentNodeInfo.Setup(m => m.FullName).Returns(parent);

            var result = RelativePath(mockSourceNodeInfo.Object, mockParentNodeInfo.Object);

            Assert.AreEqual(relative, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateSourceOnRelativePath()
        {
            RelativePath(null, null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateParentOnRelativePath()
        {
            RelativePath(mockSourceNodeInfo.Object, null);
        }

        [TestMethod]
        public void ReturnEmptyStringIfSourceIsNotUnderParentOnRelativePath()
        {
            var parent = "C:\\favorite\\music\\rock";
            var source = "C:\\song.mp3";

            mockSourceNodeInfo.Setup(m => m.FullName).Returns(source);
            mockParentNodeInfo.Setup(m => m.FullName).Returns(parent);

            var result = RelativePath(mockSourceNodeInfo.Object, mockParentNodeInfo.Object);

            Assert.AreEqual(string.Empty, result);
        }

        #endregion
    }
}
