using Kore.Code.Heaps;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Code.Tests.Heaps
{
    [TestClass]
    public class BinaryHeapMaxFunctionality: BinaryHeapFunctionality
    {
        [TestInitialize]
        public override void SetUp()
        {
            heap = new BinaryHeapMax<int, object>(100);
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
