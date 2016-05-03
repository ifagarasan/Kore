using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Trees.Binary
{
    public class BinaryNode: IBinaryNode
    {
        public BinaryNode() { }

        public BinaryNode(int key)
        {
            Key = key;
        }

        public IBinaryNode Left { get; set; }
        public IBinaryNode Right { get; set; }
        public IBinaryNode Parent { get; set; }
        public int Key { get; set; }

        public IBinaryNode Uncle
        {
            get
            {
                return Parent.Sibling;
            }
        }

        public virtual string Label
        {
            get
            {
                string nodeInfo = string.Empty;

                if (IsRoot)
                    nodeInfo = " - Root";
                else
                    nodeInfo = IsLeftChild ? "L" : "R";

                return string.Format("{0}{1}", Key, nodeInfo);
            }
        }

        public bool IsRoot
        {
            get
            {
                return Parent == Parent.Parent;
            }
        }

        public bool IsLeaf
        {
            get
            {
                return !IsRoot && Left.IsNil && Right.IsNil;
            }
        }

        public bool IsInternalNode
        {
            get
            {
                return !IsLeaf && !IsRoot;
            }
        }

        public IBinaryNode Sibling
        {
            get
            {
                return this == Parent.Left ? Parent.Right : Parent.Left;
            }
        }

        public IBinaryNode Grandparent
        {
            get
            {
                return Parent.Parent;
            }
        }

        public bool IsLeftChild
        {
            get
            {
                return Parent.Left == this;
            }
        }

        public bool IsRightChild
        {
            get
            {
                return Parent.Right == this;
            }
        }

        public bool IsNil
        {
            get
            {
                return Left == this && Right == this && Parent == this;
            }
        }

        public virtual Color Color
        {
            get
            {
                throw new NotSupportedException("BinaryNode does not support Color");
            }
            set
            {
                throw new NotSupportedException("BinaryNode does not support Color");
            }
        }

        //TODO: optimise
        public virtual int Height
        {
            get
            {
                if (IsNil || (Left.IsNil && Right.IsNil))
                    return 0;

                return 1 + Math.Max(Left.Height, Right.Height);
            }
        }
    }
}
