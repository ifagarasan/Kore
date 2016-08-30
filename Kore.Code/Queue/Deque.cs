using System;
using Kore.Exceptions;

namespace Kore.Code.Queue
{
    public class Deque<T>
    {
        //TODO: investigate whether the count can be removed and inferred from indices
        private readonly T[] _content;
        private int _count;
        private int _headIndex;
        private int _tailIndex;

        public Deque(uint capacity)
        {
            if (capacity == 0)
                throw new Exception("capacity cannot be 0");

            _content = new T[capacity];
        }

        public int Capacity
        {
            get { return _content.Length; }
        }


        public int Count
        {
            get { return _count; }
            private set
            {
                if (value < 0)
                    throw new IndexOutOfRangeException("Count cannot be negative");

                _count = value;
            }
        }

        public void Enqueue(T value)
        {
            if (Count == Capacity)
                throw new CollectionFullException("Deque");

            if (Count == 0)
                _content[_headIndex] = value;
            else
            {
                _tailIndex = GetNextIndex(_tailIndex);
                _content[_tailIndex] = value;
            }

            Count++;
        }

        public T Peek()
        {
            return Peek(_headIndex);
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new CollectionEmptyException();

            var value = _content[_headIndex];

            _headIndex = GetNextIndex(_headIndex);
            Count--;

            return value;
        }

        public void EnqueueFront(T value)
        {
            if (Count == Capacity)
                throw new CollectionFullException("Deque");

            if (Count == 0)
                _content[_headIndex] = value;
            else
            {
                _headIndex = GetPrevIndex(_headIndex);
                _content[_headIndex] = value;
            }

            Count++;
        }

        public T PeekTail()
        {
            return Peek(_tailIndex);
        }

        public T DequeueTail()
        {
            if (Count == 0)
                throw new CollectionEmptyException();

            var value = _content[_tailIndex];

            _tailIndex = GetPrevIndex(_tailIndex);
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

            return _content[index];
        }
    }
}