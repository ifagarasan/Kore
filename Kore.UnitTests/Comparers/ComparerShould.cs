using Kore.Comparers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.UnitTests.Comparers
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

        [TestMethod]
        public void ReturnTrueIfValue1IsLargerThanValue2()
        {
            Assert.IsTrue(Comparer<int>.LargerThan(1, 0));
        }

        [TestMethod]
        public void ReturnFalseIfValue1IsNotLargerThanValue2()
        {
            Assert.IsFalse(Comparer<int>.LargerThan(0, 1));
        }

        [TestMethod]
        public void ReturnTrueIfValue1EqualsValue2()
        {
            Assert.IsTrue(Comparer<int>.Equal(1, 1));
        }

        [TestMethod]
        public void ReturnFalseIfValue1DoesNotEqualValue2()
        {
            Assert.IsFalse(Comparer<int>.Equal(1, 0));
        }

        [TestMethod]
        public void ReturnTrueIfValue1IsSmallerThanValue2()
        {
            Assert.IsTrue(Comparer<int>.SmallerThan(0, 1));
        }

        [TestMethod]
        public void ReturnFalseIfValue1IsNotSmallerThanValue2()
        {
            Assert.IsFalse(Comparer<int>.SmallerThan(0, 0));
        }
    }
}
