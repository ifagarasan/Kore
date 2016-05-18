using System;
using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using KoreCode;
using KoreCode.Exceptions;
using KoreCode.Util;
using KoreCode.Traversals;
using KoreCode.Trees.Binary;
using System.Collections.Generic;
using KoreCode.Node.Builders;
using Moq;

namespace KoreCodeTest.Nodes.Builders
{
    [TestClass]
    public class RedBlackNodeBuilderFunctionality: BinaryNodeBuilderFunctionality
    {
        [TestInitialize]
        public override void Setup()
        {
            NodeBuilder = new RedBlackNodeBuilder();
        }

        #region Nil

        [TestMethod]
        public void NilIsBlack()
        {
            Assert.AreEqual(Color.Black, NodeBuilder.Nil.Color);
        }

        #endregion

        #region BuildNode

        [TestMethod]
        public void BuildNodeCreatesRedNode()
        {
            var node = NodeBuilder.BuildNode();
            Assert.AreEqual(Color.Red, node.Color);
        }

        #endregion
    }
}
