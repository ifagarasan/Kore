using System;

using KoreCode.Trees.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoreCode.Exceptions;

namespace KoreCodeTests.Tree.Binary.Nodes
{
    [TestClass]
    public class BinaryNodeFunctionality
    {
        protected IBinaryNode Nil;

        [TestInitialize]
        public virtual void SetUp()
        {
            Nil = CreateNode();
            DecorateNode(Nil);
        }

        protected virtual IBinaryNode CreateNode()
        {
            return new BinaryNode();
        }

        protected virtual IBinaryNode BuildNode()
        {
            IBinaryNode node = CreateNode();
            DecorateNode(node);
            return node;
        }

        protected virtual void DecorateNode(IBinaryNode node)
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

        #region Grandparent

        [TestMethod]
        public void GrandparentReturnsParentParent()
        {
            IBinaryNode grandParent = BuildNode();
            IBinaryNode parent = BuildNode();
            IBinaryNode node = CreateNode();

            parent.Parent = grandParent;
            node.Parent = parent;

            Assert.AreSame(grandParent, node.Grandparent);
        }

        #endregion

        #region IsRoot

        [TestMethod]
        public void IsRootReturnsTrueIfNodeParentIsNil()
        {
            var node = BuildNode();

            Assert.IsTrue(node.IsRoot);
        }

        [TestMethod]
        public void IsRootReturnsFalseIfNodeParentIsNotNil()
        {
            var node = BuildNode();
            node.Parent = BuildNode();

            Assert.IsFalse(node.IsRoot);
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

        #region IsInternalNode

        [TestMethod]
        public void IsInternalNodeReturnsFalseIfNodeIsRoot()
        {
            var node = BuildNode();

            Assert.IsTrue(node.IsRoot);
            Assert.IsFalse(node.IsInternalNode);
        }

        [TestMethod]
        public void IsInternalNodeReturnsFalseIfIsLeaf()
        {
            var node = BuildNode();
            var root = BuildNode();

            node.Parent = root;

            Assert.IsFalse(node.IsInternalNode);
        }

        [TestMethod]
        public void IsInternalNodeReturnsTrueIfIsNotLeafAndIsNotRoot()
        {
            var node = BuildNode();
            node.Left = BuildNode();
            node.Right = BuildNode();

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
