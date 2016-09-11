using System.Collections.Generic;
using Kore.Code.Node;

namespace Kore.Code.Traversals
{
    public static class Traversals<T>
    {
        public static void BreadthFirstSearch(INode<T> node, INode<T> Nil, NodeProcessor<INode<T>> processor)
        {
            if (node == Nil)
                return;

            var queue = new Queue<INode<T>>();

            queue.Enqueue(node);

            var continueExecution = true;
            while (queue.Count > 0 && continueExecution)
            {
                var current = queue.Dequeue();

                if (current == Nil)
                    break;

                if (processor != null)
                    continueExecution = processor(current);

                foreach (INode<T> child in current)
                    if (child != Nil)
                        queue.Enqueue(child);
            }
        }
    }
}