using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Code.Sort;

namespace Kore.Code.Tests.Sort
{
    [TestClass]
    public class QuickSortFunctionality : SortFunctionality
    {
        public QuickSortFunctionality()
        {
            sortFunc = SelectionSort<int>.Sort;
        }

        #region MedianOfThree

        [TestMethod]
        public void MedianOfThreeReturnsLeftPlusRightOverTwo()
        {
            var input = new int[] { 1, 2, 3, 4, 5 };

            Assert.AreEqual(2, QuickSort<int>.MedianOfThree(input, 1, 3, Sort<int>.GetComparisonFunc(SortDirection.Ascending)));
            Assert.AreEqual(2, QuickSort<int>.MedianOfThree(input, 1, 4, Sort<int>.GetComparisonFunc(SortDirection.Ascending)));
        }

        [TestMethod]
        public void MedianOfThreeExchangesLeftAndMiddleIfTheyDoNotSatisfyComparisonRelation()
        {
            var input = new int[] { 1, 2, 3, 1 };

            var leftIndex = 1;
            var rightIndex = 3;

            var middleIndex = QuickSort<int>.MedianOfThree(input, leftIndex, rightIndex, Sort<int>.GetComparisonFunc(SortDirection.Descending));

            Assert.AreEqual(3, input[leftIndex]);
            Assert.AreEqual(2, input[middleIndex]);
        }

        [TestMethod]
        public void MedianOfThreeExchangesMiddleAndRightIfTheyDoNotSatisfyComparisonRelation()
        {
            var input = new int[] { 1, 4, 3, 4 };

            var leftIndex = 1;
            var rightIndex = 3;

            var middleIndex = QuickSort<int>.MedianOfThree(input, leftIndex, rightIndex, Sort<int>.GetComparisonFunc(SortDirection.Descending));

            Assert.AreEqual(4, input[middleIndex]);
            Assert.AreEqual(3, input[rightIndex]);
        }

        #endregion
    }
}
