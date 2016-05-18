using System;
using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Kore.Code;
using Kore.Code.Exceptions;
using Kore.Code.Util;
using Kore.Code.Traversals;
using Kore.Code.Trees.Binary;
using System.Collections.Generic;
using Kore.Code.Node.Builders;

namespace Kore.Code.Tests.Util
{
    [TestClass]
    public class TraversalsFunctionality
    {
        BinaryNodeBuilder nodeBuilder;

        [TestInitialize]
        public void Setup()
        {
            nodeBuilder = new BinaryNodeBuilder();
        }

        #region BreadthFirstSearch

        [TestMethod]
        public void BFSCallsNodeProcessorInOrderForEveryNonNillNodePassingNodeReference()
        {
            List<IBinaryNode> order = new List<IBinaryNode>();
            for (int i = 1; i <= 5; ++i)
                order.Add(nodeBuilder.BuildNode(i));

            IBinaryNode root = order[0];
            root.Left = order[1];
            root.Right = order[2];
            root.Left.Left = order[3];
            root.Left.Right = order[4];

            int index = 0;

            Traversals<IBinaryNode>.BreadthFirstSearch(root, nodeBuilder.Nil, (x) =>
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

            Traversals<IBinaryNode>.BreadthFirstSearch(nodeBuilder.Nil, nodeBuilder.Nil, (x) =>
            {
                index++;
                return true;
            });

            Assert.AreEqual(0, index);
        }

        #endregion
    }
}
