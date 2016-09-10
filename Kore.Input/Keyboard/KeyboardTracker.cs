using System.Collections.Generic;
using Kore.Input.Keys;

namespace Kore.Input.Keyboard
{
    public class KeyboardTracker : IKeyboardTracker
    {
        private readonly Dictionary<string, bool> _map;

        public KeyboardTracker()
        {
            _map = new Dictionary<string, bool>();
        }

        public void Press(Key key)
        {
            Mark(key, true);
        }

        public void Release(Key key)
        {
            Mark(key, false);
        }

        public bool Pressed(Key key) => _map.ContainsKey(key.Symbol) && _map[key.Symbol];

        private void Mark(Key key, bool pressed)
        {
            if (!_map.ContainsKey(key.Symbol))
                _map.Add(key.Symbol, pressed);

            _map[key.Symbol] = pressed;
        }
    }
}