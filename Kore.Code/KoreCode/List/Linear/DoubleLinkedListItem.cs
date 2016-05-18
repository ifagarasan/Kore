namespace KoreCode.List.Linear
{
    public class DoubleLinkedListItem<T> : ListItem<T>
    {
        public DoubleLinkedListItem()
        {
        }

        public DoubleLinkedListItem(T value) : base(value)
        {
        }

        public override ListItem<T> Next { get; set; }
        public override ListItem<T> Prev { get; set; }
    }
}