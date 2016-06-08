using System;
using Kore.Exceptions;

namespace Kore.Code.Queue
{
    public class Queue<T>
    {
        private int _count;
        private QueueItem<T> _head;
        private QueueItem<T> _tail;

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
            var newItem = new QueueItem<T>(value);

            if (_head == null)
            {
                _head = _tail = newItem;
            }
            else
            {
                if (_tail == null)
                    throw new NullReferenceException("tail null");

                _tail.Next = newItem;
                _tail = newItem;
            }

            Count++;
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new CollectionEmptyException();

            if (_head == null)
                throw new Exception("head null");

            var value = _head.Value;
            _head = _head.Next;
            Count--;

            return value;
        }

        public T Peek()
        {
            if (Count == 0)
                throw new CollectionEmptyException();

            if (_head == null)
                throw new NullReferenceException("head null");

            return _head.Value;
        }
    }
}