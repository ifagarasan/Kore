using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.IO.TestUtil
{
    public static class AssertUtil
    {
        public static void AssertFileListsAreEqual(List<string> expectedFileList, List<string> actualFileList)
        {
            Assert.AreEqual(expectedFileList.Count, actualFileList.Count);

            for (int i = 0; i < expectedFileList.Count; ++i)
                Assert.IsTrue(actualFileList.Contains(expectedFileList[i]));
        }
    }
}
