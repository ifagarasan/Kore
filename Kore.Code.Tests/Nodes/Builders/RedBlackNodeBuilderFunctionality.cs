using System;
using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Kore.Code;
using Kore.Code.Util;
using Kore.Code.Traversals;
using Kore.Code.Trees.Binary;
using System.Collections.Generic;
using Kore.Code.Node.Builders;
using Moq;

namespace Kore.Code.Tests.Nodes.Builders
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
