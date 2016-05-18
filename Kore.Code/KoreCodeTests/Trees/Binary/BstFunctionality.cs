using KoreCode.Exceptions;
using KoreCode.Trees.Binary;
using KoreCode.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace KoreCodeTests.Tree.Binary
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
            binaryTree.Insert(1);

            Assert.IsNotNull(binaryTree.Root);
            Assert.AreEqual(1, binaryTree.Root.Key);
        }

        [TestMethod]
        public void InsertPreservesTreeProperty()
        {
            int[] input = RandomOps.GetContiguousRandomSequence(1, 100);

            foreach(int key in input)
            {
                binaryTree.Insert(key);

                TestTreeCorrectness();
            }
        }

        protected virtual void TestTreeCorrectness()
        {
            Assert.IsTrue(binaryTree.IsBst());
        }

        #endregion

        #region Search

        [TestMethod]
        public void SearchOnEmptyTreeReturnsNil()
        {
            Assert.AreSame(binaryTree.Root, binaryTree.Search(1));
        }

        [TestMethod]
        public void SearchFindsRootAndReturnsReference()
        {
            binaryTree.Insert(1);

            Assert.AreSame(binaryTree.Root, binaryTree.Search(1));
        }

        [TestMethod]
        public void SearchReturnsNilWhenNodeIsNotFound()
        {
            binaryTree.Insert(3);
            binaryTree.Insert(2);
            binaryTree.Insert(4);

            Assert.AreSame(binaryTree.Nil, binaryTree.Search(5));
        }

        [TestMethod]
        public void SearchReturnsReferenceToNodeWhenFound()
        {
            binaryTree.Insert(3);
            binaryTree.Insert(2);
            IBinaryNode node = binaryTree.Insert(5);
            binaryTree.Insert(4);

            Assert.AreSame(node, binaryTree.Search(5));
        }

        #endregion

        #region Remove

        [TestMethod]
        public void RemoveRoot()
        {
            binaryTree.Insert(1);
            binaryTree.Remove(1);

            Assert.AreSame(binaryTree.Nil, binaryTree.Root);
        }

        [TestMethod]
        public void RemoveRemovesLeaf()
        {
            Bst bst = new Bst();
            bst.Insert(1);
            bst.Insert(2);

            bst.Remove(2);

            Assert.AreSame(bst.Nil, bst.Root.Right);
            Assert.AreSame(bst.Nil, bst.Search(2));
        }

        [TestMethod]
        public virtual void RemoveNodeWithOneLeftChildReplacesNodeWithChild()
        {
            binaryTree.Insert(new int[] { 3, 2, 1});

            binaryTree.Remove(2);

            var one = binaryTree.Search(1);

            Assert.AreSame(one, binaryTree.Root.Left);
            Assert.AreSame(binaryTree.Root, one.Parent);
        }

        [TestMethod]
        public virtual void RemoveNodeWithOneRightChildReplacesNodeWithChild()
        {
            binaryTree.Insert(new int[] { 2, 1, 3, 4 });

            binaryTree.Remove(3);

            var three = binaryTree.Search(4);

            Assert.AreSame(three, binaryTree.Root.Right);
            Assert.AreSame(binaryTree.Root, three.Parent);
        }

        [TestMethod]
        [ExpectedException(typeof(ElementNotFoundException), "element '1' not found in collection")]
        public void RemoveNonExistentThrowsElementNotFound()
        {
            binaryTree.Remove(1);
        }

        [TestMethod]
        public virtual void RemoveCorrectlyRemovesAcrossArrayOfKeys()
        {
            TestRemove(RandomOps.GetContiguousRandomSequence(1, 100));
        }

        public void TestRemove(int[] keys)
        {
            for (int i = 0; i < keys.Length; ++i)
                TestRemoveAtIndex(keys, i);
        }

        protected virtual void TestRemoveAtIndex(int[] keys, int index)
        {
            List<int> keyList = new List<int>(keys);

            binaryTree = CreateBinaryTree();

            //TODO: investigate why this doesn't work
            //while (binaryTree.Root != binaryTree.Nil)
            //    binaryTree.Remove(binaryTree.Root.Key);

            binaryTree.Insert(keys);

            int keyToRemove = keys[index];

            binaryTree.Remove(keyToRemove);

            Assert.AreEqual(binaryTree.Nil, binaryTree.Search(keyToRemove));

            foreach (int key in keys)
                if (key != keyToRemove)
                    Assert.AreEqual(key, binaryTree.Search(key).Key);

            TestTreeCorrectness();
        }

        #endregion
    }
}
