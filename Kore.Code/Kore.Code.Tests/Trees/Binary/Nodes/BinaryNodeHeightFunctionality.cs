using Kore.Code.Node.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Code.Tests.Trees.Binary.Nodes
{
    [TestClass]
    public class BinaryNodeHeightFunctionality
    {
        BinaryNodeBuilder _nodeBuilder;

        [TestInitialize]
        public virtual void SetUp()
        {
            _nodeBuilder = new BinaryNodeBuilder();
        }


        [TestMethod]
        public void HeightReturnsZeroForNil()
        {
            Assert.AreEqual(0, _nodeBuilder.Nil.Height);
        }

        [TestMethod]
        public void HeightReturnsZeroForSingleNode()
        {
            var node = _nodeBuilder.BuildNode();

            Assert.AreEqual(0, node.Height);
        }

        [TestMethod]
        public void HeightReturnsMaxNumberOfEdgesInSubtree()
        {
            var root = _nodeBuilder.BuildNode();
            root.Left = _nodeBuilder.BuildNode();
            root.Left.Left = _nodeBuilder.BuildNode();
            root.Right = _nodeBuilder.BuildNode();

            Assert.AreEqual(2, root.Height);
        }
    }
}
