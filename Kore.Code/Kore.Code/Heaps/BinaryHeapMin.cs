using System;

namespace Kore.Code.Heaps
{
    public class BinaryHeapMax<T, R> : BinaryHeap<T, R> where T : IComparable
    {
        public BinaryHeapMax(int capacity) : base(capacity, HeapType.Max)
        {
        }

        public BinaryHeapMax(T[] input) : base(input, HeapType.Max)
        {
        }
    }
}