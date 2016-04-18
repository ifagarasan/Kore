using System;
using KoreCode.Util;
using KoreCode.Validation;

namespace KoreCode.Heaps
{
    public enum HeapType
    {
        Max = 0,
        Min
    };

    public class BinaryHeap<T, R> where T : IComparable
    {
        HeapType heapType = HeapType.Max;

        const int Nil = 0;

        public BinaryHeap(int capacity, HeapType heapType = HeapType.Max)
        {
            ComparisonValidation<int>.IsLargerThan(capacity, 0);

            Capacity = capacity;
            Array = new HeapItem<T, R>[capacity+1];
            this.heapType = heapType;
            CompareFunc = GetComparisonFunction();
        }

        public BinaryHeap(T[] input, HeapType heapType = HeapType.Max): this(input.Length + 1, heapType)
        {
            Insert(input);
        }

        public int Capacity { get; private set; }
        public int Count { get; private set; }
        public HeapItem<T, R>[] Array { get; private set; }

        public Func<T, T, bool> CompareFunc { get; private set; }

        public bool IsFull
        {
            get
            {
                return Count == Capacity;
            }
        }

        public bool IsEmpty
        {
            get
            {
                return Count == 0;
            }
        }

        public HeapItem<T, R> this[int index]
        {
            get
            {
                ValidateHeapIndex(index);
                return Array[index];
            }
        }

        public HeapItem<T, R> Root
        {
            get
            {
                if (IsEmpty)
                    throw new Exception("collection empty");

                return Array[1];
            }
        }

        private void ValidateHeapIndex(int index)
        {
            ComparisonValidation<int>.IsLargerThan(index, 0);
            ComparisonValidation<int>.IsSmallerThanOrEqualTo(index, Count);
        }

        public int GetParentIndex(int index)
        {
            ValidateHeapIndex(index);

            return index / 2;
        }

        public int GetLeftChildIndex(int index)
        {
            ValidateHeapIndex(index);

            return GetValidIndex(2 * index);
        }

        public int GetRightChildIndex(int index)
        {
            ValidateHeapIndex(index);

            return GetValidIndex(2 * index + 1);
        }

        public bool HasChildren(int index)
        {
            ValidateHeapIndex(index);

            return index * 2 <= Count;
        }

        private int GetValidIndex(int index)
        {
            if (index < 1 || index > Count)
                return Nil;

            return index;
        }

        public bool IsHeap()
        {
            if (IsEmpty)
                return true;

            int index = 1;
            int childExtremeIndex = GetChildrenExtremeIndex(index);

            while (childExtremeIndex != Nil)
            {
                if (!CompareFunc(Array[index].Key, Array[childExtremeIndex].Key))
                    return false;

                index = childExtremeIndex;
                childExtremeIndex = GetChildrenExtremeIndex(index);
            }

            return true;
        }

        public bool ContainsKey(T key)
        {
            for (int i = 1; i <= Count; ++i)
                if (Array[i].Key.CompareTo(key) == 0)
                    return true;

            return false;
        }

        public int Insert(T key, R data=default(R))
        {
            if (IsFull)
                throw new Exception("collection is full");

            Count++;
            Array[Count] = new HeapItem<T, R>(key, data);
            int index = Count;

            HeapifyUp(GetParentIndex(Count), ref index);

            return index;
        }

        public void Insert(T[] keys)
        {
            ArrayValidation<T>.ValidateArray(keys);

            foreach (T value in keys)
                Insert(value);
        }

        public HeapItem<T, R> Remove(int index)
        {
            ValidateHeapIndex(index);

            HeapItem<T, R> removed = Array[index];

            Array[index] = Array[Count--];

            if (index <= Count)
                HeapifyDown(index);

            return removed;
        }

        public HeapItem<T, R> ExtractRoot()
        {
            if (IsEmpty)
                throw new Exception("collection is empty");

            return Remove(1);
        }

        private Func<T, T, bool> GetComparisonFunction()
        {
            if (heapType == HeapType.Max)
                return Comparers<T>.LargerThan;

            return Comparers<T>.LessThan;
        }

        private int GetChildrenExtremeIndex(int index)
        {
            int leftIndex = GetLeftChildIndex(index);
            int rightIndex = GetRightChildIndex(index);

            if (leftIndex == Nil && rightIndex == Nil)
                return Nil;
            else if (leftIndex == Nil)
                return rightIndex;
            else if (rightIndex == Nil)
                return leftIndex;

            return CompareFunc(Array[leftIndex].Key, Array[rightIndex].Key) ? leftIndex : rightIndex;
        }

        private void HeapifyDown(int index)
        {
            int childExtremeIndex = GetChildrenExtremeIndex(index);
            if (childExtremeIndex == Nil)
                return;

            if (CompareFunc(Array[childExtremeIndex].Key, Array[index].Key))
            {
                Exchange<HeapItem<T, R>>.ArrayExchange(Array, childExtremeIndex, index);
                HeapifyDown(childExtremeIndex);
            }
        }

        private void HeapifyUp(int index, ref int finalIndex)
        {
            if (index == Nil)
                return;

            int childExtremeIndex = GetChildrenExtremeIndex(index);
            int parentIndex = GetParentIndex(index);

            if (childExtremeIndex == Nil)
                HeapifyUp(parentIndex, ref finalIndex);
            else if (CompareFunc(Array[childExtremeIndex].Key, Array[index].Key))
            {
                Exchange<HeapItem<T, R>>.ArrayExchange(Array, childExtremeIndex, index);
                finalIndex = index;
                HeapifyUp(parentIndex, ref finalIndex);
            }
        }
    }
}
