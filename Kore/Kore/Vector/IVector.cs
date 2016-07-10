using System.Collections.Generic;

namespace Kore.Vector
{
    public interface IVector<T>: IEnumerable<Element<T>>
    {
        uint Count { get; }

        Element<T>[] Elements { get; }

        Element<T> this[uint index] { get; }
    }
}
