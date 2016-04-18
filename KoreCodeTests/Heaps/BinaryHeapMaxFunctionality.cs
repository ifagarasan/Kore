using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoreCode;
using KoreCode.Heaps;
using KoreCode.Util;

namespace KoreCodeTests
{
    [TestClass]
    public class BinaryHeapMaxFunctionality: BinaryHeapFunctionality
    {
        [TestInitialize]
        public override void SetUp()
        {
            heap = new BinaryHeap<int>(100, HeapType.Max);
        }

        [TestMethod]
        public override void InsertPerformsHeapifyUpToTheRoot()
        {
            InsertPerformsHeapifyUpToTheRoot(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        }

        [TestMethod]
        public override void InsertReturnsCorrectIndex()
        {
            InsertReturnsCorrectIndex(new int[] { 2, 3, 1, 4, 5, 8, 7, 9, 6 });
        }
    }
}
