using System.Collections.Generic;
using Kore.Code.Trees.Binary;
using Kore.Code.Util;
using Kore.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Code.Tests.Trees.Binary
{
    [TestClass]
    public class BstFunctionality: BinaryTreeFunctionality
    {
        protected override BinaryTree CreateBinaryTree()
        {
            return new Bst();
        }

        #region Insert

        [TestMethod]
        public void InsertSetsRootIfTreeEmpty()
        {
            BinaryTree.Insert(1);

            Assert.IsNotNull(BinaryTree.Root);
            Assert.AreEqual(1, BinaryTree.Root.Key);
        }

        [TestMethod]
        public void InsertPreservesTreeProperty()
        {
            var input = RandomOps.GetContiguousRandomSequence(1, 100);

            foreach(var key in input)
            {
                BinaryTree.Insert(key);

                TestTreeCorrectness();
            }
        }

        protected virtual void TestTreeCorrectness()
        {
            Assert.IsTrue(BinaryTree.IsBst());
        }

        #endregion

        #region Search

        [TestMethod]
        public void SearchOnEmptyTreeReturnsNil()
        {
            Assert.AreSame(BinaryTree.Root, BinaryTree.Search(1));
        }

        [TestMethod]
        public void SearchFindsRootAndReturnsReference()
        {
            BinaryTree.Insert(1);

            Assert.AreSame(BinaryTree.Root, BinaryTree.Search(1));
        }

        [TestMethod]
        public void SearchReturnsNilWhenNodeIsNotFound()
        {
            BinaryTree.Insert(3);
            BinaryTree.Insert(2);
            BinaryTree.Insert(4);

            Assert.AreSame(BinaryTree.Nil, BinaryTree.Search(5));
        }

        [TestMethod]
        public void SearchReturnsReferenceToNodeWhenFound()
        {
            BinaryTree.Insert(3);
            BinaryTree.Insert(2);
            var node = BinaryTree.Insert(5);
            BinaryTree.Insert(4);

            Assert.AreSame(node, BinaryTree.Search(5));
        }

        #endregion

        #region Remove

        [TestMethod]
        public void RemoveRoot()
        {
            BinaryTree.Insert(1);
            BinaryTree.Remove(1);

            Assert.AreSame(BinaryTree.Nil, BinaryTree.Root);
        }

        [TestMethod]
        public void RemoveRemovesLeaf()
        {
            var bst = new Bst();
            bst.Insert(1);
            bst.Insert(2);

            bst.Remove(2);

            Assert.AreSame(bst.Nil, bst.Root.Right);
            Assert.AreSame(bst.Nil, bst.Search(2));
        }

        [TestMethod]
        public virtual void RemoveNodeWithOneLeftChildReplacesNodeWithChild()
        {
            BinaryTree.Insert(new int[] { 3, 2, 1});

            BinaryTree.Remove(2);

            var one = BinaryTree.Search(1);

            Assert.AreSame(one, BinaryTree.Root.Left);
            Assert.AreSame(BinaryTree.Root, one.Parent);
        }

        [TestMethod]
        public virtual void RemoveNodeWithOneRightChildReplacesNodeWithChild()
        {
            BinaryTree.Insert(new int[] { 2, 1, 3, 4 });

            BinaryTree.Remove(3);

            var three = BinaryTree.Search(4);

            Assert.AreSame(three, BinaryTree.Root.Right);
            Assert.AreSame(BinaryTree.Root, three.Parent);
        }

        [TestMethod]
        [ExpectedException(typeof(ElementNotFoundException), "element '1' not found in collection")]
        public void RemoveNonExistentThrowsElementNotFound()
        {
            BinaryTree.Remove(1);
        }

        [TestMethod]
        public virtual void RemoveCorrectlyRemovesAcrossArrayOfKeys()
        {
            TestRemove(RandomOps.GetContiguousRandomSequence(1, 100));
        }

        public void TestRemove(int[] keys)
        {
            for (var i = 0; i < keys.Length; ++i)
                TestRemoveAtIndex(keys, i);
        }

        protected virtual void TestRemoveAtIndex(int[] keys, int index)
        {
            var keyList = new List<int>(keys);

            BinaryTree = CreateBinaryTree();

            //TODO: investigate why this doesn't work
            //while (binaryTree.Root != binaryTree.Nil)
            //    binaryTree.Remove(binaryTree.Root.Key);

            BinaryTree.Insert(keys);

            var keyToRemove = keys[index];

            BinaryTree.Remove(keyToRemove);

            Assert.AreEqual(BinaryTree.Nil, BinaryTree.Search(keyToRemove));

            foreach (var key in keys)
                if (key != keyToRemove)
                    Assert.AreEqual(key, BinaryTree.Search(key).Key);

            TestTreeCorrectness();
        }

        #endregion
    }
}
