using Kore.Code.Trees.Binary;
using Kore.Code.Trees.Binary.AvlTree;
using Kore.Code.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Code.Tests.Trees.Binary.AvlTree
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
            BinaryTree.Insert(new int[] { 10, 2, 11, 1, 5, 13, 3, 6 });

            IBinaryNode target = BinaryTree.Search(2);
            IBinaryNode newNode = BinaryTree.Search(5);
            IBinaryNode expected = newNode.Left;

            BinaryTree.RotateLeft(target);

            Assert.AreSame(target.Right, expected);
            Assert.AreSame(expected.Parent, target);
        }

        [TestMethod]
        public override void RotateLeftUpdatesNodeNewNodeRelationship()
        {
            BinaryTree.Insert(new int[] { 10, 2, 11, 1, 5, 13, 3, 6 });

            IBinaryNode target = BinaryTree.Search(2);
            IBinaryNode newNode = BinaryTree.Search(5);

            BinaryTree.RotateLeft(target);

            Assert.AreSame(target.Parent, newNode);
            Assert.AreSame(newNode.Left, target);
        }

        #endregion

        #region Insert

        [TestMethod]
        public void InsertDoesNotBalanceIfBalanceModIsSmallerThanTwo()
        {
            BinaryTree.Insert(new int[] { 5, 6 });

            Assert.AreEqual(5, BinaryTree.Root.Key);
            Assert.AreEqual(6, BinaryTree.Root.Right.Key);

            BinaryTree.Insert(new int[] { 4, 3 });

            Assert.AreEqual(5, BinaryTree.Root.Key);
            Assert.AreEqual(6, BinaryTree.Root.Right.Key);
            Assert.AreEqual(4, BinaryTree.Root.Left.Key);
            Assert.AreEqual(3, BinaryTree.Root.Left.Left.Key);
        }

        [TestMethod]
        public void InsertCorrectlyHandlesRR()
        {
            BinaryTree.Insert(new int[] { 5, 6, 7 });

            Assert.AreEqual(6, BinaryTree.Root.Key);
            Assert.AreEqual(5, BinaryTree.Root.Left.Key);
            Assert.AreEqual(7, BinaryTree.Root.Right.Key);
        }

        [TestMethod]
        public void InsertCorrectlyHandlesLL()
        {
            BinaryTree.Insert(new int[] { 7, 6, 5 });

            Assert.AreEqual(6, BinaryTree.Root.Key);
            Assert.AreEqual(5, BinaryTree.Root.Left.Key);
            Assert.AreEqual(7, BinaryTree.Root.Right.Key);
        }

        [TestMethod]
        public void InsertCorrectlyHandlesLR()
        {
            BinaryTree.Insert(new int[] { 8, 6, 7 });

            Assert.AreEqual(7, BinaryTree.Root.Key);
            Assert.AreEqual(6, BinaryTree.Root.Left.Key);
            Assert.AreEqual(8, BinaryTree.Root.Right.Key);
        }

        [TestMethod]
        public void InsertCorrectlyHandlesRL()
        {
            BinaryTree.Insert(new int[] { 6, 8, 7 });

            Assert.AreEqual(7, BinaryTree.Root.Key);
            Assert.AreEqual(6, BinaryTree.Root.Left.Key);
            Assert.AreEqual(8, BinaryTree.Root.Right.Key);
        }

        [TestMethod]
        public void TestStuffAvl()
        {
            int[] input = RandomOps.GetContiguousRandomSequence(1, 100);

            foreach (int key in input)
            {
                BinaryTree.Insert(key);

                Assert.IsTrue(BinaryTree.IsBalanced());
            }
        }

        #endregion
    }
}
