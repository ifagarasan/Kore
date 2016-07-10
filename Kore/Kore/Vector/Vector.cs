using System.Collections;
using System.Collections.Generic;
using Kore.Exceptions;

namespace Kore.Vector
{
    public class Vector<T> : IVector<T>
    {
        public Vector(uint length)
        {
            if (length == 0)
                throw new InvalidQuantityException();

            Elements = new Element<T>[length];

            for (int i = 0; i < Elements.Length; ++i)
                Elements[i] = new Element<T>();
        }

        public uint Count => (uint)Elements.Length;

        public Element<T>[] Elements { get; }

        public Element<T> this[uint index] => Elements[index];

        public IEnumerator<Element<T>> GetEnumerator()
        {
            foreach (var cell in Elements)
                yield return cell;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}