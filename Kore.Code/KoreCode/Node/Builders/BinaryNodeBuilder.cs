using System;
using KoreCode.Trees.Binary;

namespace KoreCode.Nodes.Builders
{
    public class BinaryNodeBuilder: TreeNodeBuilder<BinaryNode>
    {
        protected override BinaryNode CreateNode(int value)
        {
            return new BinaryNode(value);
        }

        protected override void DecorateNode(BinaryNode node)
        {
            node.Left = node.Right = node.Parent = Nil;
        }
    }
}