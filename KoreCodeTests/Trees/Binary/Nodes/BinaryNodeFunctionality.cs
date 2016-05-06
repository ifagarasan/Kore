using System;

using KoreCode.Trees.Binary;
using KoreCode.Exceptions;
using KoreCode.Trees;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KoreCodeTests.Tree.Binary.Nodes
{
    [TestClass]
    public class BinaryNodeFunctionality: TreeNodeFunctionality<IBinaryNode>
    {
        protected override IBinaryNode CreateNode()
        {
            return new BinaryNode();
        }

        protected override void DecorateNode(IBinaryNode node)
        {
            node.Left = node.Right = node.Parent = Nil;
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
            Assert.IsTrue(Nil.IsNil);
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
    }
}
