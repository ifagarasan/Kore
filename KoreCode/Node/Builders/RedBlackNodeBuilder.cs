using System;
using KoreCode.Trees.Binary;
using KoreCode.Trees.Binary.RedBlackTree;

namespace KoreCode.Nodes.Builders
{
    public class RedBlackNodeBuilder: BinaryNodeBuilder
    {
        protected override BinaryNode CreateNode(int value)
        {
            var node = new RedBlackNode(value);
            node.Color = Color.Red;
            return node;
        }
    }
}