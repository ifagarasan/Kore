using Kore.Vector;

namespace Kore.Logic.Selection
{
    public interface ISelectionStrategy<T>
    {
        Element<T> RetrieveCell(IVector<T> vector);
    }
}