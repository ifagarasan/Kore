using KoreCode.Trees.Binary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Trees.Height
{
    public class BinaryHeightProcessor: HeightProcessor
    {
        public BinaryHeightProcessor(IBinaryNode Nil) : base(Nil) { }

        protected override int GetNodeHeight(IBinaryNode node)
        {
            if (node == Nil)
                return 0;

            return 1 + Math.Max(GetNodeHeight(node.Left), GetNodeHeight(node.Right));
        }
    }
}
