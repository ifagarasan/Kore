using KoreCode.Node;
using KoreCode.Trees.Binary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Traversals
{
    public delegate bool NodeProcessor<T>(T node);

    public static class Traversals<T>
    {
        public static void BreadthFirstSearch(INode<T> node, INode<T> Nil, NodeProcessor<INode<T>> processor)
        {
            if (node == Nil)
                return;

            Queue<INode<T>> queue = new Queue<INode<T>>();

            queue.Enqueue(node);

            bool continueExecution = true;
            while (queue.Count > 0 && continueExecution)
            {
                INode<T> current = queue.Dequeue();

                if (current == Nil)
                    break;

                if (processor != null)
                    continueExecution = processor(current);

                foreach(INode<T> child in current)
                    if (child != Nil)
                        queue.Enqueue(child);
            }
        }
    }
}
