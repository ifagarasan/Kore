using Kore.Input.Keys;

namespace Kore.Input
{
    public interface IKeyMap<T>
    {
        int Count { get; }
        bool Contains(Key key);
        T this[Key key] { get; }
    }
}