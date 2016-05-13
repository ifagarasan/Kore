using System;
using System.Collections.Generic;
using KoreCode.Exceptions;
using KoreCode.Traversals;
using KoreCode.Nodes.Builders;

namespace KoreCode.Trees.Binary
{
    public enum RotateDirection
    {
        Left = 0,
        Right
    };

    public abstract class BinaryTree
    {
        public BinaryNodeBuilder NodeBuilder { get; protected set; }

        public BinaryTree()
        {
            NodeBuilder = CreateNodeBuilder();
            Root = NodeBuilder.Nil;
        }

        protected abstract BinaryNodeBuilder CreateNodeBuilder();

        public IBinaryNode Root { get; protected set; }

        public int Count { get; private set; }

        public IBinaryNode Nil
        {
            get
            {
                return NodeBuilder.Nil;
            }
        }

        public virtual void Insert(int[] keys)
        {
            if (keys == null)
                throw new ArgumentNullException("key");

            foreach (int key in keys)
                Insert(key);
        }

        public virtual void Transplant(IBinaryNode node1, IBinaryNode node2)
        {
            if (node1.Parent == Nil)
                Root = node2;
            else if (node1.IsLeftChild)
                node1.Parent.Left = node2;
            else
                node1.Parent.Right = node2;

            node2.Parent = node1.Parent;
        }

        public virtual bool IsBalanced()
        {
            return IsNodeBalanced(Root);
        }

        protected bool IsNodeBalanced(IBinaryNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            return Math.Abs(node.Left.Height - node.Right.Height) < 2;
        }

        public bool IsBst()
        {
            if (Root == Nil)
                return true;

            return IsBst(Root.Left, Root.Key, 0) && IsBst(Root.Right, int.MaxValue, Root.Key);
        }

        bool IsBst(IBinaryNode node, int maxValue, int minValue)
        {
            if (node == Nil)
                return true;

            if (node.Key >= maxValue || node.Key < minValue) // not <=, because let's say the keys with equal value are located to the right
                return false;

            return IsBst(node.Left, node.Key, minValue) && IsBst(node.Right, maxValue, node.Key);
        }

        public IBinaryNode LowestCommonAncestor(int key1, int key2)
        {
            if (Root == Nil)
                throw new CollectionEmptyException();

            IBinaryNode x = Search(key1);
            IBinaryNode y = Search(key2);

            if (x == Nil)
                throw new ElementNotFoundException("node with key '" + key1 + "' was not found");

            if (y == Nil)
                throw new ElementNotFoundException("node with key '" + key2 + "' was not found");

            if (x == y)
                return x;

            Dictionary<int, IBinaryNode> visited = new Dictionary<int, IBinaryNode>();

            while (x != Nil && y != Nil)
            {
                if (visited.ContainsKey(x.Key))
                    return visited[x.Key];
                if (visited.ContainsKey(y.Key))
                    return visited[y.Key];

                visited.Add(x.Key, x);
                visited.Add(y.Key, y);

                x = x.Parent;
                y = y.Parent;
            }

            while (x != Nil)
            {
                if (visited.ContainsKey(x.Key))
                    return visited[x.Key];

                visited.Add(x.Key, x);
                x = x.Parent;
            }

            while (y != Nil)
            {
                if (visited.ContainsKey(y.Key))
                    return visited[y.Key];

                visited.Add(y.Key, y);
                y = y.Parent;
            }

            return Nil;
        }

        #region Traversals

        public void Inorder(NodeProcessor<IBinaryNode> processor)
        {
            bool continueExecution = true;

            Inorder(Root, processor, ref continueExecution);
        }

        protected void Inorder(IBinaryNode node, NodeProcessor<IBinaryNode> processor, ref bool continueExecution)
        {
            if (node == Nil || !continueExecution)
                return;

            Inorder(node.Left, processor, ref continueExecution);

            if (processor != null)
                continueExecution = processor(node);

            Inorder(node.Right, processor, ref continueExecution);
        }

        public void Preorder(NodeProcessor<IBinaryNode> processor)
        {
            Preorder(Root, processor);
        }

        protected void Preorder(IBinaryNode node, NodeProcessor<IBinaryNode> processor)
        {
            if (node == Nil)
                return;

            Stack<IBinaryNode> stack = new Stack<IBinaryNode>();
            stack.Push(node);

            bool continueExecution = true;
            while (stack.Count > 0 && continueExecution)
            {
                IBinaryNode current = stack.Pop();

                if (processor != null)
                    continueExecution = processor(current);

                if (current.Right != Nil)
                    stack.Push(current.Right);

                if (current.Left != Nil)
                    stack.Push(current.Left);
            }
        }

        public void Postorder(NodeProcessor<IBinaryNode> processor)
        {
            bool continueExecution = true;
            Postorder(Root, processor, ref continueExecution);
        }

        protected void Postorder(IBinaryNode node, NodeProcessor<IBinaryNode> processor, ref bool continueExecution)
        {
            if (node == Nil || !continueExecution)
                return;

            Postorder(node.Left, processor, ref continueExecution);
            Postorder(node.Right, processor, ref continueExecution);

            if (processor != null)
                continueExecution = processor(node);
        }

        #endregion

        #region Print

        public string PrettyPrintBFS()
        {
            string result = string.Empty;

            Traversals<IBinaryNode>.BreadthFirstSearch(Root, Nil, (x) => { result += string.Format("{0} ", x.Label); return true; } );

            return result;
        }

        #endregion

        public virtual void Remove(int key)
        {
            IBinaryNode node = Search(key);

            if (node == Nil)
                throw new ElementNotFoundException(key.ToString());

            Remove(node);
            Count--;
        }

        public IBinaryNode Max(IBinaryNode node)
        {
            while (node.Right != Nil)
                node = node.Right;

            return node;
        }

        public IBinaryNode Min(IBinaryNode node)
        {
            while (node.Left != Nil)
                node = node.Left;

            return node;
        }

        public IBinaryNode Successor(IBinaryNode node)
        {
            if (node.Right != Nil)
                return Min(node.Right);

            IBinaryNode current = node;
            IBinaryNode parent = node.Parent;

            while (parent != Nil && parent.Right == current)
            {
                current = current.Parent;
                parent = parent.Parent;
            }

            return parent;
        }

        public IBinaryNode Predecessor(IBinaryNode node)
        {
            if (node.Left != Nil)
                return Max(node.Left);
            
            IBinaryNode current = node;
            IBinaryNode parent = current.Parent;

            while (parent != Nil && parent.Left == current)
            {
                current = parent;
                parent = parent.Parent;
            }

            return parent;
        }

        #region Rotations

        public void RotateLeft(IBinaryNode node)
        {
            if (node == Nil)
                throw new ArgumentNullException("node");

            IBinaryNode target = node.Right;
            if (target == Nil)
                throw new NullReferenceException("node.Right null");

            node.Right = target.Left;

            if (target.Left != Nil)
                target.Left.Parent = node;

            target.Parent = node.Parent;

            if (node.Parent == Nil)
                Root = target;
            else if (node.IsLeftChild)
                node.Parent.Left = target;
            else
                node.Parent.Right = target;

            node.Parent = target;
            target.Left = node;
        }

        public void RotateRight(IBinaryNode node)
        {
            if (node == Nil)
                throw new ArgumentNullException("node");

            IBinaryNode target = node.Left;
            if (target == Nil)
                throw new NullReferenceException("node.Left is Nil");

            node.Left = target.Right;
            if (target.Right != Nil)
                target.Right.Parent = node;

            target.Parent = node.Parent;
            if (node.Parent == Nil)
                Root = target;
            else if (node.IsLeftChild)
                node.Parent.Left = target;
            else
                node.Parent.Right = target;

            target.Right = node;
            node.Parent = target;
        }

        public void Rotate(IBinaryNode node, RotateDirection direction)
        {
            if (direction == RotateDirection.Left)
                RotateLeft(node);
            else
                RotateRight(node);
        }

        #endregion

        public IBinaryNode Insert(int key)
        {
            if (Search(key) != Nil)
                throw new DuplicateKeyException(key.ToString());

            IBinaryNode node = NodeBuilder.BuildNode(key);
    
            Insert(node);
            Count++;

            return node;
        }

        public abstract void Insert(IBinaryNode node);
        public abstract IBinaryNode Search(int key);
        protected abstract void Remove(IBinaryNode node);
    }
}
