using System.Windows.Forms;
using Kore.Input.Keys;

namespace Kore.Input
{
    public interface IKeyBuilder
    {
        Key Build(KeyEventArgs e);
        Key BuildKeyUp(KeyEventArgs e);
        Key BuildKeyDown(KeyEventArgs e);
    }
}