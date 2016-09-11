using Kore.Code.Stack;
using Kore.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Code.Tests.Stack
{
    [TestClass]
    public class StackFunctional
    {
        protected IStack<int> Stack;

        [TestInitialize]
        public virtual void SetUp()
        {
            Stack = new Stack<int>();
        }

        [TestMethod]
        public void InitialCountIsZero()
        {
            Assert.AreEqual(0, Stack.Count);
        }

        [TestMethod]
        public void NewlyAddedItemBecomesTop()
        {
            Stack.Push(3);

            Assert.AreEqual(3, Stack.Peek());
        }

        [TestMethod]
        public void NewlyAddedItemIncrementsCount()
        {
            Assert.AreEqual(0, Stack.Count);

            Stack.Push(3);

            Assert.AreEqual(1, Stack.Count);
        }

        [TestMethod]
        public void PopRemovesElementAndTopIsChanged()
        {
            Stack.Push(3);
            Stack.Push(2);

            var top = Stack.Pop();

            Assert.AreEqual(top, 2);
            Assert.AreEqual(3, Stack.Peek());
        }

        [TestMethod]
        public void PopDecrementsCount()
        {
            Stack.Push(3);

            Assert.AreEqual(1, Stack.Count);

            Stack.Pop();

            Assert.AreEqual(0, Stack.Count);
        }

        [TestMethod]
        public void PeekDoesNotDecrementCount()
        {
            Stack.Push(3);

            Assert.AreEqual(1, Stack.Count);

            Stack.Peek();

            Assert.AreEqual(1, Stack.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void PopOnEmptyStackThrowsException()
        {
            Assert.AreEqual(0, Stack.Count);

            Stack.Pop();
        }

        [TestMethod]
        [ExpectedException(typeof(CollectionEmptyException))]
        public void PeekOnEmptyStackThrowsException()
        {
            Assert.AreEqual(0, Stack.Count);

            Stack.Peek();
        }
    }
}
