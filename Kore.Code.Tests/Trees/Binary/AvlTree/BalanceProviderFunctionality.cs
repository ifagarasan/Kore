using Kore.Code.Node.Builders;
using Kore.Code.Trees.Binary.AvlTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Code.Tests.Trees.Binary.AvlTree
{
    [TestClass]
    public class BalanceProviderFunctionality
    {
        protected BinaryNodeBuilder NodeBuilder;

        [TestInitialize]
        public virtual void SetUp()
        {
            NodeBuilder = new BinaryNodeBuilder();
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsZeroForNil()
        {
            Assert.AreEqual(0, BalanceProvider.GetBalanceOffset(NodeBuilder.Nil));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsZeroForSingleNode()
        {
            Assert.AreEqual(0, BalanceProvider.GetBalanceOffset(NodeBuilder.BuildNode()));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsZeroForNodeWithSubtreesOfEqualHeights()
        {
            var node = NodeBuilder.BuildNode();
            node.Left = NodeBuilder.BuildNode();
            node.Right = NodeBuilder.BuildNode();

            Assert.AreEqual(0, BalanceProvider.GetBalanceOffset(node));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsTheDifferenceAsAPositiveValueIfLeftHeightIfLargerThanRightHeight()
        {
            var node = NodeBuilder.BuildNode();
            node.Left = NodeBuilder.BuildNode();
            node.Left.Left = NodeBuilder.BuildNode();
            node.Left.Left.Left = NodeBuilder.BuildNode();
            node.Right = NodeBuilder.BuildNode();

            Assert.AreEqual(2, BalanceProvider.GetBalanceOffset(node));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsTheDifferenceAsANegativeValueIfLeftHeightIfSmallerThanRightHeight()
        {
            var node = NodeBuilder.BuildNode();
            node.Right = NodeBuilder.BuildNode();
            node.Right.Right = NodeBuilder.BuildNode();
            node.Right.Right.Right = NodeBuilder.BuildNode();
            node.Left = NodeBuilder.BuildNode();

            Assert.AreEqual(-2, BalanceProvider.GetBalanceOffset(node));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsNodeHeightIfRightIsNil()
        {
            var node = NodeBuilder.BuildNode();
            node.Left = NodeBuilder.BuildNode();
            node.Left.Left = NodeBuilder.BuildNode();

            Assert.AreEqual(2, BalanceProvider.GetBalanceOffset(node));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsMinusNodeHeightIfLeftIsNil()
        {
            var node = NodeBuilder.BuildNode();
            node.Right = NodeBuilder.BuildNode();
            node.Right.Right = NodeBuilder.BuildNode();

            Assert.AreEqual(-2, BalanceProvider.GetBalanceOffset(node));
        }
    }
}
