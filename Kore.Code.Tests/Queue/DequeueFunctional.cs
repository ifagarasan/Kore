using System;
using Kore.Code.Queue;
using Kore.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Code.Tests.Queue
{
    [TestClass]
    public class DequeueFunctional
    {
        Deque<int> deque;

        #region Initialisation

        [TestMethod]
        [ExpectedException(typeof(Exception), "capacity cannot be 0")]
        public void ThrowsExceptionIfCapacityIsZero()
        {
            new Deque<int>(0);
        }

        [TestMethod]
        public void CapacityReturnsInputCapacity()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(deque.Capacity, 10);
        }

        [TestMethod]
        public void InitialCountIsZero()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);
        }

        #endregion

        #region Enqueue

        [TestMethod]
        public void EnqueueIncrementsCount()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.Enqueue(3);

            Assert.AreEqual(1, deque.Count);
        }

        [TestMethod]
        public void EnqueueAddsItem()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.Enqueue(3);

            Assert.AreEqual(3, deque.Peek());
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionFullException))]
        public void EnqueueOnFullDequeThrowsException()
        {
            deque = new Deque<int>(1);

            deque.Enqueue(1);
            deque.Enqueue(2);
        }

        [TestMethod]
        public void EnqueueWrapsAround()
        {
            deque = new Deque<int>(2);

            deque.Enqueue(1);
            deque.Enqueue(2);

            deque.Dequeue();

            Assert.AreEqual(1, deque.Count);

            deque.Enqueue(3);

            Assert.AreEqual(2, deque.Count);
        }

        #endregion

        #region Peek

        [TestMethod]
        public void PeekDoesNotDecrementCount()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.Enqueue(3);
            deque.Peek();

            Assert.AreEqual(1, deque.Count);
        }

        [TestMethod]
        public void PeekReturnsElementAtFrontOfDeque()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.Enqueue(3);
            deque.Enqueue(2);
            Assert.AreEqual(3, deque.Peek());
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void PeekOnEmptyDequeThrowsException()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.Peek();
        }

        #endregion

        #region Dequeue

        [TestMethod]
        public void DequeueRemovesElementAndHeadIsChanged()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.Enqueue(3);
            deque.Enqueue(2);

            var head = deque.Dequeue();

            Assert.AreEqual(head, 3);
            Assert.AreEqual(2, deque.Peek());
        }

        [TestMethod]
        public void DequeueDecrementsCount()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.Enqueue(3);

            Assert.AreEqual(1, deque.Count);

            deque.Dequeue();

            Assert.AreEqual(0, deque.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void DequeueOnEmptyDequeThrowsException()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.Dequeue();
        }

        [TestMethod]
        public void DequeueWrapsAround()
        {
            deque = new Deque<int>(2);

            deque.Enqueue(1);
            deque.Enqueue(2);

            deque.Dequeue();

            deque.Enqueue(3);

            Assert.AreEqual(2, deque.Count);

            Assert.AreEqual(2, deque.Dequeue());
            Assert.AreEqual(3, deque.Dequeue());

            Assert.AreEqual(0, deque.Count);
        }

        #endregion

        #region Peek

        [TestMethod]
        public void PeekTailDoesNotDecrementCount()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.Enqueue(3);
            deque.PeekTail();

            Assert.AreEqual(1, deque.Count);
        }

        [TestMethod]
        public void PeekTailReturnsElementAtTailOfDeque()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.Enqueue(3);
            deque.Enqueue(2);

            Assert.AreEqual(2, deque.Count);

            Assert.AreEqual(2, deque.PeekTail());
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void PeekTailOnEmptyDequeThrowsException()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.PeekTail();
        }

        #endregion

        #region Enqueue

        [TestMethod]
        public void EnqueueFrontIncrementsCount()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.EnqueueFront(3);

            Assert.AreEqual(1, deque.Count);
        }

        [TestMethod]
        public void EnqueueFrontAddsItemInFront()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.EnqueueFront(3);
            deque.EnqueueFront(2);

            Assert.AreEqual(2, deque.Count);
            Assert.AreEqual(2, deque.Peek());
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionFullException))]
        public void EnqueueFrontOnFullDequeThrowsException()
        {
            deque = new Deque<int>(1);

            deque.EnqueueFront(1);

            deque.EnqueueFront(2);
        }

        [TestMethod]
        public void EnqueueFrontWrapsAround()
        {
            deque = new Deque<int>(2);

            deque.EnqueueFront(1);
            deque.EnqueueFront(2);

            Assert.AreEqual(2, deque.Count);
            Assert.AreEqual(2, deque.Peek());
            Assert.AreEqual(1, deque.PeekTail());
        }

        #endregion

        #region Dequeue

        [TestMethod]
        public void DequeueTailRemovesElementAndHeadIsChanged()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.Enqueue(3);
            deque.Enqueue(2);

            var tail = deque.DequeueTail();

            Assert.AreEqual(tail, 2);
            Assert.AreEqual(3, deque.Peek());
        }

        [TestMethod]
        public void DequeueTailDecrementsCount()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.Enqueue(3);

            Assert.AreEqual(1, deque.Count);

            deque.DequeueTail();

            Assert.AreEqual(0, deque.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void DequeueTailOnEmptyDequeThrowsException()
        {
            deque = new Deque<int>(10);

            Assert.AreEqual(0, deque.Count);

            deque.DequeueTail();
        }

        [TestMethod]
        public void DequeueTailWrapsAround()
        {
            deque = new Deque<int>(2);

            deque.Enqueue(1);
            deque.EnqueueFront(2);

            Assert.AreEqual(1, deque.DequeueTail());
            Assert.AreEqual(1, deque.Count);

            Assert.AreEqual(2, deque.DequeueTail());
            Assert.AreEqual(0, deque.Count);
        }

        #endregion
    }
}
