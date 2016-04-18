using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using KoreCode.Trees.Binary.RedBlackTree;
using KoreCode.Trees.Height;
using KoreCode.Exceptions;
using KoreCode.Trees.Binary;

namespace KoreCodeTests.Tree.Binary
{
    [TestClass]
    public class BlackHeightProcessorFunctionality: HeightProcessorFunctionality
    {
        [TestInitialize]
        public void SetUp()
        {
            binaryTree = new KoreCode.Trees.Binary.RedBlackTree.RedBlackTree();

            heightProcessor = new BlackHeightProcessor(binaryTree.Nil);
        }

        [TestMethod]
        public void HeightProcessorReturnsZeroForEmptyTree()
        {
            Assert.AreEqual(0, heightProcessor.GetHeight(binaryTree.Root));
        }

        [TestMethod]
        public void HeightProcessorDoesNotIncludeStartingNode()
        {
            binaryTree.Insert(new int[] { 1 });

            Assert.AreEqual(1, heightProcessor.GetHeight(binaryTree.Root));
        }
    }
}
