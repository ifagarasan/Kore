using Kore.Input.Keys;

namespace Kore.Input.Keyboard
{
    public interface IKeyboardTracker
    {
        void Press(Key key);
        bool Pressed(Key key);
        void Release(Key key);
    }
}