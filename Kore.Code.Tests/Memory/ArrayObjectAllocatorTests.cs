using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Code.List.Linear;
using Kore.Code.Memory;

namespace Kore.Code.Tests.Memory
{
    [TestClass]
    public class ArrayObjectAllocatorTests
    {
        ArrayObjectAllocator<DoubleLinkedListItem<int>> allocator;
        const int AllocationSize = 5;

        [TestInitialize]
        public void SetUp()
        {
            allocator = new ArrayObjectAllocator<DoubleLinkedListItem<int>>(AllocationSize);
        }

        #region Initialisation

        [TestMethod]
        public void InitiallyTheListIsFree()
        {
            Assert.AreEqual(AllocationSize, allocator.FreeMemory);
        }

        #endregion

        #region Allocation

        [TestMethod]
        public void AllocationReturnsNonNullObject()
        {
            Assert.IsNotNull(allocator.Allocate());
        }


        [TestMethod]
        public void AllocationDecrementsFreeMemory()
        {
            Assert.IsNotNull(allocator.Allocate());
            Assert.AreEqual(AllocationSize-1, allocator.FreeMemory);
        }

        [TestMethod]
        [ExpectedException(typeof(OutOfMemoryException))]
        public void AllocationThrowsOutOfMemoryWhenNoneAvailable()
        {
            for (int i = 0; i < AllocationSize; ++i)
                allocator.Allocate();

            allocator.Allocate();
        }

        #endregion

        #region Release


        [TestMethod]
        public void ReleaseIncrementsFreeMemory()
        {
            var memory = allocator.Allocate();
            allocator.Release(memory);
            
            Assert.AreEqual(AllocationSize, allocator.FreeMemory);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "memoryObject already free")]
        public void ReleaseThrowsExceptionIfBlockIsAlreadyFree()
        {
            var memory = allocator.Allocate();
            allocator.Release(memory);

            allocator.Release(memory);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "the allocator does not contain provided memoryObject")]
        public void ReleaseThrowsExceptionIfBlockIsNotOwned()
        {
            allocator.Release(new DoubleLinkedListItem<int>());
        }

        #endregion
    }
}
