using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Trees.Binary.RedBlackTree
{
    public class Node : BinaryNode
    {
        public Node() { }

        public Node(int key): base(key) { }

        public override Color Color { get; set; }

        public override string Label
        {
            get
            {
                return string.Format("{0} ({1})", base.Label, Color.ToString());
            }
        }
    }
}
