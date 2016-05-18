namespace Kore.Code.Stack
{
    public interface IStack<T>
    {
        int Count { get; }
        void Push(T value);

        T Pop();

        T Peek();
    }
}