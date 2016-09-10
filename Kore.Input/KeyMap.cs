using System.Collections.Generic;
using Kore.Input.Keys;

namespace Kore.Input
{
    public class KeyMap<T> : IKeyMap<T>
    {
        private readonly IDictionary<string, T> _keys;

        public KeyMap()
        {
            _keys = new Dictionary<string, T>();
        }

        public int Count => _keys.Keys.Count;

        public bool Contains(Key key) => _keys.ContainsKey(key.Symbol);

        public T this[Key key] => _keys[key.Symbol];

        public void Add(Key key, T item) => _keys.Add(key.Symbol, item);
    }
}