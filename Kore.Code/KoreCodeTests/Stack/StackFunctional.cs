using System;

using KoreCode.Exceptions;
using KoreCode.Stack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoreCodeTests.Stack
{
    [TestClass]
    public class StackFunctional
    {
        protected IStack<int> stack;

        [TestInitialize]
        public virtual void SetUp()
        {
            stack = new Stack<int>();
        }

        [TestMethod]
        public void InitialCountIsZero()
        {
            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void NewlyAddedItemBecomesTop()
        {
            stack.Push(3);

            Assert.AreEqual(3, stack.Peek());
        }

        [TestMethod]
        public void NewlyAddedItemIncrementsCount()
        {
            Assert.AreEqual(0, stack.Count);

            stack.Push(3);

            Assert.AreEqual(1, stack.Count);
        }

        [TestMethod]
        public void PopRemovesElementAndTopIsChanged()
        {
            stack.Push(3);
            stack.Push(2);

            int top = stack.Pop();

            Assert.AreEqual(top, 2);
            Assert.AreEqual(3, stack.Peek());
        }

        [TestMethod]
        public void PopDecrementsCount()
        {
            stack.Push(3);

            Assert.AreEqual(1, stack.Count);

            stack.Pop();

            Assert.AreEqual(0, stack.Count);
        }

        [TestMethod]
        public void PeekDoesNotDecrementCount()
        {
            stack.Push(3);

            Assert.AreEqual(1, stack.Count);

            stack.Peek();

            Assert.AreEqual(1, stack.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void PopOnEmptyStackThrowsException()
        {
            Assert.AreEqual(0, stack.Count);

            stack.Pop();
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void PeekOnEmptyStackThrowsException()
        {
            Assert.AreEqual(0, stack.Count);

            stack.Peek();
        }
    }
}
