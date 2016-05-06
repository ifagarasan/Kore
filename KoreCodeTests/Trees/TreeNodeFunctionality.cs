using System;

using KoreCode.Trees.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoreCode.Exceptions;
using KoreCode.Trees;
using Moq;

namespace KoreCodeTests.Tree.Binary.Nodes
{
    [TestClass]
    public abstract class TreeNodeFunctionality<T> where T : class, ITreeNode<T>
    {
        protected T Nil;

        [TestInitialize]
        public virtual void SetUp()
        {
            Nil = CreateNode();
            DecorateNode(Nil);
        }

        protected Mock<T> CreateMockNode()
        {
            return new Mock<T> { CallBase = true };
        }

        protected abstract T CreateNode();
        protected abstract void DecorateNode(T node);

        protected virtual T BuildNode()
        {
            T node = CreateNode();
            DecorateNode(node);
            return node;
        }

        #region Grandparent

        [TestMethod]
        public void GrandparentReturnsParentParent()
        {
            T grandParent = BuildNode();
            T parent = BuildNode();
            T node = CreateNode();

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

        ////[TestMethod] TODO: fix using Moq
        ////public void IsInternalNodeReturnsTrueIfIsNotLeafAndIsNotRoot()
        ////{
        ////    var nodeMock = CreateMockNode();

        ////    nodeMock.Setup(node => node.IsLeaf).Returns(() => false);
        ////    nodeMock.Setup(node => node.IsRoot).Returns(() => false);

        ////    var result = nodeMock.Object.IsInternalNodeFunc();

        ////    Assert.IsTrue(result);
        ////}

        #endregion
    }
}
