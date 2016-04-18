using System;
using KoreCode.Util;

namespace KoreCode.Heaps
{
    public enum HeapType
    {
        Max = 0,
        Min
    };

    public class BinaryHeap<T> where T : IComparable
    {
        HeapType heapType = HeapType.Max;

        const int Nil = 0;

        public BinaryHeap(int capacity, HeapType heapType = HeapType.Max)
        {
            Validation<int>.IsLargerThan(capacity, 0);

            Capacity = capacity;
            Array = new T[capacity + 1];
            HeapType = heapType;
        }

        public BinaryHeap(T[] input, HeapType heapType = HeapType.Max) : this(input.Length + 1, heapType)
        {
            Insert(input);
        }

        public int Capacity { get; private set; }
        public int Count { get; private set; }
        public T[] Array { get; private set; }

        public Func<T, T, bool> CompareFunc { get; private set; }

        public HeapType HeapType
        {
            get
            {
                return heapType;
            }
            set
            {
                heapType = value;
                CompareFunc = GetComparisonFunction();
            }
        }

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

        public T this[int index]
        {
            get
            {
                ValidateHeapIndex(index);
                return Array[index];
            }
            set
            {
                ValidateHeapIndex(index);
                Array[index] = value;
            }
        }

        public T Root
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
            Validation<int>.IsLargerThan(index, 0);
            Validation<int>.IsSmallerThanOrEqualTo(index, Count);
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
                if (!CompareFunc(Array[index], Array[childExtremeIndex]))
                    return false;

                index = childExtremeIndex;
                childExtremeIndex = GetChildrenExtremeIndex(index);
            }

            return true;
        }

        public bool Contains(T value)
        {
            for (int i = 1; i <= Count; ++i)
                if (Array[i].CompareTo(value) == 0)
                    return true;

            return false;
        }

        public int Insert(T value)
        {
            if (IsFull)
                throw new Exception("collection is full");

            Count++;
            Array[Count] = value;
            int index = Count;

            HeapifyUp(GetParentIndex(Count), ref index);

            return index;
        }

        public void Insert(T[] values)
        {
            Validation<T>.ValidateArray(values);

            foreach (T value in values)
                Insert(value);
        }

        public T Remove(int index)
        {
            ValidateHeapIndex(index);

            T removedValue = Array[index];

            Array[index] = Array[Count--];

            if (index <= Count)
                HeapifyDown(index);

            return removedValue;
        }

        public T Pop()
        {
            if (IsEmpty)
                throw new Exception("collection is empty");

            return Remove(1);
        }

        private Func<T, T, bool> GetComparisonFunction()
        {
            if (HeapType == HeapType.Max)
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

            return CompareFunc(Array[leftIndex], Array[rightIndex]) ? leftIndex : rightIndex;
        }

        private void HeapifyDown(int index)
        {
            int childExtremeIndex = GetChildrenExtremeIndex(index);
            if (childExtremeIndex == Nil)
                return;

            if (CompareFunc(Array[childExtremeIndex], Array[index]))
            {
                ArrayOps<T>.Exchange(Array, childExtremeIndex, index);
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
            else if (CompareFunc(Array[childExtremeIndex], Array[index]))
            {
                ArrayOps<T>.Exchange(Array, childExtremeIndex, index);
                finalIndex = index;
                HeapifyUp(parentIndex, ref finalIndex);
            }
        }
    }
}
