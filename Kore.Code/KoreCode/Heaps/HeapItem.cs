using System;
using KoreCode.Util;

namespace KoreCode.Heaps
{
    public class HeapItem<T, R> where T : IComparable
    {
        public HeapItem() { }

        public HeapItem(T key)
        {
            Key = key;
        }

        public HeapItem(T key, R data)
        {
            Key = key;
            Data = data;
        }

        public T Key { get; set; }
        public R Data { get; set; }
    }
}
