using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using Kore.IO.Scanners;
using Kore.IO.Retrievers;
using Kore.IO.TestUtil;

namespace Kore.IO.AcceptanceTests.Scanners
{
    [TestClass]
    public class FileScannerShould
    {
        List<string> _expectedFileList;
        
        [TestInitialize]
        public void Setup()
        {
            _expectedFileList = new List<string>();
            ScannerUtil.BuildTestFilesList(_expectedFileList);
        }

        [TestMethod]
        public void FileScannerReturnsAllFiles()
        {
            FileScanner fileScanner = new FileScanner(new FileRetriever());
            List<string> fileList = fileScanner.Scan(ScannerUtil.TestFolderDeep);

            AssertUtil.AssertFileListsAreEqual(_expectedFileList, fileList);
        }
    }
}
