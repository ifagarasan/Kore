using KoreCode.Exceptions;
using System;

namespace KoreCode.Trees.Binary.AvlTree
{
    public class Avl : Bst
    {
        public override void Insert(IBinaryNode node)
        {
            base.Insert(node);
            BalanceUp(node.Parent);
        }

        protected void BalanceUp(IBinaryNode node)
        {
            while (node != Nil)
            {
                int balance = BalanceProvider.GetBalanceOffset(node);

                if (Math.Abs(balance) > 2)
                    throw new Exception(string.Format("unexpected node {0} to have balance {1}", node.Key, balance));

                if (Math.Abs(balance) == 2)
                {
                    if (balance == 2)
                    {
                        int leftBalance = BalanceProvider.GetBalanceOffset(node.Left);

                        if (leftBalance < 0)
                            RotateLeft(node.Left);

                        RotateRight(node);
                        node = node.Parent;
                    }
                    else
                    {
                        int rightBalance = BalanceProvider.GetBalanceOffset(node.Right);

                        if (rightBalance > 0)
                            RotateRight(node.Right);

                        RotateLeft(node);
                        node = node.Parent;
                    }
                }

                node = node.Parent;
            }
        }
    }
}