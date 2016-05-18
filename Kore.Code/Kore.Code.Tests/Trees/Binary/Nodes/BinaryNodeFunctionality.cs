using System;

using Kore.Code.Trees.Binary;
using Kore.Code.Exceptions;
using Kore.Code.Node.Builders;
using Kore.Code.Trees;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kore.Code.Tests.Tree.Binary.Nodes
{
    [TestClass]
    public class BinaryNodeFunctionality: TreeNodeFunctionality<IBinaryNode>
    {
        protected BinaryNodeBuilder nodeBuilder;

        [TestInitialize]
        public override void SetUp()
        {
            nodeBuilder = new BinaryNodeBuilder();
        }

        protected override IBinaryNode BuildNode(int key = 0)
        {
            return nodeBuilder.BuildNode(key);
        }

        #region Uncle

        [TestMethod]
        public void UncleReturnsParentParentLeftIfParentIsRightChild()
        {
            var grandparent = BuildNode();
            var parent = BuildNode();
            var uncle = BuildNode();

            parent.Parent = grandparent;
            uncle.Parent = grandparent;
            grandparent.Left = uncle;
            grandparent.Right = parent;

            var node = BuildNode();
            node.Parent = parent;

            Assert.AreSame(uncle, node.Uncle);
        }

        [TestMethod]
        public void UncleReturnsParentParentRightIfParentIsLeftChild()
        {
            var grandparent = BuildNode();
            var parent = BuildNode();
            var uncle = BuildNode();

            parent.Parent = grandparent;
            uncle.Parent = grandparent;
            grandparent.Right = uncle;
            grandparent.Left = parent;

            var node = BuildNode();
            node.Parent = parent;

            Assert.AreSame(uncle, node.Uncle);
        }

        #endregion

        #region IsLeaf

        [TestMethod]
        public void IsLeafReturnsTrueIfLeftEqualsRight()
        {
            var root = BuildNode();
            var node = BuildNode();
            node.Parent = root;

            Assert.IsTrue(node.IsLeaf);
        }

        [TestMethod]
        public void IsLeafReturnsFalseIfLeftDoesNotEqualRight()
        {
            var parent = BuildNode();
            var node = BuildNode();
            node.Left = BuildNode();
            node.Right = BuildNode();
            node.Parent = parent;

            Assert.IsFalse(node.IsLeaf);
        }

        [TestMethod]
        public void IsLeafReturnsFalseIfNodeIsRoot()
        {
            var node = BuildNode();

            Assert.IsFalse(node.IsLeaf);
        }

        #endregion

        #region IsLeftChild

        [TestMethod]
        public void IsLeftParentNodeReturnsTrueIfParentLeftIsNode()
        {
            var parent = BuildNode();
            var node = BuildNode();

            parent.Left = node;
            node.Parent = parent;

            Assert.IsTrue(node.IsLeftChild);
        }

        [TestMethod]
        public void IsLeftParentNodeReturnsFalseIfParentLeftIsNotNode()
        {
            var parent = BuildNode();
            var node = BuildNode();

            node.Parent = parent;

            Assert.IsFalse(node.IsLeftChild);
        }

        #endregion

        #region IsRightChild

        [TestMethod]
        public void IsRightParentNodeReturnsTrueIfParentRightIsNode()
        {
            var parent = BuildNode();
            var node = BuildNode();

            parent.Right = node;
            node.Parent = parent;

            Assert.IsTrue(node.IsRightChild);
        }

        [TestMethod]
        public void IsRightParentNodeReturnsFalseIfParenRightIsNotNode()
        {
            var parent = BuildNode();
            var node = BuildNode();

            node.Parent = parent;

            Assert.IsFalse(node.IsRightChild);
        }

        #endregion

        #region Sibling

        [TestMethod]
        public void SiglingCorrectlyReturnsRightSibling()
        {
            var node = BuildNode();
            node.Left = BuildNode();
            node.Left.Parent = node;

            node.Right = BuildNode();
            node.Right.Parent = node;

            Assert.AreSame(node.Right, node.Left.Sibling);
        }

        [TestMethod]
        public void SiglingCorrectlyReturnsLeftSibling()
        {
            var node = BuildNode();
            node.Left = BuildNode();
            node.Left.Parent = node;

            node.Right = BuildNode();
            node.Right.Parent = node;

            Assert.AreSame(node.Left, node.Right.Sibling);
        }

        #endregion

        #region IsNil

        [TestMethod]
        public void IsNilReturnsTrueIfLeftRightAndParentEqualThis()
        {
            Assert.IsTrue(nodeBuilder.Nil.IsNil);
        }

        [TestMethod]
        public void IsNilReturnsFalseIfAnyofLeftRightAndParentDoesNotEqualThis()
        {
            var node = BuildNode();

            Assert.IsFalse(node.IsNil);

            node.Left = node;

            Assert.IsFalse(node.IsNil);

            node.Right = node;

            Assert.IsFalse(node.IsNil);

            node.Parent = node;

            Assert.IsTrue(node.IsNil);
        }

        #endregion

        #region IsInternalNode

        [TestMethod]
        public override void IsInternalNodeReturnsTrueIfIsNotLeafAndIsNotRoot()
        {
            RunIsInternalNodeReturnsTrueIfIsNotLeafAndIsNotRoot<BinaryNode>();
        }

        #endregion

        #region Enumerator

        [TestMethod]
        public void ForeachIncludesBothLeftAndRightNodes()
        {
            IBinaryNode[] collection = new BinaryNode[2];
            collection[0] = BuildNode();
            collection[1] = BuildNode();

            var node = BuildNode();
            node.Left = collection[0];
            node.Right = collection[1];

            int index = 0;

            foreach(IBinaryNode child in node)
            {
                Assert.AreSame(collection[index], child);
                index++;
            }

            Assert.AreEqual(collection.Length, index);
        }

        [TestMethod]
        public void ForeachIncludesBothLeftAndRightNodesWhenNil()
        {
            var node = BuildNode();

            int index = 0;

            foreach (IBinaryNode child in node)
            {
                Assert.AreSame(nodeBuilder.Nil, child);
                index++;
            }

            Assert.AreEqual(2, index);
        }

        #endregion
    }
}
