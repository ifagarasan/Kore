using System;
using System.IO;
using Kore.Dev.Util;
using Kore.Exceptions;
using Kore.IO.Exceptions;
using Kore.IO.Management;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kore.IO.UnitTests.Management
{
    [TestClass]
    public class FileCopierShould
    {
        private static readonly string CurrentWorkingFolder = Path.Combine(IoUtil.TestRoot, DateTime.Now.Ticks.ToString());
        private IFileCopier _fileCopier;
        Mock<IKoreFileInfo> _mockSourceInfo;
        Mock<IKoreFileInfo> _mockDestinationInfo;

        [TestInitialize]
        public void Setup()
        {
            _mockSourceInfo = new Mock<IKoreFileInfo>();
            _mockSourceInfo.Setup(m => m.Exists).Returns(true);

            _mockDestinationInfo = new Mock<IKoreFileInfo>();
            _mockDestinationInfo.Setup(m => m.Exists).Returns(true);

            _fileCopier = new FileCopier();
        }

        [TestMethod]
        [ExpectedException(typeof(NullException))]
        public void ValidatesSourceOnCopy()
        {
            _fileCopier.Copy(null, null);   
        }

        [TestMethod]
        [ExpectedException(typeof(NullException))]
        public void ValidatesDestinationOnCopy()
        {
            _fileCopier.Copy(_mockSourceInfo.Object, null);
        }

        [TestMethod]
        [ExpectedException(typeof(NodeNotFoundException))]
        public void ValidatesSourceExistsOnCopy()
        {
            _mockSourceInfo.Setup(m => m.Exists).Returns(false);

            _fileCopier.Copy(_mockSourceInfo.Object, _mockDestinationInfo.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidDestinationNodeException))]
        public void ValidatesSourceAndDestinationDontMatchOnCopy()
        {
            const string file = "src";

            _mockSourceInfo.Setup(m => m.FullName).Returns(file);
            _mockDestinationInfo.Setup(m => m.FullName).Returns(file);

            _fileCopier.Copy(_mockSourceInfo.Object, _mockDestinationInfo.Object);
        }

        [TestMethod]
        public void CopySourceFileToDestination()
        {
            var testFolder = Path.Combine(CurrentWorkingFolder, "test-single-file-copy");

            var fileName = "file1.txt";
            var sourceFile = Path.Combine(testFolder, fileName);
            IKoreFileInfo source = new KoreFileInfo(sourceFile);

            source.EnsureExists();

            using (var wr = new StreamWriter(source.FullName))
            {
                wr.Write('c');
                wr.Write('a');
                wr.Write('b');
            }

            var destinationFile = Path.Combine(testFolder, "file1_copy.txt");
            var destination = new KoreFileInfo(destinationFile);
            _fileCopier.Copy(source, destination);

            Assert.IsTrue(destination.Exists);
            Assert.AreEqual(source.Size, destination.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void CreatesDestinationFoldersOnCopy()
        {
            var mockFolderInfo = new Mock<IKoreFolderInfo>();
            mockFolderInfo.Setup(m => m.EnsureExists());

            _mockDestinationInfo.Setup(m => m.FolderInfo).Returns(mockFolderInfo.Object);

            _fileCopier.Copy(_mockSourceInfo.Object, _mockDestinationInfo.Object);

            mockFolderInfo.Verify(m => m.EnsureExists());
        }

        [TestMethod]
        public void OverridesDestinationOnCopy()
        {
            var testFolder = Path.Combine(CurrentWorkingFolder, "test-single-file-copy");

            var fileName = "file1.txt";
            var sourceFile = Path.Combine(testFolder, fileName);
            IKoreFileInfo source = new KoreFileInfo(sourceFile);

            source.EnsureExists();
            source.LastWriteTime = DateTime.Now;

            var destinationFile = Path.Combine(testFolder, "file1_copy.txt");
            var destination = new KoreFileInfo(destinationFile);

            destination.EnsureExists();
            destination.LastWriteTime = new DateTime(1989, 2, 27);

            using (var wr = new StreamWriter(source.FullName))
            {
                wr.Write('c');
                wr.Write('a');
                wr.Write('b');
            }

            
            _fileCopier.Copy(source, destination);

            Assert.AreEqual(source.LastWriteTime, destination.LastWriteTime);
        }
    }
}
