using System;
using KoreCode.Exceptions;

namespace KoreCode.Stack
{
    public interface IStack<T>
    {
        void Push(T value);

        int Count { get; }

        T Pop();

        T Peek();
    }
}
