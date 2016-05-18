using KoreCode.Node.Builders;

namespace KoreCode.Trees.Binary
{
    public class Bst : BinaryTree
    {
        protected override BinaryNodeBuilder CreateNodeBuilder()
        {
            return new BinaryNodeBuilder();
        }

        public override void Insert(IBinaryNode node)
        {
            if (Root == Nil)
            {
                Root = node;
                return;
            }

            var current = Root;
            var parent = Root;

            while (current != Nil)
            {
                parent = current;
                current = node.Key < current.Key ? current.Left : current.Right;
            }

            node.Parent = parent;

            if (node.Key < parent.Key)
                parent.Left = node;
            else
                parent.Right = node;
        }

        protected override void Remove(IBinaryNode node)
        {
            if (node.Left == Nil)
                Transplant(node, node.Right);
            else if (node.Right == Nil)
                Transplant(node, node.Left);
            else
            {
                var successor = Min(node.Right);
                if (successor != node.Right)
                {
                    Transplant(successor, successor.Right);
                    successor.Right = node.Right;
                    node.Right.Parent = successor;
                }

                Transplant(node, successor);
                node.Left.Parent = successor;
                successor.Left = node.Left;
            }
        }

        public override IBinaryNode Search(int key)
        {
            var node = Root;

            while (node != Nil)
            {
                if (node.Key == key)
                    break;

                node = key < node.Key ? node.Left : node.Right;
            }

            return node;
        }
    }
}