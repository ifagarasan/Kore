using System;
using KoreCode.Exceptions;
using KoreCode.Node.Builders;
using KoreCode.Trees;
using KoreCode.Trees.Binary;
using KoreCode.Trees.Binary.RedBlackTree;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoreCodeTests.Tree.Binary.Nodes
{
    [TestClass]
    public class RedBlackNodeFunctionality: BinaryNodeFunctionality
    {
        [TestInitialize]
        public override void SetUp()
        {
            nodeBuilder = new RedBlackNodeBuilder();
        }

        protected override IBinaryNode BuildNode(int key = 0)
        {
            return nodeBuilder.BuildNode(key);
        }

        #region Height

        [TestMethod]
        public void HeightReturnsZeroForNil()
        {
            Assert.AreEqual(0, nodeBuilder.Nil.Height);
        }

        [TestMethod]
        public void HeightTakesNilNodeHeightAsNil()
        {
            var node = BuildNode();
            node.Color = Color.Red;

            Assert.AreEqual(1, node.Height);
        }

        [TestMethod]
        public void HeightProcessorDoesNotIncludeStartingNode()
        {
            var node = BuildNode();
            node.Color = Color.Black;

            Assert.AreEqual(1, node.Height);
        }

        #endregion

        #region IsInternalNode

        [TestMethod]
        public override void IsInternalNodeReturnsTrueIfIsNotLeafAndIsNotRoot()
        {
            RunIsInternalNodeReturnsTrueIfIsNotLeafAndIsNotRoot<RedBlackNode>();
        }

        #endregion
    }
}
