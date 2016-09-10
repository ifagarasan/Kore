using System.Windows.Forms;
using Kore.Input.Keys;

namespace Kore.Input
{
    public class KeyBuilder : IKeyBuilder
    {
        public Key Build(KeyEventArgs e) => new Key(Symbol(e));

        public Key BuildKeyUp(KeyEventArgs e) => new KeyUp(Symbol(e));

        public Key BuildKeyDown(KeyEventArgs e) => new KeyDown(Symbol(e));

        private static char Symbol(KeyEventArgs e) => (char)e.KeyValue;
    }
}