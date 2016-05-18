using Kore.Code.Trees.Binary;
using Kore.Code.Trees.Binary.RedBlackTree;

namespace Kore.Code.Node.Builders
{
    public class RedBlackNodeBuilder : BinaryNodeBuilder
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