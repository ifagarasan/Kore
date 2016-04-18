using System;

using KoreCode.Trees.Binary;
using KoreCode.Trees.Binary.RedBlackTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoreCode.Exceptions;

namespace KoreCodeTests.Tree.Binary.Nodes
{
    [TestClass]
    public class RedBlackNodeFunctionality: BinaryNodeFunctionality
    {
        protected override IBinaryNode CreateNode()
        {
            return new RedBlackNode();
        }
    }
}
