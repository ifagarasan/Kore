namespace KoreCode.Node.Builders
{
    public abstract class TreeNodeBuilder<T>
    {
        public TreeNodeBuilder()
        {
            Nil = CreateNode(0);
            DecorateNode(Nil);
        }

        public T Nil { get; }

        public T BuildNode(int key = 0)
        {
            var node = CreateNode(key);
            DecorateNode(node);
            return node;
        }

        protected abstract T CreateNode(int value);
        protected abstract void DecorateNode(T node);
    }
}