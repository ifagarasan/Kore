using System;
using System.Collections.Generic;
using Kore.Code.Trees.Binary;
using Kore.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Code.Tests.Trees.Binary
{
    [TestClass]
    public abstract class BinaryTreeFunctionality
    {
        protected BinaryTree BinaryTree;

        protected abstract BinaryTree CreateBinaryTree();

        [TestInitialize]
        public virtual void SetUp()
        {
            BinaryTree = CreateBinaryTree();
        }

        #region Initialisation

        [TestMethod]
        public void RootIsNilAfterInit()
        {
            Assert.AreSame(BinaryTree.Nil, BinaryTree.Root);
        }

        [TestMethod]
        public void CountIsZero()
        {
            Assert.AreEqual(0, BinaryTree.Count);
        }

        #region Nil

        [TestMethod]
        public void NilIsDefined()
        {
            Assert.IsNotNull(BinaryTree.Nil);
        }

        [TestMethod]
        public void NilIsParentIsNill()
        {
            Assert.AreSame(BinaryTree.Nil, BinaryTree.Nil.Parent);
        }

        [TestMethod]
        public void NilIsLeftIsNill()
        {
            Assert.AreSame(BinaryTree.Nil, BinaryTree.Nil.Left);
        }

        [TestMethod]
        public void NilIsRightIsNill()
        {
            Assert.AreSame(BinaryTree.Nil, BinaryTree.Nil.Right);
        }

        #endregion

        #endregion

        #region IsBst

        [TestMethod]
        public void IsBstReturnsTrueForNillNode()
        {
            Assert.IsTrue(BinaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstReturnsTrueForNodeWithoutChildren()
        {
            BinaryTree.Insert(1);

            Assert.IsTrue(BinaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstReturnsTrueForIfLeftNodeKeyIsSmallerThanParentNodeKey()
        {
            BinaryTree.Insert(new int[] { 2, 1});

            Assert.IsTrue(BinaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstReturnsFalseIfLeftNodeKeyIsLargerThanParentNodeKey()
        {
            BinaryTree.Insert(2);
            BinaryTree.Root.Left = new BinaryNode(3);

            Assert.IsFalse(BinaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstReturnsFalseIfLeftNodeKeyIsEqualToParentNodeKey()
        {
            BinaryTree.Insert(2);
            BinaryTree.Root.Left = new BinaryNode(2);

            Assert.IsFalse(BinaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstReturnsTrueIfRightNodeKeyIsEqualToParentNodeKey()
        {
            BinaryTree.Insert(2);
            BinaryTree.Insert(3);

            BinaryTree.Root.Right.Key = 2;

            Assert.IsTrue(BinaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstReturnsTrueIfRightNodeKeyIsLargerThanParentNodeKey()
        {
            BinaryTree.Insert(2);
            BinaryTree.Insert(3);

            Assert.IsTrue(BinaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstReturnsFalseIfRightNodeKeyIsSmallerrThanParentNodeKey()
        {
            BinaryTree.Insert(2);
            BinaryTree.Root.Right = new BinaryNode(1);

            Assert.IsFalse(BinaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstProcessesNodesRecursively()
        {
            BinaryTree.Insert(new int[] { 2, 1, 6, 4, 5, 7 });

            Assert.IsTrue(BinaryTree.IsBst());

            var node = BinaryTree.Search(4);
            node.Left = new BinaryNode(120);

            Assert.IsFalse(BinaryTree.IsBst());
        }

        #endregion

        #region IsBalanced

        [TestMethod]
        public void IsBalancedReturnsTrueForEmptyTree()
        {
            Assert.IsTrue(BinaryTree.IsBalanced());
        }

        #endregion

        #region Traversals

        [TestMethod]
        public void InOrderReturnsArraySorted()
        {
            var input = new int[1000];
            for (var i = 0; i < input.Length; ++i)
                input[i] = i+1;

            var inputArray = new List<int>(input);

            var random = new System.Random();
            while (inputArray.Count > 0)
            {
                var index = random.Next(inputArray.Count);
                BinaryTree.Insert(inputArray[index]);
                inputArray.RemoveAt(index);
            }

            var currentIndex = 0;
            BinaryTree.Inorder((IBinaryNode node) => {
                Assert.AreEqual(input[currentIndex], node.Key);
                currentIndex++;
                return true;
            });

            Assert.AreEqual(currentIndex, input.Length);
        }

        #endregion

        #region Min

        [TestMethod]
        public void MinReturnsNilIfNodeNil()
        {
            Assert.AreSame(BinaryTree.Nil, BinaryTree.Min(BinaryTree.Root));
        }

        [TestMethod]
        public void MinReturnsNodeIfNodeHasNoLeftChild()
        {
            BinaryTree.Insert(1);

            Assert.AreSame(BinaryTree.Root, BinaryTree.Min(BinaryTree.Root));
        }

        [TestMethod]
        public void MinReturnsNodeWithSmallestKey()
        {
            BinaryTree.Insert(new int[] { 3, 2, 1 });
            
            Assert.AreSame(BinaryTree.Search(1), BinaryTree.Min(BinaryTree.Root));
        }

        #endregion

        #region Max

        [TestMethod]
        public void MaxReturnsNilIfNodeNil()
        {
            Assert.AreSame(BinaryTree.Nil, BinaryTree.Max(BinaryTree.Root));
        }

        [TestMethod]
        public void MaxReturnsNodeIfNodeHasNoRightChild()
        {
            BinaryTree.Insert(1);

            Assert.AreSame(BinaryTree.Root, BinaryTree.Max(BinaryTree.Root));
        }

        [TestMethod]
        public void MaxReturnsNodeWithLargestKey()
        {
            BinaryTree.Insert(new int[] { 1, 2, 3 });

            Assert.AreSame(BinaryTree.Search(3), BinaryTree.Max(BinaryTree.Root));
        }

        #endregion

        #region Successor

        [TestMethod]
        public void SuccesorReturnsNilForMaximum()
        {
            BinaryTree.Insert(new int[] { 2, 3, 1 });

            var minimum = BinaryTree.Search(3);

            Assert.AreSame(BinaryTree.Nil, BinaryTree.Successor(minimum));
        }

        [TestMethod]
        public void SuccesorReturnsNextElementInInOrder()
        {
            var array = new int[] { 10, 5, 7, 8, 9 };

            BinaryTree.Insert(array);

            var list = new List<int>(array);

            list.Sort();

            for (var i = 0; i < list.Count - 1; ++i)
            {
                var expected = BinaryTree.Search(list[i + 1]);
                var current = BinaryTree.Search(list[i]);

                Assert.AreSame(expected, BinaryTree.Successor(current));
            }
        }

        #endregion

        #region Predecessor

        [TestMethod]
        public void PredecessorReturnsNilForMinimum()
        {
            BinaryTree.Insert(new int[] { 2, 3, 1 });

            var minimum = BinaryTree.Search(1);

            Assert.AreSame(BinaryTree.Nil, BinaryTree.Predecessor(minimum));
        }

        [TestMethod]
        public void PredecessorReturnsPreviousElementInInOrder()
        {
            var array = new int[] { 10, 5, 7, 8, 9 };

            BinaryTree.Insert(array);

            var list = new List<int>(array);

            list.Sort();

            for (var i = 1; i < list.Count; ++i)
            {
                var expected = BinaryTree.Search(list[i-1]);
                var current = BinaryTree.Search(list[i]);

                Assert.AreSame(expected, BinaryTree.Predecessor(current));
            }
        }

        #endregion

        #region Transplant

        [TestMethod]
        public void TransplantDoesNotThrowExceptionIfNode1IsNil()
        {
            BinaryTree.Transplant(BinaryTree.Nil, new BinaryNode());
        }

        [TestMethod]
        public void TransplantSetsRootToVIfUIsRoot()
        {
            var v = new BinaryNode();
            BinaryTree.Insert(1);

            BinaryTree.Transplant(BinaryTree.Root, v);

            Assert.AreSame(BinaryTree.Root, v);
        }

        [TestMethod]
        public void TransplantSetsRootLeftToVIfUIsLeftChild()
        {
            var parent = new BinaryNode();
            var v = new BinaryNode();
            var u = new BinaryNode();
            parent.Left = u;
            u.Parent = parent;

            BinaryTree.Transplant(u, v);

            Assert.AreSame(parent.Left, v);
        }

        [TestMethod]
        public void TransplantSetsRootRightToVIfUIsRightChild()
        {
            var parent = new BinaryNode();
            var u = new BinaryNode();
            var v = new BinaryNode();
            parent.Right = u;
            u.Parent = parent;

            BinaryTree.Transplant(u, v);

            Assert.AreSame(parent.Right, v);
        }

        [TestMethod]
        public void TransplantDoesNotErrorIsVIsNil()
        {
            BinaryTree.Insert(1);
            BinaryTree.Transplant(BinaryTree.Root, BinaryTree.Nil);

            Assert.AreSame(BinaryTree.Nil, BinaryTree.Root);
        }

        [TestMethod]
        public void TransplantSetsVParentToUParent()
        {
            var u = new BinaryNode();
            var v = new BinaryNode();
            var parent = new BinaryNode();
            u.Parent = parent;

            BinaryTree.Transplant(u, v);

            Assert.AreSame(v.Parent, parent);
        }

        #endregion

        #region Rotations

        #region RotateLeft

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RotateLeftThrowsExceptionIfNodeIsNil()
        {
            BinaryTree.RotateLeft(BinaryTree.Nil);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "right node null")]
        public void RotateLeftThrowsExceptionIfNodeRightIsNil()
        {
            BinaryTree.Insert(1);

            BinaryTree.RotateLeft(BinaryTree.Root);
        }

        [TestMethod]
        public void RotateLeftSetsNodesParentToNewParent()
        {
            BinaryTree.Insert(new int[] { 1, 2 });

            var one = BinaryTree.Search(1);
            var two = BinaryTree.Search(2);

            BinaryTree.RotateLeft(one);

            Assert.AreSame(one.Parent, two);
            Assert.AreSame(two.Left, one);
        }

        [TestMethod]
        public virtual void RotateLeftSetsNodeRightToNewNodeLeft()
        {
            BinaryTree.Insert(new int[] { 10, 2, 1, 5, 3, 6 });

            var target = BinaryTree.Search(2);
            var newNode = BinaryTree.Search(5);
            var expected = newNode.Left;

            BinaryTree.RotateLeft(target);

            Assert.AreSame(target.Right, expected);
            Assert.AreSame(expected.Parent, target);
        }

        [TestMethod]
        public virtual void RotateLeftUpdatesNodeNewNodeRelationship()
        {
            BinaryTree.Insert(new int[] { 10, 2, 1, 5, 3, 6 });

            var target = BinaryTree.Search(2);
            var newNode = BinaryTree.Search(5);

            BinaryTree.RotateLeft(target);

            Assert.AreSame(target.Parent, newNode);
            Assert.AreSame(newNode.Left, target);
        }

        [TestMethod]
        public void RotateLeftUpdatesRootIfNodeIsRoot()
        {
            BinaryTree.Insert(new int[] { 2, 1, 5, 3, 6 });

            var target = BinaryTree.Search(2);
            var newNode = BinaryTree.Search(5);

            BinaryTree.RotateLeft(target);

            Assert.AreSame(BinaryTree.Root, newNode);
        }

        #endregion

        #region RotateRight

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RotateRightThrowsExceptionIfNodeIsNull()
        {
            BinaryTree.RotateRight(BinaryTree.Nil);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "right node null")]
        public void RotateRightThrowsExceptionIfNodeRightIsNull()
        {
            BinaryTree.Insert(1);

            BinaryTree.RotateRight(BinaryTree.Root);
        }

        [TestMethod]
        public void RotateRightSetsNodesParentToNewParent()
        {
            BinaryTree.Insert(new int[] { 2, 1 });

            var one = BinaryTree.Search(1);
            var two = BinaryTree.Search(2);

            BinaryTree.RotateRight(two);

            Assert.AreSame(two.Parent, one);
            Assert.AreSame(one.Right, two);
        }

        [TestMethod]
        public void RotateRightUpdatesNodeNewNodeRelationship()
        {
            BinaryTree.Insert(new int[] { 10, 11, 5, 2, 1, 3, 6 });

            var target = BinaryTree.Root;
            var newParent = BinaryTree.Root.Left;
            var newParentRight = newParent.Right;

            Assert.AreNotSame(BinaryTree.Nil, newParent);
            Assert.AreNotSame(BinaryTree.Nil, newParentRight);

            BinaryTree.RotateRight(target);

            Assert.AreSame(target.Parent, newParent);
            Assert.AreSame(newParent.Right, target);
            Assert.AreSame(target, newParentRight.Parent);
            Assert.AreSame(newParentRight, target.Left);
        }

        [TestMethod]
        public void RotateRightUpdatesRootIfNodeIsRoot()
        {
            BinaryTree.Insert(new int[] { 5, 2, 1, 3, 6 });

            var target = BinaryTree.Root;
            var newNode = BinaryTree.Root.Left;

            BinaryTree.RotateRight(target);

            Assert.AreSame(BinaryTree.Root, newNode);
        }

        #endregion

        #endregion

        #region Insert

        [TestMethod]
        public void InsertIncrementsCount()
        {
            var initialValue = BinaryTree.Count;

            BinaryTree.Insert(1);

            Assert.AreEqual(initialValue + 1, BinaryTree.Count);
        }

        [TestMethod]
        public void InsertMultipleIncrementsCountAccordingly()
        {
            var initialValue = BinaryTree.Count;
            var input = new int[] { 1, 2, 3 };

            BinaryTree.Insert(input);

            Assert.AreEqual(initialValue + input.Length, BinaryTree.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateKeyException))]
        public void InsertThrowsDuplicateKeyExceptionOnDuplcateKey()
        {
            BinaryTree.Insert(1);
            BinaryTree.Insert(1);
        }

        #endregion

        #region Remove

        [TestMethod]
        public void RemoveDecrementsCount()
        {
            BinaryTree.Insert(1);

            var initialValue = BinaryTree.Count;

            BinaryTree.Remove(1);

            Assert.AreEqual(initialValue - 1, BinaryTree.Count);
        }

        #endregion

        //[TestMethod]
        //public void LCANonExistentNodes()
        //{
        //    Bst bst = new Bst();

        //    Assert.IsNull(bst.LCA(1, 2));
        //}

        //[TestMethod]
        //public void LCANonExistentNode()
        //{
        //    Bst bst = new Bst();
        //    bst.Insert(1);

        //    Assert.IsNull(bst.LCA(1, 2));
        //}

        //[TestMethod]
        //public void LCASameNode()
        //{
        //    Bst bst = new Bst();
        //    BinaryNode node = bst.Insert(1);

        //    Assert.AreEqual(node, bst.LCA(1, 1));
        //}

        //[TestMethod]
        //public void LCAOrderDoesNotMatter()
        //{
        //    Bst bst = this.CreateBST();

        //    BinaryNode node = bst.LCA(3, 2);
        //    Assert.AreEqual(2, node.Key);

        //    node = bst.LCA(2, 3);
        //    Assert.AreEqual(2, node.Key);
        //}

        //[TestMethod]
        //public void LCAParentAndChild()
        //{
        //    Bst bst = this.CreateBST();

        //    BinaryNode node = bst.LCA(3, 2);
        //    Assert.AreEqual(2, node.Key);

        //    node = bst.LCA(3, 1);
        //    Assert.AreEqual(1, node.Key);

        //    node = bst.LCA(8, 3);
        //    Assert.AreEqual(8, node.Key);
        //}

        //[TestMethod]
        //public void LCADifferentSidesOfRoot()
        //{
        //    Bst bst = this.CreateBST();

        //    BinaryNode node = bst.LCA(9, 1);
        //    Assert.AreEqual(8, node.Key);
        //}

        //[TestMethod]
        //public void LCASameSideOfRoot()
        //{
        //    Bst bst = this.CreateBST();

        //    BinaryNode node = bst.LCA(3, 5);
        //    Assert.AreEqual(4, node.Key);
        //}
    }
}
