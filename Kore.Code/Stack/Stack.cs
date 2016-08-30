using System;
using Kore.Exceptions;

namespace Kore.Code.Stack
{
    public class Stack<T> : IStack<T>
    {
        private int _count;
        private Item<T> _top;

        public void Push(T value)
        {
            var newItem = new Item<T>(value);

            if (_top == null)
                _top = newItem;
            else
            {
                newItem.Bottom = _top;
                _top = newItem;
            }

            Count++;
        }

        public int Count
        {
            get { return _count; }
            private set
            {
                if (value < 0)
                    throw new IndexOutOfRangeException("count cannot be negative");

                _count = value;
            }
        }

        public T Pop()
        {
            if (Count == 0)
                throw new CollectionEmptyException();

            var result = _top.Value;

            _top = _top.Bottom;
            Count--;

            return result;
        }

        public T Peek()
        {
            if (Count == 0)
                throw new CollectionEmptyException();

            return _top.Value;
        }
    }
}