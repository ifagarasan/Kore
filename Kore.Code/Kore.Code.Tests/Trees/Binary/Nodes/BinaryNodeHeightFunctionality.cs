﻿using System;

using Kore.Code.Trees.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Code.Exceptions;
using Kore.Code.Node.Builders;

namespace Kore.Code.Tests.Tree.Binary.Nodes
{
    [TestClass]
    public class BinaryNodeHeightFunctionality
    {
        BinaryNodeBuilder nodeBuilder;

        [TestInitialize]
        public virtual void SetUp()
        {
            nodeBuilder = new BinaryNodeBuilder();
        }


        [TestMethod]
        public void HeightReturnsZeroForNil()
        {
            Assert.AreEqual(0, nodeBuilder.Nil.Height);
        }

        [TestMethod]
        public void HeightReturnsZeroForSingleNode()
        {
            var node = nodeBuilder.BuildNode();

            Assert.AreEqual(0, node.Height);
        }

        [TestMethod]
        public void HeightReturnsMaxNumberOfEdgesInSubtree()
        {
            var root = nodeBuilder.BuildNode();
            root.Left = nodeBuilder.BuildNode();
            root.Left.Left = nodeBuilder.BuildNode();
            root.Right = nodeBuilder.BuildNode();

            Assert.AreEqual(2, root.Height);
        }
    }
}