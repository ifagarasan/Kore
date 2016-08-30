using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.IO.TestUtil
{
    public static class AssertUtil
    {
        public static void AssertIKoreFileInfoListsAreEqual(List<IKoreFileInfo> expectedFileList, IList<IKoreFileInfo> actualFileList)
        {
            Assert.AreEqual(expectedFileList.Count, actualFileList.Count);

            foreach (IKoreFileInfo expectedFileInfo in expectedFileList)
            {
                string expectedFileName = expectedFileInfo.FullName.ToLower();
                bool found = false;

                foreach (IKoreFileInfo actualFileInfo in actualFileList)
                {
                    if (actualFileInfo.FullName.ToLower().Equals(expectedFileName))
                    {
                        found = true;
                        break;
                    }
                }

                Assert.IsTrue(found);
            }
        }
    }
}
