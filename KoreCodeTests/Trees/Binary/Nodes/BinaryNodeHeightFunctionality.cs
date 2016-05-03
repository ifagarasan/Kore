using System;

using KoreCode.Trees.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoreCode.Exceptions;

namespace KoreCodeTests.Tree.Binary.Nodes
{
    [TestClass]
    public class BinaryNodeHeightFunctionality
    {
        protected IBinaryNode Nil;

        [TestInitialize]
        public virtual void SetUp()
        {
            Nil = CreateNode();
            DecorateNode(Nil);
        }

        private IBinaryNode CreateNode()
        {
            return new BinaryNode();
        }

        private IBinaryNode BuildNode()
        {
            IBinaryNode node = CreateNode();
            DecorateNode(node);
            return node;
        }

        private void DecorateNode(IBinaryNode node)
        {
            node.Left = node.Right = node.Parent = Nil;
        }

        [TestMethod]
        public void HeightReturnsZeroForNil()
        {
            Assert.AreEqual(0, Nil.Height);
        }

        [TestMethod]
        public void HeightReturnsZeroForSingleNode()
        {
            var node = BuildNode();

            Assert.AreEqual(1, node.Height);
        }

        [TestMethod]
        public void HeightReturnsMaxNumberOfEdgesInSubtree()
        {
            var root = BuildNode();
            root.Left = BuildNode();
            root.Left.Left = BuildNode();
            root.Right = BuildNode();

            Assert.AreEqual(2, root.Height);
        }
    }
}
