using System;
using KoreCode.Trees.Binary;

namespace KoreCode.Nodes.Builders
{
    public class BinaryNodeBuilder<T> where T: IBinaryNode, new()
    {
        public BinaryNodeBuilder()
        {
            Nil = CreateNode(0);
            DecorateNode(Nil);
        }

        public T Nil { get; private set; }

        private T CreateNode(int value)
        {
            var node = new T();
            node.Key = value;
            return node;
        }

        private void DecorateNode(IBinaryNode node)
        {
            node.Left = node.Right = node.Parent = Nil;
        }

        public IBinaryNode BuildNode(int key = 0)
        {
            var node = CreateNode(key);
            DecorateNode(node);
            return node;
        }
    }
}