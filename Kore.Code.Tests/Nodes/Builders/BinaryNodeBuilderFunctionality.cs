﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kore.Code.Util;
using Kore.Code.Trees.Binary;
using Kore.Code.Node.Builders;

namespace Kore.Code.Tests.Nodes.Builders
{
    [TestClass]
    public class BinaryNodeBuilderFunctionality
    {
        public BinaryNodeBuilder NodeBuilder { get; protected set; }

        [TestInitialize]
        public virtual void Setup()
        {
            NodeBuilder = new BinaryNodeBuilder();
        }

        #region Nil

        [TestMethod]
        public void NilIsDefinedAtInitialisation()
        {
            Assert.IsNotNull(NodeBuilder.Nil);
        }

        [TestMethod]
        public void NilIsHasParentLeftAndRightPointingBackToItself()
        {
            AssertDecorations(NodeBuilder.Nil);
        }

        private void AssertDecorations(IBinaryNode node)
        {
            Assert.AreSame(NodeBuilder.Nil, node.Parent);
            Assert.AreSame(NodeBuilder.Nil, node.Left);
            Assert.AreSame(NodeBuilder.Nil, node.Right);
        }

        [TestMethod]
        public void NilIsHasDefaultKey()
        {
            Assert.AreEqual(Types.GetDefaultValue(NodeBuilder.Nil.Key.GetType()), NodeBuilder.Nil.Key);
        }

        #endregion

        #region BuildNode

        [TestMethod]
        public void BuildNodeCreatesANodeWithSpecifiedKey()
        {
            var key = 32;
            var node = NodeBuilder.BuildNode(key);
            Assert.AreEqual(key, node.Key);
        }

        [TestMethod]
        public void BuildNodeCreatesANodeHavingParentLeftAndRightNil()
        {
            var node = NodeBuilder.BuildNode();
            AssertDecorations(node);
        }

        #endregion
    }
}
