namespace KoreCode.List
{
    public interface IList<T>
    {
        int Count { get; }
        T this[int index] { get; set; }

        void Add(T value);
        void Insert(T value, int index);
        bool Contains(T value);
        void RemoveAt(int index);
    }
}