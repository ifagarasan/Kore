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
        [TestMethod]
        public void CallsFileRetrieverGetFiles()
        {
            Mock<IFileRetriever> mockFileRetriever = new Mock<IFileRetriever>();
            FileScanner fileScanner = new FileScanner(mockFileRetriever.Object);

            string folder = "TestFolderDeep";

            mockFileRetriever.Setup(m => m.GetFiles(It.IsAny<string>()));

            fileScanner.Scan(folder);

            mockFileRetriever.Verify(m => m.GetFiles(folder));
        }
    }
}
