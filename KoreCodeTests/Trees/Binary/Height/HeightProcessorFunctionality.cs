using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using KoreCode.Trees.Binary;
using KoreCode.Trees.Height;
using KoreCode.Exceptions;

namespace KoreCodeTests.Tree.Binary
{
    [TestClass]
    public abstract class HeightProcessorFunctionality
    {
        protected IHeightProcessor heightProcessor;
        protected BinaryTree binaryTree; 

        [TestMethod]
        public void HeightProcessorReturnsZeroForNil()
        {
            Assert.AreEqual(0, heightProcessor.GetHeight(binaryTree.Nil));
        }
    }
}
