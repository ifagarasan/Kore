using KoreCode.Trees.Binary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Trees.Height
{
    public abstract class HeightProcessor: IHeightProcessor
    {
        Dictionary<int, int> heights;

        public HeightProcessor(IBinaryNode Nil)
        {
            heights = new Dictionary<int, int>();
            this.Nil = Nil;
        }

        public IBinaryNode Nil { get; set; }

        public int GetHeight(IBinaryNode node)
        {
            //if (heights.ContainsKey(node.Key))
            //    return heights[node.Key];

            int height = GetNodeHeight(node);
            //heights[node.Key] = height;

            return height;
        }

        public void Clear()
        {
            heights.Clear();
        }

        protected abstract int GetNodeHeight(IBinaryNode node);
    }
}
