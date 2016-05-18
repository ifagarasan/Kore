using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KoreCode.List;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoreCode.Exceptions;

namespace KoreCodeTests.List
{
    public abstract class LinkedListFunctional
    {
        protected IList<int> list;

        #region Indexer

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void IndexerThrowsExceptionOnEmptyCollection()
        {
            list[0] = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerThrowsExceptionOnPositiveOutOfBoundsIndex()
        {
            list.Add(1);

            list[2] = 0;
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void IndexerThrowsExceptionOnNegativeOutOfBoundsIndex()
        {
            list.Add(1);

            list[-1] = 0;
        }

        [TestMethod]
        public void IndexerReturnsItemValueAtIndex()
        {
            list.Add(1);

            Assert.AreEqual(1, list[0]);
        }

        [TestMethod]
        public void IndexerSetsItemValueAtIndex()
        {
            list.Add(1);
            list[0] = 2;

            Assert.AreEqual(2, list[0]);
        }

        #endregion

        #region Contains

        [TestMethod]
        public void ContainsReturnsFalseWhenCollectionIsEmpty()
        {
            Assert.AreEqual(0, list.Count);

            Assert.IsFalse(list.Contains(3));
        }

        [TestMethod]
        public void ContainsReturnsFalseWhenItemWasNotFoundInCollection()
        {
            list.Add(1);

            Assert.IsFalse(list.Contains(2));
        }

        [TestMethod]
        public void ContainsReturnsTrueWhenItemWasFoundInCollection()
        {
            list.Add(1);

            Assert.IsTrue(list.Contains(1));
        }

        #endregion

        #region Add

        [TestMethod]
        public void AddAppendsItemAtEnd()
        {
            list.Add(1);
            list.Add(2);

            Assert.AreEqual(2, list[1]);
        }

        [TestMethod]
        public void AddIncrementsCount()
        {
            Assert.AreEqual(0, list.Count);

            list.Add(1);

            Assert.AreEqual(1, list.Count);
        }

        #endregion

        #region Insert

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void InsertAtThrowsExceptionOnNegativeOutOfBoundsIndex()
        {
            list.Insert(1, -1);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void InsertAtThrowsExceptionOnPositiveOutOfBoundsIndex()
        {
            list.Add(1);
            list.Insert(1, 2);
        }

        [TestMethod]
        public void InsertAtIncrementsCount()
        {
            Assert.AreEqual(0, list.Count);

            list.Insert(1, 0);

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void InsertAtZeroInsertsAtBeginning()
        {
            Assert.AreEqual(0, list.Count);

            list.Insert(1, 0);
            list.Insert(2, 0);

            Assert.AreEqual(2, list[0]);
        }

        #endregion

        #region RemoveAt

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void RemoveAtThrowsExceptionOnOutOfBoundsIndex()
        {
            list.Add(1);

            list.RemoveAt(2);
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void RemoveAtThrowsExceptionIfCollectionIsEmpty()
        {
            list.RemoveAt(0);
        }

        [TestMethod]
        public void RemoveAtRemovesFirstElementWhenIndexIsZero()
        {
            list.Add(1);
            list.Add(2);

            list.RemoveAt(0);

            Assert.AreEqual(2, list[0]);
        }

        [TestMethod]
        public void RemoveAtRemovesDecrementsCount()
        {
            list.Add(1);
            list.Add(2);

            Assert.AreEqual(2, list.Count);

            list.RemoveAt(0);

            Assert.AreEqual(1, list.Count);
        }

        [TestMethod]
        public void RemoveAtRemovesLastItem()
        {
            list.Add(1);
            list.Add(2);

            list.RemoveAt(1);

            Assert.AreEqual(1, list.Count);
            Assert.IsFalse(list.Contains(2));
        }

        [TestMethod]
        public void RemoveAtRemovesItemInTheMiddle()
        {
            list.Add(1);
            list.Add(2);
            list.Add(3);

            list.RemoveAt(1);

            Assert.AreEqual(2, list.Count);
            Assert.IsFalse(list.Contains(2));
        }

        [TestMethod]
        public void RemoveAtPrevervesConsistencyAfterTotalRemoval()
        {
            list.Add(1);
            list.Add(2);

            list.RemoveAt(0);
            list.RemoveAt(0);

            Assert.AreEqual(0, list.Count);
            Assert.IsFalse(list.Contains(1));
            Assert.IsFalse(list.Contains(2));

            list.Add(3);
            list.Add(4);

            Assert.AreEqual(2, list.Count);
            Assert.IsFalse(list.Contains(1));
            Assert.IsFalse(list.Contains(2));
            
            Assert.IsTrue(list.Contains(3));
            Assert.IsTrue(list.Contains(4));

            Assert.AreEqual(3, list[0]);
            Assert.AreEqual(4, list[1]);
        }

        #endregion
    }
}
