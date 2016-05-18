using KoreCode.Trees.Binary;
using KoreCode.Trees.Binary.RedBlackTree;

namespace KoreCode.Node.Builders
{
    public class RedBlackNodeBuilder: BinaryNodeBuilder
    {
        public RedBlackNodeBuilder()
        {
            Nil.Color = Color.Black;
        }

        protected override BinaryNode CreateNode(int value)
        {
            return new RedBlackNode(value);
        }
    }
}