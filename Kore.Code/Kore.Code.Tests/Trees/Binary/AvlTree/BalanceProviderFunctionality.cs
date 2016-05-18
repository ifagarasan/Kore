using Kore.Code.Exceptions;
using Kore.Code.Trees.Binary;
using Kore.Code.Trees.Binary.AvlTree;
using Kore.Code.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Kore.Code.Node.Builders;

namespace Kore.Code.Tests.Tree.Binary.AvlTree
{
    [TestClass]
    public class BalanceProviderFunctionality
    {
        protected BinaryNodeBuilder nodeBuilder;

        [TestInitialize]
        public virtual void SetUp()
        {
            nodeBuilder = new BinaryNodeBuilder();
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsZeroForNil()
        {
            Assert.AreEqual(0, BalanceProvider.GetBalanceOffset(nodeBuilder.Nil));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsZeroForSingleNode()
        {
            Assert.AreEqual(0, BalanceProvider.GetBalanceOffset(nodeBuilder.BuildNode()));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsZeroForNodeWithSubtreesOfEqualHeights()
        {
            var node = nodeBuilder.BuildNode();
            node.Left = nodeBuilder.BuildNode();
            node.Right = nodeBuilder.BuildNode();

            Assert.AreEqual(0, BalanceProvider.GetBalanceOffset(node));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsTheDifferenceAsAPositiveValueIfLeftHeightIfLargerThanRightHeight()
        {
            var node = nodeBuilder.BuildNode();
            node.Left = nodeBuilder.BuildNode();
            node.Left.Left = nodeBuilder.BuildNode();
            node.Left.Left.Left = nodeBuilder.BuildNode();
            node.Right = nodeBuilder.BuildNode();

            Assert.AreEqual(2, BalanceProvider.GetBalanceOffset(node));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsTheDifferenceAsANegativeValueIfLeftHeightIfSmallerThanRightHeight()
        {
            var node = nodeBuilder.BuildNode();
            node.Right = nodeBuilder.BuildNode();
            node.Right.Right = nodeBuilder.BuildNode();
            node.Right.Right.Right = nodeBuilder.BuildNode();
            node.Left = nodeBuilder.BuildNode();

            Assert.AreEqual(-2, BalanceProvider.GetBalanceOffset(node));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsNodeHeightIfRightIsNil()
        {
            var node = nodeBuilder.BuildNode();
            node.Left = nodeBuilder.BuildNode();
            node.Left.Left = nodeBuilder.BuildNode();

            Assert.AreEqual(2, BalanceProvider.GetBalanceOffset(node));
        }

        [TestMethod]
        public void GetBalanceOffsetReturnsMinusNodeHeightIfLeftIsNil()
        {
            var node = nodeBuilder.BuildNode();
            node.Right = nodeBuilder.BuildNode();
            node.Right.Right = nodeBuilder.BuildNode();

            Assert.AreEqual(-2, BalanceProvider.GetBalanceOffset(node));
        }
    }
}
