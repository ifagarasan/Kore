using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Code;
using Kore.Code.Heaps;
using Kore.Code.Util;

namespace Kore.Code.Tests
{
    [TestClass]
    public abstract class BinaryHeapFunctionality
    {
        protected BinaryHeap<int, object> heap;

        [TestInitialize]
        public abstract void SetUp();

        #region Init

        [TestMethod]
        public void CapacityIsThatPassedInViaTheConstructor()
        {
            int capacity = 3;
            heap = new BinaryHeap<int, object>(capacity);

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

            Assert.AreEqual(3, heap.Root.Key);
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
                Assert.AreEqual(insertValues[i], heap.Root.Key);
            }
        }

        protected void InsertReturnsCorrectIndex(int[] insertValues)
        {
            for (int i = 0; i < insertValues.Length; ++i)
            {
                int index = heap.Insert(insertValues[i]);
                Assert.AreEqual(insertValues[i], heap[index].Key);
            }
        }

        [TestMethod]
        public void InsertAllowsInsertingUpToCapacity()
        {
            heap = new BinaryHeap<int, object>(3);

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

            heap = new BinaryHeap<int, object>(input);

            Exchange<HeapItem<int, object>>.ArrayExchange(heap.Array, 1, 2);

            Assert.IsFalse(heap.IsHeap());
        }

        #endregion

        #region Contains

        [TestMethod]
        public void ContainsReturnsFalseOnEmptyCollection()
        {
            Assert.IsFalse(heap.ContainsKey(1));
        }

        [TestMethod]
        public void ContainsReturnsTrueIfItemWasFound()
        {
            int[] input = new int[] { 1, 4, 2, 7, 6 };
            heap.Insert(input);

            for (int i = 0; i < input.Length; ++i)
                Assert.IsTrue(heap.ContainsKey(input[i]));
        }

        [TestMethod]
        public void ContainsReturnsFalseIfItemWasNotFound()
        {
            int[] input = new int[] { 1, 4, 2, 7, 6 };
            heap.Insert(input);

            for (int i = 10; i < 20; ++i)
                Assert.IsFalse(heap.ContainsKey(i));
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
        public void RemoveLeadsToItemBeingNotFound()
        {
            int[] input = new int[] { 1, 4, 2, 7, 6 };
            heap.Insert(input);

            for (int i = 0; i < input.Length; ++i)
            {
                int itemToRemove = heap.Root.Key;
                heap.Remove(1);
                Assert.IsFalse(heap.ContainsKey(itemToRemove));
            }
        }

        [TestMethod]
        public void ExtractRootRemovesElementAtTop()
        {
            int[] input = new int[] { 1, 4, 2, 7, 6 };
            heap.Insert(input);

            for (int i = 0; i < input.Length; ++i)
            {
                int keyToRemove = heap.Root.Key;
                heap.ExtractRoot();
                Assert.IsFalse(heap.ContainsKey(keyToRemove));
            }
        }

        #endregion
    }
}
