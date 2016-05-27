using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Kore.IO.Retrievers;
using Kore.IO.Scanners;

namespace Kore.IO.UnitTests.Scanners
{
    [TestClass]
    public class FileScannerShould
    {
        Mock<IFileRetriever> mockFileRetriever;
        FileScanner fileScanner;
        FileScanOptions fileScanOptions;

        [TestInitialize]
        public void Setup()
        {
            mockFileRetriever = new Mock<IFileRetriever>();
            fileScanner = new FileScanner(mockFileRetriever.Object);
            fileScanOptions = new FileScanOptions();
        }

        [TestMethod]
        public void CallFileRetrieverGetFiles()
        {
            string folder = "TestFolderDeep";

            mockFileRetriever.Setup(m => m.GetFiles(It.IsAny<string>(), It.IsAny<string>()));

            fileScanner.Scan(folder);

            mockFileRetriever.Verify(m => m.GetFiles(folder, fileScanOptions.SearchPattern));
        }

        [TestMethod]
        public void CallFileRetrieverGetFilesPassingScanOptionsSearchPattern()
        {   
            fileScanOptions.SearchPattern = "*.txt";

            string folder = "TestFolderDeep";

            mockFileRetriever.Setup(m => m.GetFiles(It.IsAny<string>(), It.IsAny<string>()));

            fileScanner.Scan(folder, fileScanOptions);

            mockFileRetriever.Verify(m => m.GetFiles(folder, fileScanOptions.SearchPattern));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowArgumentNullExceptionIfFileScanOptionsIsNull()
        {
            fileScanner.Scan("TestFolderDeep", null);
        }
    }
}
