using System;
using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using KoreCode;
using KoreCode.Exceptions;
using KoreCode.Util;
using KoreCode.Traversals;
using KoreCode.Trees.Binary;
using System.Collections.Generic;

namespace KoreCodeTest.Util
{
    [TestClass]
    public class TraversalsFunctionality
    {
        IBinaryNode Nil;

        IBinaryNode CreateNode(int key)
        {
            return new BinaryNode(key);
        }

        void DecorateNode(IBinaryNode node)
        {
            node.Left = node.Right = node.Parent = Nil;
        }

        IBinaryNode BuildNode(int key = 0)
        {
            var node = CreateNode(key);
            DecorateNode(node);
            return node;
        }

        [TestInitialize]
        public void Setup()
        {
            Nil = BuildNode();
        }

        #region BreadthFirstSearch

        [TestMethod]
        public void BFSCallsNodeProcessorInOrderForEveryNonNillNodePassingNodeReference()
        {
            List<IBinaryNode> order = new List<IBinaryNode>();
            for (int i = 1; i <= 5; ++i)
                order.Add(BuildNode(i));

            IBinaryNode root = order[0];
            root.Left = order[1];
            root.Right = order[2];
            root.Left.Left = order[3];
            root.Left.Right = order[4];

            int index = 0;

            Traversals<IBinaryNode>.BreadthFirstSearch(root, Nil, (x) =>
            {
                Assert.AreSame(order[index], x);
                index++;
                return true;
            });

            Assert.AreEqual(order.Count, index);
        }

        [TestMethod]
        public void BFSDoesNothingIfNodePassedIsNil()
        {
            int index = 0;

            Traversals<IBinaryNode>.BreadthFirstSearch(Nil, Nil, (x) =>
            {
                index++;
                return true;
            });

            Assert.AreEqual(0, index);
        }

        #endregion
    }
}
