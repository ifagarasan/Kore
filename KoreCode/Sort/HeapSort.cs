using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KoreCode.Util;
using KoreCode.Heaps;

namespace KoreCode.Sort
{
    public static class HeapSort<T> where T : IComparable
    {
        public static void Sort(T[] input, SortDirection direction = SortDirection.Ascending)
        {
            Validation<T>.ValidateArray(input);

            HeapType heapType = direction == SortDirection.Ascending ? HeapType.Min : HeapType.Max;
            BinaryHeap<T> heap = new BinaryHeap<T>(input, heapType);

            for (int i = 0; i < input.Length; ++i)
                input[i] = heap.Pop();
        }
    }
}
