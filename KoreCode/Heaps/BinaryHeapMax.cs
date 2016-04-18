﻿using System;
using KoreCode.Util;

namespace KoreCode.Heaps
{
    public class BinaryHeapMin<T, R>: BinaryHeap<T, R> where T : IComparable
    {
        public BinaryHeapMin(int capacity) : base(capacity, HeapType.Min) { }

        public BinaryHeapMin(T[] input) : base(input, HeapType.Min) { }
    }
}
