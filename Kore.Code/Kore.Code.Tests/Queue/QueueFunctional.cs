using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kore.Code.Queue;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Code.Exceptions;

namespace Kore.Code.Tests.Queue
{
    [TestClass]
    public class QueueFunctional
    {
        Queue<int> queue;

        [TestInitialize]
        public void SetUp()
        {
            queue = new Queue<int>();
        }

        [TestMethod]
        public void InitialCountIsZero()
        {
            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public void NewlyAddedItemIncrementsCount()
        {
            Assert.AreEqual(0, queue.Count);

            queue.Enqueue(3);

            Assert.AreEqual(1, queue.Count);
        }

        [TestMethod]
        public void DequeueRemovesElementAndHeadIsChanged()
        {
            Assert.AreEqual(0, queue.Count);

            queue.Enqueue(3);
            queue.Enqueue(2);

            int head = queue.Dequeue();

            Assert.AreEqual(head, 3);
            Assert.AreEqual(2, queue.Peek());
        }

        [TestMethod]
        public void DequeueDecrementsCount()
        {
            Assert.AreEqual(0, queue.Count);

            queue.Enqueue(3);

            Assert.AreEqual(1, queue.Count);

            queue.Dequeue();

            Assert.AreEqual(0, queue.Count);
        }

        [TestMethod]
        public void PeekDoesNotDecrementCount()
        {
            Assert.AreEqual(0, queue.Count);

            queue.Enqueue(3);

            Assert.AreEqual(1, queue.Count);

            queue.Peek();

            Assert.AreEqual(1, queue.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void DequeueOnEmptyThrowsException()
        {
            Assert.AreEqual(0, queue.Count);

            queue.Dequeue();
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void PeekOnEmptyStackThrowsException()
        {
            Assert.AreEqual(0, queue.Count);

            queue.Peek();
        }
    }
}
