using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Kore.IO.Retrievers;
using Kore.IO.Scanners;
using Kore.IO.TestUtil;
using Kore.IO.Filters;
using static Kore.Dev.Util.IoUtil;
using Kore.IO.Management;

namespace Kore.IO.UnitTests.Scanners
{
    [TestClass]
    public class FileCopierShould
    {
        private static readonly string _currentWorkingFolder = Path.Combine(TestRoot, DateTime.Now.Ticks.ToString());

        [TestMethod]
        public void CopySourceFileToDestination()
        {
            var testFolder = Path.Combine(_currentWorkingFolder, "test-single-file-copy");

            var fileName = "file1.txt";
            var sourceFile = Path.Combine(testFolder, fileName);
            IKoreFileInfo source = new KoreFileInfo(sourceFile);

            source.EnsureExists();

            using (StreamWriter wr = new StreamWriter(source.FullName))
            {
                wr.Write('c');
                wr.Write('a');
                wr.Write('b');
            }

            IFileCopier copier = new FileCopier();

            var destinationFile = Path.Combine(testFolder, "file1_copy.txt");
            var destination = new KoreFileInfo(destinationFile);
            copier.Copy(source, destination);

            Assert.IsTrue(destination.Exists);
            Assert.AreEqual(source.Size, destination.Size);
        }
    }
}
