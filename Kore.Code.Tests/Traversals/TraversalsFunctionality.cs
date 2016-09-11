using System.Collections.Generic;
using Kore.Code.Node.Builders;
using Kore.Code.Traversals;
using Kore.Code.Trees.Binary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Kore.Code.Tests.Traversals
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
            var order = new List<IBinaryNode>();
            for (var i = 1; i <= 5; ++i)
                order.Add(nodeBuilder.BuildNode(i));

            var root = order[0];
            root.Left = order[1];
            root.Right = order[2];
            root.Left.Left = order[3];
            root.Left.Right = order[4];

            var index = 0;

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
            var index = 0;

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
