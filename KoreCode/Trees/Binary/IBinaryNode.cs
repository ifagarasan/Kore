using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Trees.Binary
{
    public enum Color
    {
        Red = 0,
        Black
    }

    public interface IBinaryNode
    {
        IBinaryNode Left { get; set; }
        IBinaryNode Right { get; set; }
        IBinaryNode Parent { get; set; }
        IBinaryNode Uncle { get; }
        IBinaryNode Grandparent { get; }
        int Key { get; set; }
        Color Color { get; set; }
        bool IsRoot { get; }
        bool IsLeaf { get; }
        bool IsInternalNode { get; }
        bool IsLeftChild { get; }
        bool IsRightChild { get; }
        IBinaryNode Sibling { get; }
        int Height { get; }
        bool IsNil { get; }

        string Label { get; }
    }
}
