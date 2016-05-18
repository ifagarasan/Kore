using System;
using Kore.Code.Exceptions;

namespace Kore.Code.Queue
{
    public class Deque<T>
    {
        //TODO: investigate whether the count can be removed and inferred from indices
        private readonly T[] content;
        private int count;
        private int headIndex;
        private int tailIndex;

        public Deque(uint capacity)
        {
            if (capacity == 0)
                throw new Exception("capacity cannot be 0");

            content = new T[capacity];
        }

        public int Capacity
        {
            get { return content.Length; }
        }


        public int Count
        {
            get { return count; }
            private set
            {
                if (value < 0)
                    throw new IndexOutOfRangeException("Count cannot be negative");

                count = value;
            }
        }

        public void Enqueue(T value)
        {
            if (Count == Capacity)
                throw new CollectionFullException("Deque");

            if (Count == 0)
                content[headIndex] = value;
            else
            {
                tailIndex = GetNextIndex(tailIndex);
                content[tailIndex] = value;
            }

            Count++;
        }

        public T Peek()
        {
            return Peek(headIndex);
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new CollectionEmptyException();

            var value = content[headIndex];

            headIndex = GetNextIndex(headIndex);
            Count--;

            return value;
        }

        public void EnqueueFront(T value)
        {
            if (Count == Capacity)
                throw new CollectionFullException("Deque");

            if (Count == 0)
                content[headIndex] = value;
            else
            {
                headIndex = GetPrevIndex(headIndex);
                content[headIndex] = value;
            }

            Count++;
        }

        public T PeekTail()
        {
            return Peek(tailIndex);
        }

        public T DequeueTail()
        {
            if (Count == 0)
                throw new CollectionEmptyException();

            var value = content[tailIndex];

            tailIndex = GetPrevIndex(tailIndex);
            Count--;

            return value;
        }

        private int GetPrevIndex(int index)
        {
            return index - 1 >= 0 ? index - 1 : Capacity - 1;
        }

        private int GetNextIndex(int index)
        {
            return index + 1 >= Capacity ? 0 : index + 1;
        }

        private T Peek(int index)
        {
            if (Count == 0)
                throw new CollectionEmptyException();

            return content[index];
        }
    }
}