using KoreCode.Exceptions;
using System;
using System.Collections.Generic;

namespace KoreCode.Trees.Binary.AvlTree
{
    public static class BalanceProvider
    {
        public static int GetBalanceOffset(IBinaryNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            if (node.Left.IsNil)
                return -node.Height;
            else if (node.Right.IsNil)
                return node.Height;

            return node.Left.Height - node.Right.Height;
        }
    }
}