namespace KoreCode.Queue
{
    public class QueueItem<T>
    {
        public QueueItem()
        {
        }

        public QueueItem(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
        public QueueItem<T> Next { get; set; }
    }
}