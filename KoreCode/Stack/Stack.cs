using System;
using KoreCode.Exceptions;

namespace KoreCode.Stack
{
    public class Stack<T>: IStack<T>
    {
        int count;
        Item<T> top;

        public void Push(T value)
        {
            Item<T> newItem = new Item<T>(value);

            if (top == null)
                top = newItem;
            else
            {
                newItem.Bottom = top;
                top = newItem;
            }

            Count++;
        }

        public int Count
        {
            get
            {
                return count;
            }
            private set
            {
                if (value < 0)
                    throw new IndexOutOfRangeException("count cannot be negative");

                count = value;
            }
        }

        public T Pop()
        {
            if (Count == 0)
                throw new CollectionEmptyException();

            T result = top.Value;

            top = top.Bottom;
            Count--;

            return result;
        }

        public T Peek()
        {
            if (Count == 0)
                throw new CollectionEmptyException();

            return top.Value;
        }
    }
}
