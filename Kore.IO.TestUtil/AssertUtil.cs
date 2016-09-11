using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.IO.TestUtil
{
    public static class AssertUtil
    {
        public static void AssertIKoreFileInfoListsAreEqual(List<IKoreFileInfo> expectedFileList, IList<IKoreFileInfo> actualFileList)
        {
            Assert.AreEqual(expectedFileList.Count, actualFileList.Count);

            foreach (var expectedFileInfo in expectedFileList)
            {
                var expectedFileName = expectedFileInfo.FullName.ToLower();
                var found = false;

                foreach (var actualFileInfo in actualFileList)
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
