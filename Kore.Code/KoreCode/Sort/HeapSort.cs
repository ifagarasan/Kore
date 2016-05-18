using System;
using KoreCode.Heaps;
using KoreCode.Validation;

namespace KoreCode.Sort
{
    public static class HeapSort<T> where T : IComparable
    {
        public static void Sort(T[] input, SortDirection direction = SortDirection.Ascending)
        {
            ArrayValidation<T>.ValidateArray(input);

            var heapType = direction == SortDirection.Ascending ? HeapType.Min : HeapType.Max;
            var heap = new BinaryHeap<T, object>(input, heapType);

            for (var i = 0; i < input.Length; ++i)
                input[i] = heap.ExtractRoot().Key;
        }
    }
}