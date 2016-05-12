using KoreCode.Node;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Trees
{
    public interface ITreeNode<T>: INode<T>
    {
        T Parent { get; set; }
        T Grandparent { get; }
        bool IsNil { get; }
        bool IsRoot { get; }
        bool IsLeaf { get; }
        bool IsInternalNode { get; }
        int Height { get; }
    }
}
