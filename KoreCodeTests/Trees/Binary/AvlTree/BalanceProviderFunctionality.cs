using KoreCode.Exceptions;
using KoreCode.Trees.Binary;
using KoreCode.Trees.Binary.AvlTree;
using KoreCode.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KoreCodeTests.Tree.Binary.AvlTree
{
    [TestClass]
    public class BalanceProviderFunctionality
    {
        //TODO: introduce the concept of a node builder - Create, Build, Nil
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
        public void GetBalanceOffsetReturnsZeroForNil()
        {
            Assert.AreEqual(0, BalanceProvider.GetBalanceOffset(Nil));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsZeroForSingleNode()
        {
            Assert.AreEqual(0, BalanceProvider.GetBalanceOffset(BuildNode()));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsZeroForNodeWithSubtreesOfEqualHeights()
        {
            var node = BuildNode();
            node.Left = BuildNode();
            node.Right = BuildNode();

            Assert.AreEqual(0, BalanceProvider.GetBalanceOffset(node));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsTheDifferenceAsAPositiveValueIfLeftHeightIfLargerThanRightHeight()
        {
            var node = BuildNode();
            node.Left = BuildNode();
            node.Left.Left = BuildNode();
            node.Left.Left.Left = BuildNode();
            node.Right = BuildNode();

            Assert.AreEqual(2, BalanceProvider.GetBalanceOffset(node));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsTheDifferenceAsANegativeValueIfLeftHeightIfSmallerThanRightHeight()
        {
            var node = BuildNode();
            node.Right = BuildNode();
            node.Right.Right = BuildNode();
            node.Right.Right.Right = BuildNode();
            node.Left = BuildNode();

            Assert.AreEqual(-2, BalanceProvider.GetBalanceOffset(node));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsNodeHeightIfRightIsNil()
        {
            var node = BuildNode();
            node.Left = BuildNode();
            node.Left.Left = BuildNode();

            Assert.AreEqual(2, BalanceProvider.GetBalanceOffset(node));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsMinusNodeHeightIfLeftIsNil()
        {
            var node = BuildNode();
            node.Right = BuildNode();
            node.Right.Right = BuildNode();

            Assert.AreEqual(-2, BalanceProvider.GetBalanceOffset(node));
        }
    }
}
