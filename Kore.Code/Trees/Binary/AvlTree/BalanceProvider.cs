using System;

namespace Kore.Code.Trees.Binary.AvlTree
{
    public static class BalanceProvider
    {
        public static int GetBalanceOffset(IBinaryNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            if (node.Left.IsNil)
                return -node.Height;
            if (node.Right.IsNil)
                return node.Height;

            return node.Left.Height - node.Right.Height;
        }
    }
}