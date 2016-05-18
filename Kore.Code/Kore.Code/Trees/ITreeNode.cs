using Kore.Code.Node;

namespace Kore.Code.Trees
{
    public interface ITreeNode<T> : INode<T>
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