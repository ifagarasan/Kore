using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using KoreCode.Trees.Binary;
using KoreCode.Trees.Height;
using KoreCode.Exceptions;

namespace KoreCodeTests.Tree.Binary
{
    [TestClass]
    public class BinaryHeightProcessorFunctionality: HeightProcessorFunctionality
    {
        [TestInitialize]
        public void SetUp()
        {
            binaryTree = new Bst();

            heightProcessor = new BinaryHeightProcessor(binaryTree.Nil);
        }

        [TestMethod]
        public void HeightProcessorReturnsOneForSingleNode()
        {
            binaryTree.Insert(1);

            Assert.AreEqual(1, heightProcessor.GetHeight(binaryTree.Root));
        }

        [TestMethod]
        public void HeightProcessorReturnsOnePlusMaxOfSubtree()
        {
            binaryTree.Insert(new int[] { 5, 3, 1, 7 });

            Assert.AreEqual(3, heightProcessor.GetHeight(binaryTree.Root));
        }

        //TODO: insert / remove tests that preserve height
    }
}
