using System;
using KoreCode.Exceptions;

namespace KoreCode.Queue
{
    public class Queue<T>
    {
        int count;
        QueueItem<T> head, tail;

        public int Count
        {
            get
            {
                return count;
            }
            private set
            {
                if (value < 0)
                    throw new IndexOutOfRangeException("Count cannot be negative");

                count = value;
            }
        }

        public void Enqueue(T value)
        {
            QueueItem<T> newItem = new QueueItem<T>(value);

            if (head == null)
            {
                head = tail = newItem;
            }
            else
            {
                if (tail == null)
                    throw new NullReferenceException("tail null");

                tail.Next = newItem;
                tail = newItem;
            }

            Count++;
        }

        public T Dequeue()
        {
            if (Count == 0)
                throw new CollectionEmptyException();

            if (head == null)
                throw new Exception("head null");

            T value = head.Value;
            head = head.Next;
            Count--;

            return value;
        }

        public T Peek()
        {
            if (Count == 0)
                throw new CollectionEmptyException();

            if (head == null)
                throw new NullReferenceException("head null");

            return head.Value;
        }
    }
}
