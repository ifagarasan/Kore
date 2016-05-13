using System;

using KoreCode.Trees.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoreCode.Exceptions;
using KoreCode.Trees;
using Moq;
using KoreCode.Nodes.Builders;

namespace KoreCodeTests.Tree.Binary.Nodes
{
    [TestClass]
    public abstract class TreeNodeFunctionality<T> where T : class, ITreeNode<T>
    {
        [TestInitialize]
        public abstract void SetUp();

        protected abstract T BuildNode(int key = 0);

        #region Grandparent

        [TestMethod]
        public void GrandparentReturnsParentParent()
        {
            T grandParent = BuildNode();
            T parent = BuildNode();
            T node = BuildNode();

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
        public abstract void IsInternalNodeReturnsTrueIfIsNotLeafAndIsNotRoot();

        protected virtual void RunIsInternalNodeReturnsTrueIfIsNotLeafAndIsNotRoot<R>() where R : class, ITreeNode<T>
        {
            var mock = new Mock<R> { CallBase = true };

            mock.Setup(node => node.IsLeaf).Returns(() => false);
            mock.Setup(node => node.IsRoot).Returns(() => false);

            Assert.IsTrue(mock.Object.IsInternalNode);
        }

        #endregion
    }
}
