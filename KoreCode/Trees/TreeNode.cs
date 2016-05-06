using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Trees
{
    public abstract class TreeNode<T>: ITreeNode<T> where T : ITreeNode<T>
    {
        public TreeNode(): this(0) { }

        public TreeNode(int key)
        {
            Key = key;
        }

        public virtual int Key { get; set; }

        public virtual T Parent { get; set; }

        public virtual bool IsRoot
        {
            get
            {
                return Parent.IsNil;
            }
        }

        public virtual bool IsInternalNodeFunc()
        {
            return !IsLeaf && !IsRoot;
        }

        public virtual bool IsInternalNode
        {
            get
            {
                return !IsLeaf && !IsRoot;
            }
        }

        public virtual T Grandparent
        {
            get
            {
                return Parent.Parent;
            }
        }

        public virtual string Label
        {
            get
            {
                return Key.ToString();
            }
        }

        public abstract int Height { get; }
        public abstract bool IsNil { get; }
        public abstract bool IsLeaf { get; }
    }
}
