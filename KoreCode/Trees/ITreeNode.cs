using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Trees
{
    public interface ITreeNode<T>
    {
        T Parent { get; set; }
        T Grandparent { get; }
        bool IsNil { get; }
        string Label { get; }
        bool IsRoot { get; }
        bool IsLeaf { get; }
        bool IsInternalNode { get; }

        bool IsInternalNodeFunc();

        int Height { get; }
    }
}
