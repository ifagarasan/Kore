﻿using System;

using KoreCode.Trees.Binary;
using KoreCode.Trees.Binary.RedBlackTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoreCode.Exceptions;

namespace KoreCodeTests.Tree.Binary.Nodes
{
    [TestClass]
    public class RedBlackNodeFunctionality: BinaryNodeFunctionality
    {
        protected override IBinaryNode CreateNode()
        {
            return new RedBlackNode();
        }

        #region Height

        [TestMethod]
        public void HeightReturnsZeroForNil()
        {
            Assert.AreEqual(0, Nil.Height);
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
    }
}
