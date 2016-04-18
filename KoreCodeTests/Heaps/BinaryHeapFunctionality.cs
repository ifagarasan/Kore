﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoreCode;
using KoreCode.Heaps;
using KoreCode.Util;

namespace KoreCodeTests
{
    [TestClass]
    public abstract class BinaryHeapFunctionality
    {
        protected BinaryHeap<int> heap;

        [TestInitialize]
        public abstract void SetUp();

        #region Init

        [TestMethod]
        public void CapacityIsThatPassedInViaTheConstructor()
        {
            int capacity = 3;
            heap = new BinaryHeap<int>(capacity);

            Assert.AreEqual(capacity, heap.Capacity);
        }

        [TestMethod]
        public void CountIsInitiallyZero()
        {
            Assert.AreEqual(0, heap.Count);
        }

        [TestMethod]
        public void IsEmptyInitiallyTrue()
        {
            Assert.IsTrue(heap.IsEmpty);
        }

        [TestMethod]
        public void IsFullInitiallyFalse()
        {
            Assert.IsFalse(heap.IsFull);
        }

        #endregion

        #region IsEmpty

        [TestMethod]
        public void IsEmptyReturnsTrueIfCountIsZero()
        {
            Assert.IsTrue(heap.IsEmpty);
        }

        [TestMethod]
        public void IsEmptyReturnsFalseIfCountIsNotZero()
        {
            heap.Insert(1);

            Assert.IsFalse(heap.IsEmpty);
        }

        #endregion

        #region HasChildren

        [TestMethod]
        public void HasChildrenReturnsFalseIfANodeHasNoChildren()
        {
            heap.Insert(1);

            Assert.IsFalse(heap.HasChildren(1));
        }

        [TestMethod]
        public void HasChildrenReturnsTrueIfTheNodeHasAtLeastALeftChild()
        {
            heap.Insert(new int[] { 1, 2 });

            Assert.IsTrue(heap.HasChildren(1));
        }

        #endregion

        #region Insert

        [TestMethod]
        public void InsertIncrementsCount()
        {
            heap.Insert(3);

            Assert.AreEqual(1, heap.Count);
        }

        [TestMethod]
        public void InsertSetsTheRootIfCountBecomesOne()
        {
            heap.Insert(3);

            Assert.AreEqual(3, heap.Root);
        }

        [TestMethod]
        public void InsertReturnsFinalIndex()
        {
            Assert.AreEqual(1, heap.Insert(3));
        }

        protected void InsertPerformsHeapifyUpToTheRoot(int[] insertValues)
        {
            for (int i = 0; i < insertValues.Length; ++i)
            {
                heap.Insert(insertValues[i]);
                Assert.AreEqual(insertValues[i], heap.Root);
            }
        }

        protected void InsertReturnsCorrectIndex(int[] insertValues)
        {
            for (int i = 0; i < insertValues.Length; ++i)
            {
                int index = heap.Insert(insertValues[i]);
                Assert.AreEqual(insertValues[i], heap[index]);
            }
        }

        [TestMethod]
        public void InsertAllowsInsertingUpToCapacity()
        {
            heap = new BinaryHeap<int>(3);

            for (int i = 1; i <= 3; ++i)
                heap.Insert(i);

            Assert.IsTrue(heap.IsFull);
        }

        [TestMethod]
        public abstract void InsertPerformsHeapifyUpToTheRoot();

        [TestMethod]
        public abstract void InsertReturnsCorrectIndex();

        #endregion

        #region IsHeap

        [TestMethod]
        public void IsHeapReturnsTrueForEmptyCollection()
        {
            Assert.IsTrue(heap.IsHeap());
        }

        [TestMethod]
        public void IsHeapReturnsTrueAfterInsertThatIncludesHeapifyUp()
        {
            int[] input = new int[] { 1, 5, 4, 2, 3 };
            heap.Insert(input);

            Assert.IsTrue(heap.IsHeap());
        }

        [TestMethod]
        public void IsHeapReturnsFalseIfTheHeapConditionIsBroken()
        {
            int[] input = new int[] { 1, 5, 4, 2, 3 };

            heap = new BinaryHeap<int>(input);

            ArrayOps<int>.Exchange(heap.Array, 1, 2);

            Assert.IsFalse(heap.IsHeap());
        }

        #endregion

        #region Contains

        [TestMethod]
        public void ContainsReturnsFalseOnEmptyCollection()
        {
            Assert.IsFalse(heap.Contains(1));
        }

        [TestMethod]
        public void ContainsReturnsTrueIfItemWasFound()
        {
            int[] input = new int[] { 1, 4, 2, 7, 6 };
            heap.Insert(input);

            for (int i = 0; i < input.Length; ++i)
                Assert.IsTrue(heap.Contains(input[i]));
        }

        [TestMethod]
        public void ContainsReturnsFalseIfItemWasNotFound()
        {
            int[] input = new int[] { 1, 4, 2, 7, 6 };
            heap.Insert(input);

            for (int i = 10; i < 20; ++i)
                Assert.IsFalse(heap.Contains(i));
        }

        #endregion

        #region Remove

        [TestMethod]
        public void RemoveDecrementsCount()
        {
            heap.Insert(3);
            heap.Remove(1);

            Assert.AreEqual(0, heap.Count);
        }

        [TestMethod]
        public void RemoveEnsuresContainsTheItemIsNotFound()
        {
            int[] input = new int[] { 1, 4, 2, 7, 6 };
            heap.Insert(input);

            for (int i = 0; i < input.Length; ++i)
            {
                int itemToRemove = heap.Root;
                heap.Remove(1);
                Assert.IsFalse(heap.Contains(itemToRemove));
            }
        }

        [TestMethod]
        public void PopRemovesElementAtTop()
        {
            int[] input = new int[] { 1, 4, 2, 7, 6 };
            heap.Insert(input);

            for (int i = 0; i < input.Length; ++i)
            {
                int itemToRemove = heap.Root;
                heap.Pop();
                Assert.IsFalse(heap.Contains(itemToRemove));
            }
        }

        #endregion
    }
}
