using KoreCode.Trees.Binary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Traversals
{
    public delegate bool NodeProcessor(IBinaryNode node);

    public static class Traversals
    {
        public static void BreadthFirstSearch(BinaryTree tree, NodeProcessor processor)
        {
            if (tree == null)
                throw new ArgumentNullException("tree");

            BreadthFirstSearch(tree.Root, tree.Nil, processor);
        }

        public static void BreadthFirstSearch(IBinaryNode node, IBinaryNode Nil, NodeProcessor processor)
        {
            Queue<IBinaryNode> queue = new Queue<IBinaryNode>();

            queue.Enqueue(node);

            bool continueExecution = true;
            while (queue.Count > 0 && continueExecution)
            {
                IBinaryNode current = queue.Dequeue();

                if (current == Nil)
                    break;

                if (processor != null)
                    continueExecution = processor(current);

                if (current.Left != Nil)
                    queue.Enqueue(current.Left);
                if (current.Right != Nil)
                    queue.Enqueue(current.Right);
            }
        }
    }
}
