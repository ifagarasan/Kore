namespace KoreCode.Trees.Binary.RedBlackTree
{
    public class RedBlackNode : BinaryNode
    {
        public RedBlackNode() : this(0)
        {
        }

        public RedBlackNode(int key) : base(key)
        {
            Color = Color.Red;
        }

        public override Color Color { get; set; }

        public override string Label
        {
            get { return string.Format("{0} ({1})", base.Label, Color); }
        }

        //TODO: optimise
        public override int Height
        {
            get
            {
                if (IsNil)
                    return 0;

                var current = Left;
                var amount = 1;

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