namespace Kore.Logic.Dynamic
{
    public interface IGrid<T>
    {
        T Value { get; set; }

        void Add(T amount);
    }
}