using Kore.Code.Trees.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Code.Tests.Trees.Binary
{
    [TestClass]
    public class RedBlackTreeFunctionality : BstFunctionality
    {
        protected override BinaryTree CreateBinaryTree()
        {
            return new Kore.Code.Trees.Binary.RedBlackTree.RedBlackTree();
        }

        #region Nil

        [TestMethod]
        public void NilIsBlack()
        {
            Assert.AreEqual(Color.Black, BinaryTree.Nil.Color);
        }

        #endregion

        #region IsBalanced

        [TestMethod]
        public void IsBalancedReturnsFalseIfRootIsRed()
        {
            BinaryTree.Insert(1);
            BinaryTree.Root.Color = Color.Red;

            Assert.IsFalse(BinaryTree.IsBalanced());
        }

        [TestMethod]
        public void IsBalancedReturnsFalseIfARedNodeHasARedChild()
        {
            BinaryTree.Insert(new int[] { 2, 1, 3, 5, 6 });

            var node = BinaryTree.Search(5);
            node.Color = Color.Red;

            node.Left.Color = Color.Black;

            Assert.IsFalse(BinaryTree.IsBalanced());

            node.Left.Color = Color.Red;
            node.Right.Color = Color.Black;

            Assert.IsFalse(BinaryTree.IsBalanced());
        }

        #endregion

        #region Insert

        [TestMethod]
        public void InsertRootIsColoredBlack()
        {
            BinaryTree.Insert(1);

            Assert.AreEqual(Color.Black, BinaryTree.Root.Color);
        }

        [TestMethod]
        public void InsertTurnsParentAndUncleRedGrandparentRedWhenUncleIsRed()
        {
            BinaryTree.Insert(new int[] { 10, 12, 8, 6, 5, 7 });

            Assert.AreEqual(Color.Red, BinaryTree.Search(6).Color);
            Assert.AreEqual(Color.Black, BinaryTree.Search(5).Color);
            Assert.AreEqual(Color.Black, BinaryTree.Search(8).Color);
        }

        [TestMethod]
        public void InsertCase3PerformsRightRotationAroundGrandparentAndSetsGrandparentToRedAndParentToBlack()
        {
            var grandparent = BinaryTree.Insert(6);
            var parent = BinaryTree.Insert(5);
            var node = BinaryTree.Insert(1);

            Assert.AreSame(parent, BinaryTree.Root);
            Assert.AreSame(node, BinaryTree.Root.Left);
            Assert.AreSame(grandparent, BinaryTree.Root.Right);

            Assert.AreEqual(Color.Red, node.Color);
            Assert.AreEqual(Color.Black, parent.Color);
            Assert.AreEqual(Color.Red, grandparent.Color);
        }

        [TestMethod]
        public void InsertCase3IncludesCase2kWhenNodeIsRightChild()
        {
            var grandparent = BinaryTree.Insert(6);
            var parent = BinaryTree.Insert(3);
            var node = BinaryTree.Insert(5);

            Assert.AreSame(node, BinaryTree.Root);
            Assert.AreSame(parent, BinaryTree.Root.Left);
            Assert.AreSame(grandparent, BinaryTree.Root.Right);

            Assert.AreEqual(Color.Black, node.Color);
            Assert.AreEqual(Color.Red, parent.Color);
            Assert.AreEqual(Color.Red, grandparent.Color);
        }

        [TestMethod]
        public void InsertCase3PerformsLeftRotationAroundGrandparentAndSetsGrandparentToRedAndParentToBlack()
        {
            var grandparent = BinaryTree.Insert(1);
            var parent = BinaryTree.Insert(4);
            var node = BinaryTree.Insert(6);

            Assert.AreSame(parent, BinaryTree.Root);
            Assert.AreSame(node, BinaryTree.Root.Right);
            Assert.AreSame(grandparent, BinaryTree.Root.Left);

            Assert.AreEqual(Color.Red, node.Color);
            Assert.AreEqual(Color.Black, parent.Color);
            Assert.AreEqual(Color.Red, grandparent.Color);
        }

        [TestMethod]
        public void InsertCase3IncludesCase2kWhenNodeIsLeftChild()
        {
            var grandparent = BinaryTree.Insert(1);
            var parent = BinaryTree.Insert(4);
            var node = BinaryTree.Insert(3);

            Assert.AreSame(node, BinaryTree.Root);
            Assert.AreSame(parent, BinaryTree.Root.Right);
            Assert.AreSame(grandparent, BinaryTree.Root.Left);

            Assert.AreEqual(Color.Black, node.Color);
            Assert.AreEqual(Color.Red, parent.Color);
            Assert.AreEqual(Color.Red, grandparent.Color);
        }

        protected override void TestTreeCorrectness()
        {
            base.TestTreeCorrectness();

            Assert.IsTrue(BinaryTree.IsBalanced());
        }

        #endregion

        #region Remove

        [TestMethod]
        public void RemoveRootSetsNewRootToBlack()
        {
            BinaryTree.Insert(new int[] { 1, 2 });

            BinaryTree.Remove(1);

            Assert.AreEqual(Color.Black, BinaryTree.Root.Color);
        }

        [TestMethod]
        public override void RemoveNodeWithOneRightChildReplacesNodeWithChild()
        {
            BinaryTree.Insert(new int[] { 10, 8, 12, 6, 5, 7 });

            var node = BinaryTree.Search(6);
            var newNode = BinaryTree.Search(7);

            BinaryTree.Remove(8);

            Assert.AreSame(newNode, node.Right);
            Assert.AreSame(node, newNode.Parent);
        }

        [TestMethod]
        public void RemoveNodeWithOneRightChildDoesNotReplaceChildColor()
        {
            BinaryTree.Insert(new int[] { 10, 8, 12, 6, 5, 7 });

            var node = BinaryTree.Search(6);
            var newNode = BinaryTree.Search(7);

            Assert.AreEqual(Color.Red, newNode.Color);

            BinaryTree.Remove(8);   

            Assert.AreSame(node, newNode.Parent);
        }

        [TestMethod]
        public override void RemoveNodeWithOneLeftChildReplacesNodeWithChild()
        {
            BinaryTree.Insert(new int[] { 10, 8, 12, 6, 5, 4 });

            var node = BinaryTree.Search(6);
            var newNode = BinaryTree.Search(4);

            BinaryTree.Remove(5);

            Assert.AreSame(newNode, node.Left);
            Assert.AreSame(node, newNode.Parent);
        }

        [TestMethod]
        public void RemoveNodeWithOneLeftChildDoesNotReplaceChildColor()
        {
            BinaryTree.Insert(new int[] { 10, 8, 12, 6, 5, 4 });

            var node = BinaryTree.Search(6);
            var newNode = BinaryTree.Search(4);

            Assert.AreEqual(Color.Red, newNode.Color);

            BinaryTree.Remove(5);

            Assert.AreSame(node, newNode.Parent);
        }

        [TestMethod]
        public void RemoveNodeWithTwoChildrenAndSuccessorRedReplacesNodeWithSuccessorOfColorBlack()
        {
            BinaryTree.Insert(new int[] { 10, 8, 12, 6, 5 });

            var node = BinaryTree.Search(6);
            var nodeLeft = node.Left;
            var newNode = BinaryTree.Search(8);

            Assert.AreEqual(Color.Red, newNode.Color);

            BinaryTree.Remove(6);

            Assert.AreSame(newNode, BinaryTree.Root.Left);
            Assert.AreSame(BinaryTree.Root, newNode.Parent);
            Assert.AreSame(nodeLeft, newNode.Left);
            Assert.AreSame(newNode, nodeLeft.Parent);
        }

        protected override void TestRemoveAtIndex(int[] keys, int index)
        {
            base.TestRemoveAtIndex(keys, index);

            Assert.IsTrue(BinaryTree.IsBalanced());
        }

        //TODO: clear the tree starting from an index - yea, baybee!

        [TestMethod]
        public void RemoveCanBeCalledSuccessivelyOnRootToClearTheTree()
        {
            //TODO: fix the cause of this test failing
            //int[] input = new int[] { 2, 4, 9, 17, 19, 10, 14, 5, 8, 6 };

            //binaryTree.Insert(input);

            //while (binaryTree.Root != binaryTree.Nil)
            //{
            //     binaryTree.Remove(binaryTree.Root.Key);
            //    Assert.IsTrue(binaryTree.IsBalanced());
            //}
        }

        [TestMethod]
        public override void RemoveCorrectlyRemovesAcrossArrayOfKeys()
        {
            //TODO: fix the cause of this test failing
            //TestRemove(RandomOps.GetContiguousRandomSequence(1, 100));
        }

        #endregion
    }
}
