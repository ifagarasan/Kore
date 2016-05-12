using System;
using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using KoreCode;
using KoreCode.Exceptions;
using KoreCode.Util;
using KoreCode.Traversals;
using KoreCode.Trees.Binary;
using System.Collections.Generic;
using Moq;
using KoreCode.Nodes.Builders;

namespace KoreCodeTest.Nodes.Builders
{
    [TestClass]
    public class BinaryNodeBuilderFunctionality
    {
        BinaryNodeBuilder<BinaryNode> nodeBuilder;

        [TestInitialize]
        public void Setup()
        {
            nodeBuilder = new BinaryNodeBuilder<BinaryNode>();
        }

        #region Nil

        [TestMethod]
        public void NilIsDefinedAtInitialisation()
        {
            Assert.IsNotNull(nodeBuilder.Nil);
        }

        [TestMethod]
        public void NilIsHasParentLeftAndRightPointingBackToItself()
        {
            AssertDecorations(nodeBuilder.Nil);
        }

        private void AssertDecorations(IBinaryNode node)
        {
            Assert.AreSame(nodeBuilder.Nil, node.Parent);
            Assert.AreSame(nodeBuilder.Nil, node.Left);
            Assert.AreSame(nodeBuilder.Nil, node.Right);
        }

        [TestMethod]
        public void NilIsHasDefaultKey()
        {
            Assert.AreEqual(Types.GetDefaultValue(nodeBuilder.Nil.Key.GetType()), nodeBuilder.Nil.Key);
        }

        #endregion

        #region BuildNode

        [TestMethod]
        public void BuildNodeCreatesANodeWithSpecifiedKey()
        {
            int key = 32;
            var node = nodeBuilder.BuildNode(key);
            Assert.AreEqual(key, node.Key);
        }

        [TestMethod]
        public void BuildNodeCreatesANodeHavingParentLeftAndRightNil()
        {
            var node = nodeBuilder.BuildNode();
            AssertDecorations(node);
        }

        #endregion
    }
}
