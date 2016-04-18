using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KoreCode.Util;
using KoreCode.Heaps;
using KoreCode.Validation;

namespace KoreCode.Sort
{
    public static class HeapSort<T> where T : IComparable
    {
        public static void Sort(T[] input, SortDirection direction = SortDirection.Ascending)
        {
            ArrayValidation<T>.ValidateArray(input);

            HeapType heapType = direction == SortDirection.Ascending ? HeapType.Min : HeapType.Max;
            BinaryHeap<T, object> heap = new BinaryHeap<T, object>(input, heapType);

            for (int i = 0; i < input.Length; ++i)
                input[i] = heap.ExtractRoot().Key;
        }
    }
}
