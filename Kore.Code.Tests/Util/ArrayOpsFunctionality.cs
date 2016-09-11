using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Code.Util;
using Kore.Exceptions;

namespace Kore.Code.Tests.Util
{
    [TestClass]
    public class ArrayOpsFunctionality
    {
        #region Exchange

        [TestMethod]
        public void ExchangeInterchangesTheItemsWhenIndex1IsSmallerThanIndex2()
        {
            var input = new int[] { 0, 1 };

            Exchange<int>.ArrayExchange(input, 0, 1);

            Assert.AreEqual(1, input[0]);
            Assert.AreEqual(0, input[1]);
        }

        [TestMethod]
        public void ExchangeInterchangesTheItemsWhenIndex1IsLargerThanIndex2()
        {
            var input = new int[] { 0, 1 };

            Exchange<int>.ArrayExchange(input, 1, 0);

            Assert.AreEqual(1, input[0]);
            Assert.AreEqual(0, input[1]);
        }

        [TestMethod]
        public void ExchangeDoesNotErrorWhenIndex1EqualsIndex2()
        {
            var input = new int[] { 0, 1 };

            Exchange<int>.ArrayExchange(input, 0, 0);

            Assert.AreEqual(0, input[0]);
            Assert.AreEqual(1, input[1]);
        }

        #endregion

        #region CopyArray

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CopyArrayThrowsIndexOutOfBoundsIfSourceIndexIsOutOfBounds()
        {
            var input = new int[] { };

            ArrayOps<int>.CopyArray(input, 0, 1, input);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CopyArrayThrowsIndexOutOfBoundsIfTargetIndexIsOutOfBounds()
        {
            var input = new int[] { 1 };

            ArrayOps<int>.CopyArray(input, 0, 1, input, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ComparisonException))]
        public void CopyArrayThrowsComparisonExceptionIfSourceStartIndexPlusLengthIsOutOfBounds()
        {
            var input = new int[] { 1 };

            ArrayOps<int>.CopyArray(input, 0, 2, input, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ComparisonException))]
        public void CopyArrayThrowsComparisonExceptionIfTargetStartIndexPlusSourceLengthIsOutOfBounds()
        {
            var input = new int[] { 1, 2, 3 };
            var target = new int[] { 1 };

            ArrayOps<int>.CopyArray(input, 0, 3, target, 0);
        }

        [TestMethod]
        public void CopyArrayCopiesAllTheSourceElementsSpecifiedByIntervalToTargetInterval()
        {
            var input = new int[] { 1, 2, 3 };
            var target = new int[] { 0, 0, 0, 0, 0 };

            ArrayOps<int>.CopyArray(input, 0, target, 1);

            var expected = new int[] { 0, 1, 2, 3, 0 };

            CollectionAssert.AreEqual(expected, target);
        }

        [TestMethod]
        public void CopyArrayCopiesTheArrayWhenLengthsAreEqual()
        {
            var input = new int[] { 1, 2, 3 };
            var target = new int[] { 0, 0, 0 };

            ArrayOps<int>.CopyArray(input, target);

            CollectionAssert.AreEqual(input, target);
        }

        [TestMethod]
        public void CopyArraySourceCountCoupledWithProperStartIndexIncludesLastItem()
        {
            var input = new int[] { 1, 2, 3 };
            var target = new int[] { 0, 0, 0 };
            var expected = new int[] { 2, 3, 0 };

            ArrayOps<int>.CopyArray(input, 1, 2, target);

            CollectionAssert.AreEqual(expected, target);
        }

        #endregion

        #region Extreme

        [TestMethod]
        public void ExtremeWithoutIndexProcessesTheWholeArray()
        {
            var input = new int[] { 1, 2, 3 };

            Assert.AreEqual(0, ArrayOps<int>.GetExtremeIndex(input, Comparers.Comparer<int>.SmallerThan));

            input = new int[] { 3, 2, 1 };

            Assert.AreEqual(2, ArrayOps<int>.GetExtremeIndex(input, Comparers.Comparer<int>.SmallerThan));
        }

        [TestMethod]
        public void ExtremeIncludesValueAtIndex()
        {
            var input = new int[] { 1, 2, 3 };

            Assert.AreEqual(0, ArrayOps<int>.GetExtremeIndex(input, 0, Comparers.Comparer<int>.SmallerThan));

            input = new int[] { 3, 2, 1 };

            Assert.AreEqual(2, ArrayOps<int>.GetExtremeIndex(input, 2, Comparers.Comparer<int>.SmallerThan));
        }

        #endregion

        #region Merge

        [TestMethod]
        public void MergeReturnsArrayOfLengthEqualToLengthSumOfInputsContainingAllElements()
        {
            var input1 = new int[] { 1, 2, 3 };
            var input2 = new int[] { 1 };
            var expected = new int[] { 1, 1, 2, 3 };

            var result = ArrayOps<int>.Merge(input1, input2, Comparers.Comparer<int>.SmallerThan);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MergeReturnsResultArrayIfProvided()
        {
            var input1 = new int[] { 1, 2, 3 };
            var input2 = new int[] { 1 };
            var result = new int[4];

            var actual = ArrayOps<int>.Merge(input1, input2, result, Comparers.Comparer<int>.SmallerThan);

            Assert.AreSame(result, actual);
        }

        [TestMethod]
        public void MergeIsAbleToProcessEmptyArrays()
        {
            var input1 = new int[] { 1, 2, 3 };
            var input2 = new int[] { };

            var result = ArrayOps<int>.Merge(input1, input2, Comparers.Comparer<int>.SmallerThan);

            Assert.AreEqual(input1.Length + input2.Length, result.Length);
        }

        [TestMethod]
        public void MergeTakesResultIndexIntoConsideration()
        {
            var input1 = new int[] { 1, 2, 3 };
            var input2 = new int[] { 1 };
            var result = new int[6];
            var expected = new int[] { 0, 1, 1, 2, 3, 0 };

            var actual = ArrayOps<int>.Merge(input1, input2, result, 1, Comparers.Comparer<int>.SmallerThan);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MergePreservesValuesWhenCalledWithSingleSourceAndTargetArray()
        {
            var input = new int[] { 2, 4, 6, 1, 3, 5 };
            var expected = new int[] { 1, 2, 3, 4, 5, 6 };

            var actual = ArrayOps<int>.Merge(input, 0, 3, 3, 3, Comparers.Comparer<int>.SmallerThan);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion

        #region Search

        [TestMethod]
        public void ArrayContainsReturnsFalseIfTheValueWasNotFound()
        {
            var input = new int[] { 1, 2, 3 };

            Assert.IsFalse(ArrayOps<int>.ArrayContains(input, 4));
        }

        [TestMethod]
        public void ArrayContainsReturnsFalseIfArrayIsEmpty()
        {
            var input = new int[] { };

            Assert.IsFalse(ArrayOps<int>.ArrayContains(input, 1));
        }

        [TestMethod]
        public void ArrayContainsReturnsTrueIfValueIsInArrayRegardlessOfPosition()
        {
            var input = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (var i = 0; i < input.Length; ++i)
                Assert.IsTrue(ArrayOps<int>.ArrayContains(input, input[i]));
        }

        #endregion
    }
}
