using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Code;
using Kore.Code.Util;
using System.Collections;
using System.Collections.Generic;

using Kore.Code.Sort;

namespace Kore.Code.Tests.Sort
{
    [TestClass]
    public abstract class SortFunctionality
    {
        protected delegate void SortFunc(int[] input, SortDirection direction);

        protected SortFunc sortFunc;

        [TestMethod]
        public void SortProcessesCorrectlyATwoElementArray()
        {
            TestSort(new int[] { 3, 5 });
        }

        [TestMethod]
        public void SortDoesNotErrorOnEmptyArray()
        {
            TestSort(new int[] { });
        }

        [TestMethod]
        public void SortDoesNotErrorOnSingleElementArray()
        {
            TestSort(new int[] { 5 });
        }

        [TestMethod]
        public void SortCorrectlyInvertsAnArray()
        {
            TestSort(new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 });
        }

        [TestMethod]
        public void SortDoesntChangeSortedArray()
        {
            TestSort(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
        }

        protected void TestSort(int[] input)
        {
            TestSort(input, SortDirection.Ascending);
            TestSort(input, SortDirection.Descending);
        }

        private void TestSort(int[] input, SortDirection direction)
        {
            List<int> expected = new List<int>(input);

            if (direction == SortDirection.Ascending)
                expected.Sort((x, y) => x.CompareTo(y));
            else
                expected.Sort((x, y) => -x.CompareTo(y));

            sortFunc(input, direction);

            CollectionAssert.AreEqual(expected.ToArray(), input);
        }
    }
}
