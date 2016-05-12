﻿using KoreCode.Exceptions;
using KoreCode.Nodes.Builders;
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
        protected BinaryNodeBuilder<BinaryNode> nodeBuilder;

        [TestInitialize]
        public virtual void SetUp()
        {
            nodeBuilder = new BinaryNodeBuilder<BinaryNode>();
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
