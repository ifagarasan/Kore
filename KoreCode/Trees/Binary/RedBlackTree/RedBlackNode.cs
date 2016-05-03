﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoreCode.Trees.Binary.RedBlackTree
{
    public class RedBlackNode : BinaryNode
    {
        public RedBlackNode() { }

        public RedBlackNode(int key): base(key) { }

        public override Color Color { get; set; }

        public override string Label
        {
            get
            {
                return string.Format("{0} ({1})", base.Label, Color.ToString());
            }
        }

        //TODO: optimise
        public override int Height
        {
            get
            {
                if (IsNil)
                    return 0;

                IBinaryNode current = Left;
                int amount = 1;

                while (!current.IsNil)
                {
                    amount += current.Color == Color.Black ? 1 : 0;
                    current = current.Left;
                }

                return amount;
            }
        }
    }
}