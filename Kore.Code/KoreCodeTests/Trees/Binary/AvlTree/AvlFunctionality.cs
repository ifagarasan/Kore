using KoreCode.Exceptions;
using KoreCode.Trees.Binary;
using KoreCode.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using KoreCode.Trees.Binary.AvlTree;

namespace KoreCodeTests.Tree.Binary.AvlTree
{
    [TestClass]
    public class AvlFunctionality: BstFunctionality
    {
        protected override BinaryTree CreateBinaryTree()
        {
            return new Avl();
        }

        #region Rotation

        [TestMethod]
        public override void RotateLeftSetsNodeRightToNewNodeLeft()
        {
            binaryTree.Insert(new int[] { 10, 2, 11, 1, 5, 13, 3, 6 });

            IBinaryNode target = binaryTree.Search(2);
            IBinaryNode newNode = binaryTree.Search(5);
            IBinaryNode expected = newNode.Left;

            binaryTree.RotateLeft(target);

            Assert.AreSame(target.Right, expected);
            Assert.AreSame(expected.Parent, target);
        }

        [TestMethod]
        public override void RotateLeftUpdatesNodeNewNodeRelationship()
        {
            binaryTree.Insert(new int[] { 10, 2, 11, 1, 5, 13, 3, 6 });

            IBinaryNode target = binaryTree.Search(2);
            IBinaryNode newNode = binaryTree.Search(5);

            binaryTree.RotateLeft(target);

            Assert.AreSame(target.Parent, newNode);
            Assert.AreSame(newNode.Left, target);
        }

        #endregion

        #region Insert

        [TestMethod]
        public void InsertDoesNotBalanceIfBalanceModIsSmallerThanTwo()
        {
            binaryTree.Insert(new int[] { 5, 6 });

            Assert.AreEqual(5, binaryTree.Root.Key);
            Assert.AreEqual(6, binaryTree.Root.Right.Key);

            binaryTree.Insert(new int[] { 4, 3 });

            Assert.AreEqual(5, binaryTree.Root.Key);
            Assert.AreEqual(6, binaryTree.Root.Right.Key);
            Assert.AreEqual(4, binaryTree.Root.Left.Key);
            Assert.AreEqual(3, binaryTree.Root.Left.Left.Key);
        }

        [TestMethod]
        public void InsertCorrectlyHandlesRR()
        {
            binaryTree.Insert(new int[] { 5, 6, 7 });

            Assert.AreEqual(6, binaryTree.Root.Key);
            Assert.AreEqual(5, binaryTree.Root.Left.Key);
            Assert.AreEqual(7, binaryTree.Root.Right.Key);
        }

        [TestMethod]
        public void InsertCorrectlyHandlesLL()
        {
            binaryTree.Insert(new int[] { 7, 6, 5 });

            Assert.AreEqual(6, binaryTree.Root.Key);
            Assert.AreEqual(5, binaryTree.Root.Left.Key);
            Assert.AreEqual(7, binaryTree.Root.Right.Key);
        }

        [TestMethod]
        public void InsertCorrectlyHandlesLR()
        {
            binaryTree.Insert(new int[] { 8, 6, 7 });

            Assert.AreEqual(7, binaryTree.Root.Key);
            Assert.AreEqual(6, binaryTree.Root.Left.Key);
            Assert.AreEqual(8, binaryTree.Root.Right.Key);
        }

        [TestMethod]
        public void InsertCorrectlyHandlesRL()
        {
            binaryTree.Insert(new int[] { 6, 8, 7 });

            Assert.AreEqual(7, binaryTree.Root.Key);
            Assert.AreEqual(6, binaryTree.Root.Left.Key);
            Assert.AreEqual(8, binaryTree.Root.Right.Key);
        }

        [TestMethod]
        public void TestStuffAvl()
        {
            int[] input = RandomOps.GetContiguousRandomSequence(1, 100);

            foreach (int key in input)
            {
                binaryTree.Insert(key);

                Assert.IsTrue(binaryTree.IsBalanced());
            }
        }

        #endregion
    }
}
