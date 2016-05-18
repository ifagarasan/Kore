namespace Kore.Code.Trees.Binary
{
    public enum Color
    {
        Red = 0,
        Black
    }

    public interface IBinaryNode : ITreeNode<IBinaryNode>
    {
        IBinaryNode Left { get; set; }
        IBinaryNode Right { get; set; }
        IBinaryNode Uncle { get; }
        Color Color { get; set; }
        bool IsLeftChild { get; }
        bool IsRightChild { get; }
        IBinaryNode Sibling { get; }
    }
}