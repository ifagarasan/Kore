using Kore.Code.Heaps;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Code.Tests.Heaps
{
    [TestClass]
    public class BinaryHeapMinFunctionality : BinaryHeapFunctionality
    {
        [TestInitialize]
        public override void SetUp()
        {
            heap = new BinaryHeapMin<int, object>(100);
        }

        [TestMethod]
        public override void InsertPerformsHeapifyUpToTheRoot()
        {
            InsertPerformsHeapifyUpToTheRoot(new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 });
        }

        [TestMethod]
        public override void InsertReturnsCorrectIndex()
        {
            InsertReturnsCorrectIndex(new int[] { 2, 3, 1, 4, 5, 8, 7, 9, 6 });
        }
    }
}
