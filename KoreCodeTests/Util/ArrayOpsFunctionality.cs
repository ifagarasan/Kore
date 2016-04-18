using System;
using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using KoreCode;
using KoreCode.Exceptions;
using KoreCode.Util;

namespace KoreCodeTest.Util
{
    [TestClass]
    public class ArrayOpsFunctionality
    {
        #region Exchange

        [TestMethod]
        public void ExchangeInterchangesTheItemsWhenIndex1IsSmallerThanIndex2()
        {
            int[] input = new int[] { 0, 1 };

            ArrayOps<int>.Exchange(input, 0, 1);

            Assert.AreEqual(1, input[0]);
            Assert.AreEqual(0, input[1]);
        }

        [TestMethod]
        public void ExchangeInterchangesTheItemsWhenIndex1IsLargerThanIndex2()
        {
            int[] input = new int[] { 0, 1 };

            ArrayOps<int>.Exchange(input, 1, 0);

            Assert.AreEqual(1, input[0]);
            Assert.AreEqual(0, input[1]);
        }

        [TestMethod]
        public void ExchangeDoesNotErrorWhenIndex1EqualsIndex2()
        {
            int[] input = new int[] { 0, 1 };

            ArrayOps<int>.Exchange(input, 0, 0);

            Assert.AreEqual(0, input[0]);
            Assert.AreEqual(1, input[1]);
        }

        #endregion

        #region CopyArray

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CopyArrayThrowsIndexOutOfBoundsIfSourceIndexIsOutOfBounds()
        {
            int[] input = new int[] { };

            ArrayOps<int>.CopyArray(input, 0, 1, input);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void CopyArrayThrowsIndexOutOfBoundsIfTargetIndexIsOutOfBounds()
        {
            int[] input = new int[] { 1 };

            ArrayOps<int>.CopyArray(input, 0, 1, input, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ComparisonException))]
        public void CopyArrayThrowsComparisonExceptionIfSourceStartIndexPlusLengthIsOutOfBounds()
        {
            int[] input = new int[] { 1 };

            ArrayOps<int>.CopyArray(input, 0, 2, input, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ComparisonException))]
        public void CopyArrayThrowsComparisonExceptionIfTargetStartIndexPlusSourceLengthIsOutOfBounds()
        {
            int[] input = new int[] { 1, 2, 3 };
            int[] target = new int[] { 1 };

            ArrayOps<int>.CopyArray(input, 0, 3, target, 0);
        }

        [TestMethod]
        public void CopyArrayCopiesAllTheSourceElementsSpecifiedByIntervalToTargetInterval()
        {
            int[] input = new int[] { 1, 2, 3 };
            int[] target = new int[] { 0, 0, 0, 0, 0 };

            ArrayOps<int>.CopyArray(input, 0, target, 1);

            int[] expected = new int[] { 0, 1, 2, 3, 0 };

            CollectionAssert.AreEqual(expected, target);
        }

        [TestMethod]
        public void CopyArrayCopiesTheArrayWhenLengthsAreEqual()
        {
            int[] input = new int[] { 1, 2, 3 };
            int[] target = new int[] { 0, 0, 0 };

            ArrayOps<int>.CopyArray(input, target);

            CollectionAssert.AreEqual(input, target);
        }

        [TestMethod]
        public void CopyArraySourceCountCoupledWithProperStartIndexIncludesLastItem()
        {
            int[] input = new int[] { 1, 2, 3 };
            int[] target = new int[] { 0, 0, 0 };
            int[] expected = new int[] { 2, 3, 0 };

            ArrayOps<int>.CopyArray(input, 1, 2, target);

            CollectionAssert.AreEqual(expected, target);
        }

        #endregion

        #region Extreme

        [TestMethod]
        public void ExtremeWithoutIndexProcessesTheWholeArray()
        {
            int[] input = new int[] { 1, 2, 3 };

            Assert.AreEqual(0, ArrayOps<int>.GetExtremeIndex(input, Comparers<int>.LessThan));

            input = new int[] { 3, 2, 1 };

            Assert.AreEqual(2, ArrayOps<int>.GetExtremeIndex(input, Comparers<int>.LessThan));
        }

        [TestMethod]
        public void ExtremeIncludesValueAtIndex()
        {
            int[] input = new int[] { 1, 2, 3 };

            Assert.AreEqual(0, ArrayOps<int>.GetExtremeIndex(input, 0, Comparers<int>.LessThan));

            input = new int[] { 3, 2, 1 };

            Assert.AreEqual(2, ArrayOps<int>.GetExtremeIndex(input, 2, Comparers<int>.LessThan));
        }

        #endregion

        #region Merge

        [TestMethod]
        public void MergeReturnsArrayOfLengthEqualToLengthSumOfInputsContainingAllElements()
        {
            int[] input1 = new int[] { 1, 2, 3 };
            int[] input2 = new int[] { 1 };
            int[] expected = new int[] { 1, 1, 2, 3 };

            int[] result = ArrayOps<int>.Merge(input1, input2, Comparers<int>.LessThan);

            CollectionAssert.AreEqual(expected, result);
        }

        [TestMethod]
        public void MergeReturnsResultArrayIfProvided()
        {
            int[] input1 = new int[] { 1, 2, 3 };
            int[] input2 = new int[] { 1 };
            int[] result = new int[4];

            int[] actual = ArrayOps<int>.Merge(input1, input2, result, Comparers<int>.LessThan);

            Assert.AreSame(result, actual);
        }

        [TestMethod]
        public void MergeIsAbleToProcessEmptyArrays()
        {
            int[] input1 = new int[] { 1, 2, 3 };
            int[] input2 = new int[] { };

            int[] result = ArrayOps<int>.Merge(input1, input2, Comparers<int>.LessThan);

            Assert.AreEqual(input1.Length + input2.Length, result.Length);
        }

        [TestMethod]
        public void MergeTakesResultIndexIntoConsideration()
        {
            int[] input1 = new int[] { 1, 2, 3 };
            int[] input2 = new int[] { 1 };
            int[] result = new int[6];
            int[] expected = new int[] { 0, 1, 1, 2, 3, 0 };

            int[] actual = ArrayOps<int>.Merge(input1, input2, result, 1, Comparers<int>.LessThan);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void MergePreservesValuesWhenCalledWithSingleSourceAndTargetArray()
        {
            int[] input = new int[] { 2, 4, 6, 1, 3, 5 };
            int[] expected = new int[] { 1, 2, 3, 4, 5, 6 };

            int[] actual = ArrayOps<int>.Merge(input, 0, 3, 3, 3, Comparers<int>.LessThan);

            CollectionAssert.AreEqual(expected, actual);
        }

        #endregion

        #region Search

        [TestMethod]
        public void ArrayContainsReturnsFalseIfTheValueWasNotFound()
        {
            int[] input = new int[] { 1, 2, 3 };

            Assert.IsFalse(ArrayOps<int>.ArrayContains(input, 4));
        }

        [TestMethod]
        public void ArrayContainsReturnsFalseIfArrayIsEmpty()
        {
            int[] input = new int[] { };

            Assert.IsFalse(ArrayOps<int>.ArrayContains(input, 1));
        }

        [TestMethod]
        public void ArrayContainsReturnsTrueIfValueIsInArrayRegardlessOfPosition()
        {
            int[] input = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int i = 0; i < input.Length; ++i)
                Assert.IsTrue(ArrayOps<int>.ArrayContains(input, input[i]));
        }

        #endregion
    }
}
