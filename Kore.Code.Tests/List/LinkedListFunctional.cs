using System;
using Kore.Code.List;
using Kore.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Code.Tests.List
{
    public abstract class LinkedListFunctional
    {
        protected IList<int> List;

        #region Indexer

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void IndexerThrowsExceptionOnEmptyCollection()
        {
            List[0] = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerThrowsExceptionOnPositiveOutOfBoundsIndex()
        {
            List.Add(1);

            List[2] = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerThrowsExceptionOnNegativeOutOfBoundsIndex()
        {
            List.Add(1);

            List[-1] = 0;
        }

        [TestMethod]
        public void IndexerReturnsItemValueAtIndex()
        {
            List.Add(1);

            Assert.AreEqual(1, List[0]);
        }

        [TestMethod]
        public void IndexerSetsItemValueAtIndex()
        {
            List.Add(1);
            List[0] = 2;

            Assert.AreEqual(2, List[0]);
        }

        #endregion

        #region Contains

        [TestMethod]
        public void ContainsReturnsFalseWhenCollectionIsEmpty()
        {
            Assert.AreEqual(0, List.Count);

            Assert.IsFalse(List.Contains(3));
        }

        [TestMethod]
        public void ContainsReturnsFalseWhenItemWasNotFoundInCollection()
        {
            List.Add(1);

            Assert.IsFalse(List.Contains(2));
        }

        [TestMethod]
        public void ContainsReturnsTrueWhenItemWasFoundInCollection()
        {
            List.Add(1);

            Assert.IsTrue(List.Contains(1));
        }

        #endregion

        #region Add

        [TestMethod]
        public void AddAppendsItemAtEnd()
        {
            List.Add(1);
            List.Add(2);

            Assert.AreEqual(2, List[1]);
        }

        [TestMethod]
        public void AddIncrementsCount()
        {
            Assert.AreEqual(0, List.Count);

            List.Add(1);

            Assert.AreEqual(1, List.Count);
        }

        #endregion

        #region Insert

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void InsertAtThrowsExceptionOnNegativeOutOfBoundsIndex()
        {
            List.Insert(1, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void InsertAtThrowsExceptionOnPositiveOutOfBoundsIndex()
        {
            List.Add(1);
            List.Insert(1, 2);
        }

        [TestMethod]
        public void InsertAtIncrementsCount()
        {
            Assert.AreEqual(0, List.Count);

            List.Insert(1, 0);

            Assert.AreEqual(1, List.Count);
        }

        [TestMethod]
        public void InsertAtZeroInsertsAtBeginning()
        {
            Assert.AreEqual(0, List.Count);

            List.Insert(1, 0);
            List.Insert(2, 0);

            Assert.AreEqual(2, List[0]);
        }

        #endregion

        #region RemoveAt

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void RemoveAtThrowsExceptionOnOutOfBoundsIndex()
        {
            List.Add(1);

            List.RemoveAt(2);
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void RemoveAtThrowsExceptionIfCollectionIsEmpty()
        {
            List.RemoveAt(0);
        }

        [TestMethod]
        public void RemoveAtRemovesFirstElementWhenIndexIsZero()
        {
            List.Add(1);
            List.Add(2);

            List.RemoveAt(0);

            Assert.AreEqual(2, List[0]);
        }

        [TestMethod]
        public void RemoveAtRemovesDecrementsCount()
        {
            List.Add(1);
            List.Add(2);

            Assert.AreEqual(2, List.Count);

            List.RemoveAt(0);

            Assert.AreEqual(1, List.Count);
        }

        [TestMethod]
        public void RemoveAtRemovesLastItem()
        {
            List.Add(1);
            List.Add(2);

            List.RemoveAt(1);

            Assert.AreEqual(1, List.Count);
            Assert.IsFalse(List.Contains(2));
        }

        [TestMethod]
        public void RemoveAtRemovesItemInTheMiddle()
        {
            List.Add(1);
            List.Add(2);
            List.Add(3);

            List.RemoveAt(1);

            Assert.AreEqual(2, List.Count);
            Assert.IsFalse(List.Contains(2));
        }

        [TestMethod]
        public void RemoveAtPrevervesConsistencyAfterTotalRemoval()
        {
            List.Add(1);
            List.Add(2);

            List.RemoveAt(0);
            List.RemoveAt(0);

            Assert.AreEqual(0, List.Count);
            Assert.IsFalse(List.Contains(1));
            Assert.IsFalse(List.Contains(2));

            List.Add(3);
            List.Add(4);

            Assert.AreEqual(2, List.Count);
            Assert.IsFalse(List.Contains(1));
            Assert.IsFalse(List.Contains(2));
            
            Assert.IsTrue(List.Contains(3));
            Assert.IsTrue(List.Contains(4));

            Assert.AreEqual(3, List[0]);
            Assert.AreEqual(4, List[1]);
        }

        #endregion
    }
}
