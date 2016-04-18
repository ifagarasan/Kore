using KoreCode.Trees.Binary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Trees.Height
{
    public class BlackHeightProcessor : HeightProcessor
    {
        public BlackHeightProcessor(IBinaryNode Nil) : base(Nil) { }

        protected override int GetNodeHeight(IBinaryNode node)
        {
            if (node == Nil)
                return 0;

            IBinaryNode current = node.Left;
            int amount = 1;

            while (current != Nil)
            {
                amount += current.Color == Color.Black ? 1 : 0;
                current = current.Left;
            }

            return amount;
        }
    }
}
