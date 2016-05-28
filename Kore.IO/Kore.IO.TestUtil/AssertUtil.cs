using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Kore.IO.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.IO.TestUtil
{
    public static class AssertUtil
    {
        public static void AssertFileListsAreEqual(List<IKoreFileInfo> expectedFileList, List<IKoreFileInfo> actualFileList)
        {
            Assert.AreEqual(expectedFileList.Count, actualFileList.Count);

            foreach (IKoreFileInfo expectedFile in expectedFileList)
                Assert.IsTrue(actualFileList.Exists(fileInfo => fileInfo.FullName.Equals(expectedFile.FullName)));
        }
    }
}
