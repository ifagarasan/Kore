using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Comparers;

namespace Kore.UnitTests
{
    [TestClass]
    public class ComparerShould
    {
        [TestMethod]
        public void ReturnComparisonResultEqualsForTwoEqualValues()
        {
            Assert.AreEqual(ComparisonResult.Equal, Comparer<int>.Compare(1, 1));
        }

        [TestMethod]
        public void ReturnComparisonResultSmallerThanIfValue1SmallerThanValue2()
        {
            Assert.AreEqual(ComparisonResult.SmallerThan, Comparer<int>.Compare(0, 1));
        }

        [TestMethod]
        public void ReturnComparisonResultLargerThanIfValue1SmallerThanValue2()
        {
            Assert.AreEqual(ComparisonResult.LargerThan, Comparer<int>.Compare(1, 0));
        }
    }
}
