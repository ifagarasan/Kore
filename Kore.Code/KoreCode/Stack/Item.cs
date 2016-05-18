namespace KoreCode.Stack
{
    public class Item<T>
    {
        public Item()
        {
        }

        public Item(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
        public Item<T> Bottom { get; set; }
    }
}