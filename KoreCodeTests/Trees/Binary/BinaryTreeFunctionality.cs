using System;

using KoreCode.Trees.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoreCode.Exceptions;
using System.Collections.Generic;
using System.Collections;

namespace KoreCodeTests.Tree.Binary
{
    [TestClass]
    public abstract class BinaryTreeFunctionality
    {
        protected BinaryTree binaryTree;

        protected abstract BinaryTree CreateBinaryTree();

        [TestInitialize]
        public virtual void SetUp()
        {
            binaryTree = CreateBinaryTree();
        }

        #region Initialisation

        [TestMethod]
        public void RootIsNilAfterInit()
        {
            Assert.AreSame(binaryTree.Nil, binaryTree.Root);
        }

        [TestMethod]
        public void CountIsZero()
        {
            Assert.AreEqual(0, binaryTree.Count);
        }

        #region Nil

        [TestMethod]
        public void NilIsDefined()
        {
            Assert.IsNotNull(binaryTree.Nil);
        }

        [TestMethod]
        public void NilIsParentIsNill()
        {
            Assert.AreSame(binaryTree.Nil, binaryTree.Nil.Parent);
        }

        [TestMethod]
        public void NilIsLeftIsNill()
        {
            Assert.AreSame(binaryTree.Nil, binaryTree.Nil.Left);
        }

        [TestMethod]
        public void NilIsRightIsNill()
        {
            Assert.AreSame(binaryTree.Nil, binaryTree.Nil.Right);
        }

        #endregion

        #endregion

        #region IsBst

        [TestMethod]
        public void IsBstReturnsTrueForNillNode()
        {
            Assert.IsTrue(binaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstReturnsTrueForNodeWithoutChildren()
        {
            binaryTree.Insert(1);

            Assert.IsTrue(binaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstReturnsTrueForIfLeftNodeKeyIsSmallerThanParentNodeKey()
        {
            binaryTree.Insert(new int[] { 2, 1});

            Assert.IsTrue(binaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstReturnsFalseIfLeftNodeKeyIsLargerThanParentNodeKey()
        {
            binaryTree.Insert(2);
            binaryTree.Root.Left = new BinaryNode(3);

            Assert.IsFalse(binaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstReturnsFalseIfLeftNodeKeyIsEqualToParentNodeKey()
        {
            binaryTree.Insert(2);
            binaryTree.Root.Left = new BinaryNode(2);

            Assert.IsFalse(binaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstReturnsTrueIfRightNodeKeyIsEqualToParentNodeKey()
        {
            binaryTree.Insert(2);
            binaryTree.Insert(3);

            binaryTree.Root.Right.Key = 2;

            Assert.IsTrue(binaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstReturnsTrueIfRightNodeKeyIsLargerThanParentNodeKey()
        {
            binaryTree.Insert(2);
            binaryTree.Insert(3);

            Assert.IsTrue(binaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstReturnsFalseIfRightNodeKeyIsSmallerrThanParentNodeKey()
        {
            binaryTree.Insert(2);
            binaryTree.Root.Right = new BinaryNode(1);

            Assert.IsFalse(binaryTree.IsBst());
        }

        [TestMethod]
        public void IsBstProcessesNodesRecursively()
        {
            binaryTree.Insert(new int[] { 2, 1, 6, 4, 5, 7 });

            Assert.IsTrue(binaryTree.IsBst());

            IBinaryNode node = binaryTree.Search(4);
            node.Left = new BinaryNode(120);

            Assert.IsFalse(binaryTree.IsBst());
        }

        #endregion

        #region IsBalanced

        [TestMethod]
        public void IsBalancedReturnsTrueForEmptyTree()
        {
            Assert.IsTrue(binaryTree.IsBalanced());
        }

        #endregion

        #region Traversals

        [TestMethod]
        public void InOrderReturnsArraySorted()
        {
            int[] input = new int[1000];
            for (int i = 0; i < input.Length; ++i)
                input[i] = i+1;

            List<int> inputArray = new List<int>(input);

            Random random = new Random();
            while (inputArray.Count > 0)
            {
                int index = random.Next(inputArray.Count);
                binaryTree.Insert(inputArray[index]);
                inputArray.RemoveAt(index);
            }

            int currentIndex = 0;
            binaryTree.Inorder((IBinaryNode node) => {
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
            Assert.AreSame(binaryTree.Nil, binaryTree.Min(binaryTree.Root));
        }

        [TestMethod]
        public void MinReturnsNodeIfNodeHasNoLeftChild()
        {
            binaryTree.Insert(1);

            Assert.AreSame(binaryTree.Root, binaryTree.Min(binaryTree.Root));
        }

        [TestMethod]
        public void MinReturnsNodeWithSmallestKey()
        {
            binaryTree.Insert(new int[] { 3, 2, 1 });
            
            Assert.AreSame(binaryTree.Search(1), binaryTree.Min(binaryTree.Root));
        }

        #endregion

        #region Max

        [TestMethod]
        public void MaxReturnsNilIfNodeNil()
        {
            Assert.AreSame(binaryTree.Nil, binaryTree.Max(binaryTree.Root));
        }

        [TestMethod]
        public void MaxReturnsNodeIfNodeHasNoRightChild()
        {
            binaryTree.Insert(1);

            Assert.AreSame(binaryTree.Root, binaryTree.Max(binaryTree.Root));
        }

        [TestMethod]
        public void MaxReturnsNodeWithLargestKey()
        {
            binaryTree.Insert(new int[] { 1, 2, 3 });

            Assert.AreSame(binaryTree.Search(3), binaryTree.Max(binaryTree.Root));
        }

        #endregion

        #region Successor

        [TestMethod]
        public void SuccesorReturnsNilForMaximum()
        {
            binaryTree.Insert(new int[] { 2, 3, 1 });

            var minimum = binaryTree.Search(3);

            Assert.AreSame(binaryTree.Nil, binaryTree.Successor(minimum));
        }

        [TestMethod]
        public void SuccesorReturnsNextElementInInOrder()
        {
            int[] array = new int[] { 10, 5, 7, 8, 9 };

            binaryTree.Insert(array);

            List<int> list = new List<int>(array);

            list.Sort();

            for (int i = 0; i < list.Count - 1; ++i)
            {
                IBinaryNode expected = binaryTree.Search(list[i + 1]);
                IBinaryNode current = binaryTree.Search(list[i]);

                Assert.AreSame(expected, binaryTree.Successor(current));
            }
        }

        #endregion

        #region Predecessor

        [TestMethod]
        public void PredecessorReturnsNilForMinimum()
        {
            binaryTree.Insert(new int[] { 2, 3, 1 });

            var minimum = binaryTree.Search(1);

            Assert.AreSame(binaryTree.Nil, binaryTree.Predecessor(minimum));
        }

        [TestMethod]
        public void PredecessorReturnsPreviousElementInInOrder()
        {
            int[] array = new int[] { 10, 5, 7, 8, 9 };

            binaryTree.Insert(array);

            List<int> list = new List<int>(array);

            list.Sort();

            for (int i = 1; i < list.Count; ++i)
            {
                IBinaryNode expected = binaryTree.Search(list[i-1]);
                IBinaryNode current = binaryTree.Search(list[i]);

                Assert.AreSame(expected, binaryTree.Predecessor(current));
            }
        }

        #endregion

        #region Transplant

        [TestMethod]
        public void TransplantDoesNotThrowExceptionIfNode1IsNil()
        {
            binaryTree.Transplant(binaryTree.Nil, new BinaryNode());
        }

        [TestMethod]
        public void TransplantSetsRootToVIfUIsRoot()
        {
            BinaryNode v = new BinaryNode();
            binaryTree.Insert(1);

            binaryTree.Transplant(binaryTree.Root, v);

            Assert.AreSame(binaryTree.Root, v);
        }

        [TestMethod]
        public void TransplantSetsRootLeftToVIfUIsLeftChild()
        {
            BinaryNode parent = new BinaryNode();
            BinaryNode v = new BinaryNode();
            BinaryNode u = new BinaryNode();
            parent.Left = u;
            u.Parent = parent;

            binaryTree.Transplant(u, v);

            Assert.AreSame(parent.Left, v);
        }

        [TestMethod]
        public void TransplantSetsRootRightToVIfUIsRightChild()
        {
            BinaryNode parent = new BinaryNode();
            BinaryNode u = new BinaryNode();
            BinaryNode v = new BinaryNode();
            parent.Right = u;
            u.Parent = parent;

            binaryTree.Transplant(u, v);

            Assert.AreSame(parent.Right, v);
        }

        [TestMethod]
        public void TransplantDoesNotErrorIsVIsNil()
        {
            binaryTree.Insert(1);
            binaryTree.Transplant(binaryTree.Root, binaryTree.Nil);

            Assert.AreSame(binaryTree.Nil, binaryTree.Root);
        }

        [TestMethod]
        public void TransplantSetsVParentToUParent()
        {
            BinaryNode u = new BinaryNode();
            BinaryNode v = new BinaryNode();
            BinaryNode parent = new BinaryNode();
            u.Parent = parent;

            binaryTree.Transplant(u, v);

            Assert.AreSame(v.Parent, parent);
        }

        #endregion

        #region Rotations

        #region RotateLeft

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RotateLeftThrowsExceptionIfNodeIsNil()
        {
            binaryTree.RotateLeft(binaryTree.Nil);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "right node null")]
        public void RotateLeftThrowsExceptionIfNodeRightIsNil()
        {
            binaryTree.Insert(1);

            binaryTree.RotateLeft(binaryTree.Root);
        }

        [TestMethod]
        public void RotateLeftSetsNodesParentToNewParent()
        {
            binaryTree.Insert(new int[] { 1, 2 });

            IBinaryNode one = binaryTree.Search(1);
            IBinaryNode two = binaryTree.Search(2);

            binaryTree.RotateLeft(one);

            Assert.AreSame(one.Parent, two);
            Assert.AreSame(two.Left, one);
        }

        [TestMethod]
        public void RotateLeftSetsNodeRightToNewNodeLeft()
        {
            binaryTree.Insert(new int[] { 10, 2, 1, 5, 3, 6 });

            IBinaryNode target = binaryTree.Search(2);
            IBinaryNode newNode = binaryTree.Search(5);
            IBinaryNode expected = newNode.Left;

            binaryTree.RotateLeft(target);

            Assert.AreSame(target.Right, expected);
            Assert.AreSame(expected.Parent, target);
        }

        [TestMethod]
        public void RotateLeftUpdatesNodeNewNodeRelationship()
        {
            binaryTree.Insert(new int[] { 10, 2, 1, 5, 3, 6 });

            IBinaryNode target = binaryTree.Search(2);
            IBinaryNode newNode = binaryTree.Search(5);

            binaryTree.RotateLeft(target);

            Assert.AreSame(target.Parent, newNode);
            Assert.AreSame(newNode.Left, target);
        }

        [TestMethod]
        public void RotateLeftUpdatesRootIfNodeIsRoot()
        {
            binaryTree.Insert(new int[] { 2, 1, 5, 3, 6 });

            IBinaryNode target = binaryTree.Search(2);
            IBinaryNode newNode = binaryTree.Search(5);

            binaryTree.RotateLeft(target);

            Assert.AreSame(binaryTree.Root, newNode);
        }

        #endregion

        #region RotateRight

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RotateRightThrowsExceptionIfNodeIsNull()
        {
            binaryTree.RotateRight(binaryTree.Nil);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "right node null")]
        public void RotateRightThrowsExceptionIfNodeRightIsNull()
        {
            binaryTree.Insert(1);

            binaryTree.RotateRight(binaryTree.Root);
        }

        [TestMethod]
        public void RotateRightSetsNodesParentToNewParent()
        {
            binaryTree.Insert(new int[] { 2, 1 });

            IBinaryNode one = binaryTree.Search(1);
            IBinaryNode two = binaryTree.Search(2);

            binaryTree.RotateRight(two);

            Assert.AreSame(two.Parent, one);
            Assert.AreSame(one.Right, two);
        }

        [TestMethod]
        public void RotateRightUpdatesNodeNewNodeRelationship()
        {
            binaryTree.Insert(new int[] { 10, 11, 5, 2, 1, 3, 6 });

            IBinaryNode target = binaryTree.Root;
            IBinaryNode newParent = binaryTree.Root.Left;
            IBinaryNode newParentRight = newParent.Right;

            Assert.AreNotSame(binaryTree.Nil, newParent);
            Assert.AreNotSame(binaryTree.Nil, newParentRight);

            binaryTree.RotateRight(target);

            Assert.AreSame(target.Parent, newParent);
            Assert.AreSame(newParent.Right, target);
            Assert.AreSame(target, newParentRight.Parent);
            Assert.AreSame(newParentRight, target.Left);
        }

        [TestMethod]
        public void RotateRightUpdatesRootIfNodeIsRoot()
        {
            binaryTree.Insert(new int[] { 5, 2, 1, 3, 6 });

            IBinaryNode target = binaryTree.Root;
            IBinaryNode newNode = binaryTree.Root.Left;

            binaryTree.RotateRight(target);

            Assert.AreSame(binaryTree.Root, newNode);
        }

        #endregion

        #endregion

        #region Insert

        [TestMethod]
        public void InsertIncrementsCount()
        {
            int initialValue = binaryTree.Count;

            binaryTree.Insert(1);

            Assert.AreEqual(initialValue + 1, binaryTree.Count);
        }

        [TestMethod]
        public void InsertMultipleIncrementsCountAccordingly()
        {
            int initialValue = binaryTree.Count;
            int[] input = new int[] { 1, 2, 3 };

            binaryTree.Insert(input);

            Assert.AreEqual(initialValue + input.Length, binaryTree.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateKeyException))]
        public void InsertThrowsDuplicateKeyExceptionOnDuplcateKey()
        {
            binaryTree.Insert(1);
            binaryTree.Insert(1);
        }

        #endregion

        #region Remove

        [TestMethod]
        public void RemoveDecrementsCount()
        {
            binaryTree.Insert(1);

            int initialValue = binaryTree.Count;

            binaryTree.Remove(1);

            Assert.AreEqual(initialValue - 1, binaryTree.Count);
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
